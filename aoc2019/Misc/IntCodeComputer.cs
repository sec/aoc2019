using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace aoc2019.Misc
{
    public class IntCodeComputer
    {
        private readonly long[] _ram, _rom;
        private readonly Queue<long> _inputs;
        private long _pointer;
        private bool _running;

        public IntCodeComputer(IEnumerable<long> code)
        {
            _inputs = new Queue<long>();
            _ram = new long[1024 * 5];
            _rom = new long[_ram.Length];
            Array.Copy(code.ToArray(), 0, _ram, 0, code.Count());
            Array.Copy(_ram, 0, _rom, 0, _ram.Length);
        }

        public void Reset()
        {
            Array.Copy(_rom, 0, _ram, 0, _ram.Length);
        }

        public long this[int index]
        {
            get => _ram[index];
            set => _ram[index] = value;
        }

        public void AddInput(long input) => _inputs.Enqueue(input);
        public event Action<long> SignalOutput;
        public event Func<long> ProvideInput;

        enum OpCode
        {
            Halt = 99,
            Add = 1,
            Mul = 2,
            Input = 3,
            Output = 4,
            JIT = 5,
            JIF = 6,
            LT = 7,
            EQ = 8,
            PTR = 9
        }

        public void Run()
        {
            long index = 0;
            _pointer = 0;
            _running = true;

            while (_running)
            {
                var instr = _ram[index].ToString().PadLeft(5, '0');

                var opcode = (OpCode) int.Parse(instr.Substring(3, 2));
                var mode1 = int.Parse(instr[2].ToString());
                var mode2 = int.Parse(instr[1].ToString());
                var mode3 = int.Parse(instr[0].ToString());
                var jump = 0;

                switch (opcode)
                {
                    case OpCode.Halt:
                        return;

                    case OpCode.Add:
                        SetParam(GetIndex(index + 3, mode3), GetParam(index + 1, mode1) + GetParam(index + 2, mode2));
                        jump = 4;
                        break;

                    case OpCode.Mul:
                        SetParam(GetIndex(index + 3, mode3), GetParam(index + 1, mode1) * GetParam(index + 2, mode2));
                        jump = 4;
                        break;

                    case OpCode.Input:
                        while (_inputs.Count == 0)
                        {
                            if (ProvideInput != null)
                            {
                                AddInput(ProvideInput());
                            }
                            else
                            {
                                Thread.Sleep(10);
                            }
                        }
                        SetParam(GetIndex(index + 1, mode1), _inputs.Dequeue());
                        jump = 2;
                        break;

                    case OpCode.Output:
                        SignalOutput(GetParam(index + 1, mode1));
                        jump = 2;
                        break;

                    case OpCode.JIT:
                        jump = 3;
                        if (GetParam(index + 1, mode1) != 0)
                        {
                            jump = 0;
                            index = GetParam(index + 2, mode2);
                        }
                        break;

                    case OpCode.JIF:
                        jump = 3;
                        if (GetParam(index + 1, mode1) == 0)
                        {
                            jump = 0;
                            index = GetParam(index + 2, mode2);
                        }
                        break;

                    case OpCode.LT:
                        jump = 4;
                        SetParam(GetIndex(index + 3, mode3), GetParam(index + 1, mode1) < GetParam(index + 2, mode2) ? 1 : 0);
                        break;

                    case OpCode.EQ:
                        jump = 4;
                        SetParam(GetIndex(index + 3, mode3), GetParam(index + 1, mode1) == GetParam(index + 2, mode2) ? 1 : 0);
                        break;

                    case OpCode.PTR:
                        _pointer += GetParam(index + 1, mode1);
                        jump = 2;
                        break;

                    default:
                        throw new NotImplementedException();
                }

                index += jump;
            }
        }

        private long GetParam(long index, long mode)
        {
            var val = _ram[index];

            return mode switch
            {
                0 => _ram[val], // position
                1 => val, // immediate
                2 => _ram[val + _pointer],
                _ => throw new NotImplementedException(),
            };
        }

        private void SetParam(long index, long value) => _ram[index] = value;

        private long GetIndex(long index, long mode)
        {
            var val = _ram[index];

            return mode switch
            {
                0 => val,
                2 => val + _pointer,
                _ => throw new NotImplementedException(),
            };
        }

        public void Terminate() => _running = false;
    }
}