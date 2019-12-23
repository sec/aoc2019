using aoc2019.Misc;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace aoc2019.Code
{
    public class Day23 : BaseDay
    {
        const int N = 50;
        private long _part1 = 0, _part2 = 0;

        public Day23() : base(23)
        {
            Network();
        }

        class Packet
        {
            public long IP, X, Y;
            public bool SentX;
        }

        void Network()
        {
            var tasks = new List<Task>();
            using var abort = new ManualResetEvent(false);

            var lan = new IntCodeComputer[N];
            var hub = new ConcurrentDictionary<long, List<Packet>>();
            var buf = new ConcurrentDictionary<long, List<long>>();
            var init = new bool[N];

            for (int i = 0; i < N; i++)
            {
                var ip = i;
                var c = new IntCodeComputer(ReadAllText().ToIntCode());

                c.SignalOutput += _output =>
                {
                    if (abort.WaitOne(10))
                    {
                        c.Terminate();
                    }

                    buf[ip].Add(_output);
                    if (buf[ip].Count == 3)
                    {
                        var packet = new Packet()
                        {
                            IP = buf[ip][0],
                            X = buf[ip][1],
                            Y = buf[ip][2],
                            SentX = false
                        };

                        if (packet.IP == 255 && _part1 == 0)
                        {
                            _part1 = packet.Y;
                        }

                        lock (this)
                        {
                            hub[packet.IP].Add(packet);
                        }
                        buf[ip].Clear();
                    }
                };
                c.ProvideInput += () =>
                {
                    if (abort.WaitOne(10))
                    {
                        c.Terminate();
                    }

                    if (init[ip])
                    {
                        if (hub[ip].Any())
                        {
                            Packet p;
                            lock (this)
                            {
                                p = hub[ip][0];
                                if (p.SentX)
                                {
                                    hub[ip].RemoveAt(0);
                                }
                            }
                            if (p.SentX)
                            {
                                return p.Y;
                            }
                            else
                            {
                                p.SentX = true;

                                return p.X;
                            }
                        }
                        return -1;
                    }
                    else
                    {
                        init[ip] = true;

                        return ip;
                    }
                };

                lan[i] = c;
                buf[i] = new List<long>();
                hub[i] = new List<Packet>();
                hub[255] = new List<Packet>();

                tasks.Add(new Task(c.Run));
            }
            tasks.Add(new Task(NAT));

            tasks.ForEach(x => x.Start());
            Task.WhenAll(tasks).ConfigureAwait(false).GetAwaiter().GetResult();

            void NAT()
            {
                while (!init.All(x => x))
                {
                    Thread.Sleep(100);
                }
                var sent = new DefaultDict<long, int>();

                while (true)
                {
                    Thread.Sleep(100);
                    List<(long Key, int Count)> queues;

                    lock (this)
                    {
                        queues = hub.Where(kv => kv.Key != 255).Select(kv => (kv.Key, kv.Value.Count)).ToList();
                    }

                    if (queues.All(item => item.Count == 0) && hub[255].Any())
                    {
                        Packet last;
                        lock (this)
                        {
                            last = hub[255].Last();
                            hub[255].Clear();
                        }
                        sent[last.Y]++;

                        if (sent[last.Y] == 2)
                        {
                            _part2 = last.Y;
                            abort.Set();
                            
                            return;
                        }

                        lock (this)
                        {
                            hub[0].Add(last);
                        }
                    }
                }
            }
        }

        public override string Part1() => _part1.ToString();

        public override string Part2() => _part2.ToString();
    }
}