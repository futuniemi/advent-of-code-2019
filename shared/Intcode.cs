using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2019.intcode
{
    public enum OpCode
    {
        Add = 1,
        Multiply = 2,
        SaveAt = 3,
        Output = 4,
        JumpIfTrue = 5,
        JumpIfFalse = 6,
        LessThan = 7,
        Equals = 8,
        AdjustRelativeBase = 9,
        Terminate = 99
    }

    public enum ParameterMode
    {
        Position = 0,
        Immediate = 1,
        Relative = 2
    }

    public class IntcodeMachine
    {
        private int pointer = 0;
        private List<long> program;
        private List<long> input;
        private int inputPointer = 0;

        private int relativeBase = 0;

        public void Modify(int valueAt, long replaceWith)
        {
            this.program[valueAt] = replaceWith;
        }

        public IntcodeMachine(List<long> program, List<long> input = null)
        {
            this.program = program;
            if (input != null)
            {
                this.input = input;
            }
        }

        public void AddInput(long input)
        {
            if (this.input != null)
                this.input.Add(input);
        }

        public void FeedJustOneTypeOfInputRepeatedly(long input)
        {
            this.input = Enumerable.Repeat(input, 1000).ToList();
            this.inputPointer = 0;
        }

        public List<long> GetStateAfterRun(out List<long> output, out bool paused)
        {
            output = this.IterateProgram(out paused);
            return program;
        }

        private long GetAt(int p, ParameterMode mode)
        {
            switch (mode)
            {
                case ParameterMode.Immediate:
                    return program[pointer + p];
                case ParameterMode.Position:
                    return program[(int)program[pointer + p]];
                case ParameterMode.Relative:
                    return program[relativeBase + (int)program[pointer + p]];
                default:
                    throw new Exception("Unknown Parameter Mode");
            }
        }

        private void SetAt(int at, ParameterMode mode, long value)
        {
            switch (mode)
            {
                case ParameterMode.Immediate:
                    program[pointer + at] = value;
                    break;
                case ParameterMode.Position:
                    program[(int)program[pointer + at]] = value;
                    break;
                case ParameterMode.Relative:
                    program[relativeBase + (int)program[pointer + at]] = value;
                    break;
                default:
                    throw new Exception("Unknown Parameter mode");
            }
        }

        private void Add(Operation op) =>
            SetAt(3, op.ModeParam3,
                GetAt(1, op.ModeParam1) + GetAt(2, op.ModeParam2)
            );

        private void Multiply(Operation op) =>
            SetAt(3, op.ModeParam3,
                GetAt(1, op.ModeParam1) * GetAt(2, op.ModeParam2)
            );

        private void Set(Operation op)
        {
            int position = op.ModeParam1 == ParameterMode.Relative ?
                relativeBase + (int)program[pointer + 1] :
                (int)program[pointer + 1];

            if (this.input != null)
            {
                program[position] = input[inputPointer];
                inputPointer++;
            }
        }

        private class Operation
        {
            public OpCode OpCode { get; private set; }
            public ParameterMode ModeParam1 { get; private set; }
            public ParameterMode ModeParam2 { get; private set; }
            public ParameterMode ModeParam3 { get; private set; }

            public Operation(string value)
            {
                var len = value.Length;
                this.OpCode = (OpCode)int.Parse(
                    value.Substring(len - 2 < 0 ? 0 : len - 2, len > 1 ? 2 : 1));
                this.ModeParam1 = len >= 3 ? (ParameterMode)int.Parse(value.Substring(len - 3, 1)) : ParameterMode.Position;
                this.ModeParam2 = len >= 4 ? (ParameterMode)int.Parse(value.Substring(len - 4, 1)) : ParameterMode.Position;
                this.ModeParam3 = len >= 5 ? (ParameterMode)int.Parse(value.Substring(len - 5, 1)) : ParameterMode.Position;
            }
        }

        private List<long> IterateProgram(out bool pause)
        {
            List<long> output = new List<long>();
            var stop = false;
            pause = false;
            var maxLength = program.Count();

            while (pointer < maxLength && !stop)
            {
                var operation = new Operation(program[pointer].ToString());
                switch (operation.OpCode)
                {
                    case OpCode.Add:
                        Add(operation);
                        pointer += 4;
                        break;
                    case OpCode.Multiply:
                        Multiply(operation);
                        pointer += 4;
                        break;
                    case OpCode.SaveAt:
                        Set(operation);
                        pointer += 2;
                        break;
                    case OpCode.Output:
                        output.Add(GetAt(1, operation.ModeParam1));
                        pointer += 2;
                        pause = true;
                        break;
                    case OpCode.Terminate:
                        stop = true;
                        pointer += 4;
                        break;
                    case OpCode.JumpIfTrue:
                        if (GetAt(1, operation.ModeParam1) != 0)
                            pointer = (int)GetAt(2, operation.ModeParam2);
                        else
                            pointer += 3;
                        break;
                    case OpCode.JumpIfFalse:
                        if (GetAt(1, operation.ModeParam1) == 0)
                            pointer = (int)GetAt(2, operation.ModeParam2);
                        else
                            pointer += 3;
                        break;
                    case OpCode.LessThan:
                        SetAt(3, operation.ModeParam3,
                            (GetAt(1, operation.ModeParam1) < GetAt(2, operation.ModeParam2) ? 1 : 0));
                        pointer += 4;
                        break;
                    case OpCode.Equals:
                        SetAt(3, operation.ModeParam3,
                            (GetAt(1, operation.ModeParam1) == GetAt(2, operation.ModeParam2) ? 1 : 0));
                        pointer += 4;
                        break;
                    case OpCode.AdjustRelativeBase:
                        this.relativeBase += (int)GetAt(1, operation.ModeParam1);
                        pointer += 2;
                        break;
                    default:
                        throw new Exception("Invalid opcode " +
                            program[pointer] + " at pointer " + pointer);
                }
                if (stop || pause)
                    break;
            }
            return output;
        }
    }
}