using System.Collections.Generic;
using System.Linq;
using System;
using ModeWork;
using System.Numerics;

namespace Crypto.Lab1.DESAlgo
{
    public partial class DES : ICoderBlok
    {
        private ulong _key;

        public int Size => 8;

        public DES(ulong key)
        {
            _key = key;
        }

        public byte[] EncodeBlok(byte[] blok)
        {
            ulong ulBlok = BitConverter.ToUInt64(blok);
            ulong ip = Permute(ulBlok, IP);
            var schedule = KeySchedule(_key);

            var pair = new Pair
            {
                Left = ip & 0xFFFFFFFF00000000,
                Right = (ip & 0x00000000FFFFFFFF) << 32
            };

            for (int i = 0; i < 16; i++)
            {
                pair = new Pair
                {
                    Left = pair.Right,
                    Right = pair.Left ^ F(pair.Right, schedule[i + 1])
                };
            }

            ulong joined = pair.Right | (pair.Left >> 32);

            return BitConverter.GetBytes(Permute(joined, IPINV));
        }

        public byte[] DecodeBlok(byte[] blok)
        {
            ulong ulBlok = BitConverter.ToUInt64(blok);
            ulong ip = Permute(ulBlok, IP);
            var schedule = KeySchedule(_key);

            var pair = new Pair
            {
                Right = ip & 0xFFFFFFFF00000000,
                Left = (ip & 0x00000000FFFFFFFF) << 32
            };

            for (int i = 0; i < 16; i++)
            {
                pair = new Pair
                {
                    Right = pair.Left,
                    Left = pair.Right ^ F(pair.Left, schedule[i + 1])
                };
            }

            ulong joined = pair.Left | (pair.Right >> 32);


            return BitConverter.GetBytes(Permute(joined, IPINV));
        }

        private ulong Permute(ulong val, uint[] changes)
        {
            int[] cloneChanges = (int[])changes.Clone();
            Array.Reverse(cloneChanges);

            for (int i = 0; i < cloneChanges.Length; i++)
                cloneChanges[i]--;


            return TransposBits(val, cloneChanges);
        }

        private ulong Left28(ulong val)
        {
            return val & 0xFFFFFFF000000000;
        }

        private ulong Right28(ulong val)
        {
            return (val << 28) & 0xFFFFFFF000000000;
        }

        private ulong Concat56(ulong left, ulong right)
        {
            return (left & 0xFFFFFFF000000000) | ((right & 0xFFFFFFF000000000) >> 28);
        }

        private ulong LeftShift56(ulong val, uint count)
        {
            for (int i = 0; i < count; i++)
            {
                ulong msb = val & 0x8000000000000000;
                val = (val << 1) & 0xFFFFFFE000000000 | msb >> 27;
            }

            return val;
        }

        private List<byte> Split(ulong val)
        {
            var result = new List<byte>();

            for (int i = 0; i < 8; i++)
            {
                result.Add((byte)((val & 0xFC00000000000000) >> 56));

                val <<= 6;
            }

            return result;
        }

        private byte SBoxLookup(byte val, int table)
        {
            int index = ((val & 0x80) >> 2) | ((val & 0x04) << 2) | ((val & 0x78) >> 3);
            return SBoxes[table, index];
        }

        private List<ulong> KeySchedule(ulong key)
        {
            ulong p = Permute(key, PC1);
            ulong c = Left28(p);
            ulong d = Right28(p);

            var schedule = new List<Pair> { new Pair { Left = c, Right = d } };

            for (int i = 1; i <= LeftShifts.Count(); i++)
            {
                schedule.Add(new Pair
                {
                    Left = LeftShift56(schedule[i - 1].Left, LeftShifts[i - 1]),
                    Right = LeftShift56(schedule[i - 1].Right, LeftShifts[i - 1])
                });
            }

            var result = new List<ulong>();

            for (int i = 0; i < schedule.Count; i++)
            {
                ulong joined = Concat56(schedule[i].Left, schedule[i].Right);
                ulong permuted = Permute(joined, PC2);

                result.Add(permuted);
            }
            return result;
        }

        private ulong F(ulong right, ulong key)
        {
            ulong e = Permute(right, E);

            ulong x = e ^ key;

            var bs = Split(x);

            ulong boxLookup = 0;

            for (int i = 0; i < 8; i++)
            {
                boxLookup <<= 4;
                boxLookup |= SBoxLookup(bs[i], i);
            }

            boxLookup <<= 32;

            var result = Permute(boxLookup, P);

            return result;
        }

        struct Pair
        {
            public ulong Left;
            public ulong Right;
        }

        private ulong GetBit(ulong a, int k)
        {
            return (a & (1UL << k)) >> k;
        }

        private ulong TransposBits(ulong n, int[] tranpos)
        {
            ulong result = 0;
            int i = 0;

            int[] cloneTranspos = (int[])tranpos.Clone();
            Array.Reverse(cloneTranspos);

            foreach (var item in cloneTranspos)
            {
                result |= (GetBit(n, item) << i);
                i++;
            }

            return result;
        }
    }
}
