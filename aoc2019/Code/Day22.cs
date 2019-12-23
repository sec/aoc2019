using aoc2019.Misc;
using System;
using System.Linq;
using System.Numerics;

namespace aoc2019.Code
{
    public class Day22 : BaseDay
    {
        public class Deck
        {
            private readonly int _count;
            private int[] _cards, _table;

            public Deck(int n)
            {
                _count = n;
                _cards = Enumerable.Range(0, n).ToArray();
                _table = new int[n];
            }

            public int[] Cards => _cards.ToArray();
            public int CardAt(int n) => _cards[n];

            void Swap()
            {
                var t = _cards;
                _cards = _table;
                _table = t;
            }

            public void DealNew()
            {
                for (var i = 0; i < _count; i++)
                {
                    _table[_count - i - 1] = _cards[i];
                }
                Swap();
            }

            public void Cut(int n)
            {
                if (n > 0)
                {
                    Array.Copy(_cards, 0, _table, _count - n, n);
                    Array.Copy(_cards, n, _table, 0, _count - n);
                }
                else
                {
                    Array.Copy(_cards, _count + n, _table, 0, n * -1);
                    Array.Copy(_cards, 0, _table, (n * -1), _count + n);
                }
                Swap();
            }

            public void Deal(int n)
            {
                for (int i = 0; i < _count; i++)
                {
                    _table[i * n % _count] = _cards[i];
                }
                Swap();
            }
        }

        public Day22() : base(22)
        {
        }

        void Shuffle(Deck deck)
        {
            foreach (var line in ReadAllLines())
            {
                if (line == "deal into new stack")
                {
                    deck.DealNew();
                }
                else
                {
                    var s = line.Split(' ');
                    var n = int.Parse(s.Last());
                    if (s[0] == "deal")
                    {
                        deck.Deal(n);
                    }
                    else
                    {
                        deck.Cut(n);
                    }
                }
            }
        }

        public override string Part1()
        {
            var deck = new Deck(10007);
            Shuffle(deck);

            return deck.Cards.ToList().IndexOf(2019).ToString();
        }

        public override string Part2()
        {
            // too hard - based on https://topaz.github.io/paste/#XQAAAQDbCAAAAAAAAAARiEJHiiMzw3cPM/1Vl+2nx/DqKkM2yi+HVdpp+qLh9Jwh+ZECcFH/z2ezeBhLAAlZkhU0of3X0pFe6IC5Pt8zg+VNLW+7Hk/5EV5YpqOp6CSqtGyVPuSZaaoRLb7LS9oSqXoOJ/1joxUBAhkJcQvg07AIO5qtpviCy3hmvbmUKkGpJCyUBuCAJxm5+D5dys7Df5zKP6yNPHru9X91OmoufXhhi6kh2VywYD+wXQr44lxaXITD51KaaRa9VL3swZs0AP+RmWm3ptN717QGopPq2DzQJlu6F+YEfCHXb0J2mXftgPNdPnQhmV62/u+FcBFo+Fh4nNcmMwIQ97o736Ze/1kpuZoUoAW0pZtoITllJRYQ7nnF2vyqlblAT1eeg1CWtRQazBy+XZiPEmvvgYjYNgtI8C6pr/xfh/dIgWTJakJ7jxOKvehmqRLFofjxc+xxVURjkqOYAwMVaJRlN2hhu+d9LYWiJk5wqPW60coXFfhSU6QyRNqHWgSOkfisJ+qVUsI0z2eLeWkZtJHYELSUrRGHwXyBOIMqpTZVIEN5C4h+Du4upTtIzNe1OHn28CO6bgkC121maLN7idJD/rMtDilJjBmYI+/WITAyW+oBsN/3InB0fQSou+Tznl9e99m1hhDxLFSFAbPx5PSK3XBqIMpr1AIbeDZ0jMXFDE1qKjEJ9Ru0W1W5iK+Ffx5tcKTbpdAeqkHXruc0J6TGcwUdnoNPsz+QxLke1zt+O89z/PhnvIlkXS/tO17RJKCdL2Jf60Io+6d377cHi8znL19z7m1lTMvp67bP7nPHzQaw0zeAXbNpGdm6v+ZHIRJ0DR8WE7daQzcQKRFXbTnJe0yJji4hpf1EF3LxkTEjLeLuq2yxtw9e4wEguBgUVyK+YhwrJBoj2X+RGCh96cakHGuhY24z0xk0An1JtBNypGpnPntS4omSrBb8VsLHxziD/rNrxh34UUOI3cjek1z1Hu+seKrP3IJMdR6zQvistqcFD2/mJAMvQcpllWKwjrFdZ0Wap8w9LHFLMWUEPDOgv7nNB8OdvsFr7wO/5PWNlKgnReA/jLW/1iathfe5ADqzd5wmDFk6RTWMasO1+b4CFEEDM1sjq78KjxkF0hsgMS0wA/RuK5yGt14QIwqvFoTRVgNn3+/nCqWkGLnaziBLTF5F2fB0AZLmeL4vq5UybptsKVaxtpXOlROAjAgIs2GW6GnDVjlnwOR+vV5GeULzRvqSP30b1JShOLYGb+mp2Me0fdS6gQv90uqkA8sv1ZdQ/477AYAPBPDJ+10xIGcPzWItPpk0mhuf6ec0XhTp9wqrjjekK4sHYP64S1447X/nVWmDWFRx1//49ZZb
            var pos = new BigInteger(2020);
            var size = new BigInteger(119315717514047);
            var iter = new BigInteger(101741582076661);

            var offsetDiff = new BigInteger(0);
            var incrMul = new BigInteger(1);

            foreach (var line in ReadAllLines())
            {
                if (line == "deal into new stack")
                {
                    incrMul *= -1;
                    offsetDiff += incrMul;
                }
                else
                {
                    var s = line.Split(' ');
                    var n = int.Parse(s.Last());
                    if (s[0] == "deal")
                    {
                        // deal n                        
                        incrMul *= BigInteger.ModPow(n, size - 2, size);
                    }
                    else
                    {
                        // cut n
                        offsetDiff += n * incrMul;
                    }
                }

                incrMul %= size;
                offsetDiff %= size;
            }

            var incr = BigInteger.ModPow(incrMul, iter, size);
            var offset = (offsetDiff * (1 - incr) * BigInteger.ModPow((1 - incrMul) % size, size - 2, size)) % size;

            return ((offset + pos * incr) % size).ToString();
        }
    }
}