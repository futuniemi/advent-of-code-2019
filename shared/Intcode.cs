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
        Terminate = 99
    }

    public enum ParameterMode
    {
        Position = 0,
        Immediate = 1
    }

    public class IntcodeMachine
    {
        private int pointer = 0;
        private List<int> program;
        private int? input;

        public void Modify(int valueAt, int replaceWith)
        {
            this.program[valueAt] = replaceWith;
        }

        public IntcodeMachine(List<int> program, int? input = null)
        {
            this.program = program;
            this.input = input;
        }

        public List<int> GetStateAfterRun()
        {
            this.IterateProgram();
            return program;
        }

        private int GetAt(int p, ParameterMode mode) =>
            mode == ParameterMode.Immediate ?
            program[pointer + p] :
            program[program[pointer + p]];

        private int SetAt(int at, ParameterMode mode, int value) =>
            mode == ParameterMode.Immediate ?
            program[pointer + at] = value :
            program[program[pointer + at]] = value;

        private void Add(Operation op) =>
            SetAt(3, op.ModeParam3,
                GetAt(1, op.ModeParam1) + GetAt(2, op.ModeParam2)
            );

        private void Multiply(Operation op) =>
             SetAt(3, op.ModeParam3,
                GetAt(1, op.ModeParam1) * GetAt(2, op.ModeParam2)
             );

        private void Set(int position)
        {
            if (this.input != null)
            {
                program[position] = input.Value;
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

        private void IterateProgram()
        {
            var stop = false;
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
                        Set(program[pointer + 1]);
                        pointer += 2;
                        break;
                    case OpCode.Output:
                        Console.WriteLine(GetAt(1, operation.ModeParam1));
                        pointer += 2;
                        break;
                    case OpCode.Terminate:
                        stop = true;
                        pointer += 4;
                        break;
                    case OpCode.JumpIfTrue:
                        if (GetAt(1, operation.ModeParam1) != 0)
                            pointer = GetAt(2, operation.ModeParam2);
                        else
                            pointer += 3;
                        break;
                    case OpCode.JumpIfFalse:
                        if (GetAt(1, operation.ModeParam1) == 0)
                            pointer = GetAt(2, operation.ModeParam2);
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
                    default:
                        throw new Exception("Invalid opcode " +
                            program[pointer] + " at pointer " + pointer);
                }
                if (stop)
                    break;
            }
        }
    }
}