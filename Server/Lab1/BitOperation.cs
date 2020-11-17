using System;
using System.Numerics;
using System.Text;
using System.Linq;

namespace Crypto.Lab1
{
    static public class BitOperation
    {
        static public BigInteger GetBit(BigInteger a, uint k)
        {
            return (a & (1 << (int)k)) >> (int)k;
        }

        static public BigInteger ChangeBit(BigInteger a, uint k)
        {
            return a ^ (BigInteger.One << (int)k);
        }

        static public BigInteger SwapBit(BigInteger a, uint i, uint j)
        {
            BigInteger bi = GetBit(a, i);
            BigInteger bj = GetBit(a, j);

            return (a & ~(1UL << (int)j) & ~(1UL << (int)i)) | (bi << (int)j) | (bj << (int)i);
        }

        static public BigInteger ZeroSmallBits(BigInteger a, uint m)
        {
            return a & ~((1UL << (int)m) - 1UL);
        }

        static public BigInteger GlueBits(BigInteger a, uint i, uint bitDepth)
        {
            BigInteger right = a & ((1UL << (int)i) - 1UL);
            BigInteger left = a & ZeroSmallBits((1UL << (int)bitDepth) - 1UL, bitDepth - i);

            return right | (left >> (int)(bitDepth - 2 * i));
        }

        static public BigInteger MidleBits(BigInteger a, uint i, uint bitDepth)
        {
            return (a & ZeroSmallBits(a & ((1UL << (int)(bitDepth - i + 1)) - 1UL), i)) >> (int)i;
        }

        static public BigInteger SwapBytes(BigInteger a, uint i, uint j)
        {
            BigInteger bi = (a & ((1UL << (int)(8 * (i + 1))) - 1)) >> (int)(8 * i);
            BigInteger bj = (a & ((1UL << (int)(8 * (j + 1))) - 1)) >> (int)(8 * j);
            a = a ^ (bi << (int)(i * 8));
            a = a ^ (bj << (int)(j * 8));

            return (a | (bi << (int)(j * 8)) | (bj << (int)(i * 8)));
        }

        static public BigInteger MaxDegreeBin(BigInteger n)
        {
            return n & -n;
        }

        static public BigInteger InsideDiapason(BigInteger x)
        {
            BigInteger p = 0;
            while (x > 1)
            {
                x >>= 1;
                p++;
            }

            return p;
        }

        static public BigInteger AutoXOR(BigInteger a, uint depthBit)
        {
            BigInteger result = 0;

            for (int i = 0; i < depthBit; i++)
            {
                result ^= GetBit(a, 1);
                a >>= 1;
            }

            return result;
        }

        static public BigInteger CycleShiftLeft(BigInteger x, uint p, uint n)
        {
            n = n - n / p * p;

            return ((x << (int)n) | (x >> (int)(p - n))) & ((1UL << (int)p) - 1UL);
        }

        static public BigInteger CycleShiftRight(BigInteger x, uint p, uint n)
        {
            n = n - n / p * p;

            return ((x >> (int)n) | (x << (int)(p - n))) & ((1UL << (int)p) - 1UL);
        }

        static public BigInteger TransposBits(BigInteger n, uint[] tranpos)
        {
            BigInteger result = 0;
            int i = 0;

            uint[] cloneTranspos = (uint[])tranpos.Clone();
            Array.Reverse(cloneTranspos);

            foreach (var item in cloneTranspos)
            {
                result |= (GetBit(n, item) << i);
                i++;
            }

            return result;
        }

        static public byte[] BytesXOR(byte[] first, byte[] second)
        {
            byte[] result = new byte[first.Length];

            for (int i = 0; i < first.Length; i++)
                result[i] = (byte)(first[i] ^ second[i]);

            return result;
        }

        static public BigInteger BinStrToBigInteger(string str)
        {
            if(str.Length == 0) throw new FormatException();

            BigInteger res = 0;
        
            foreach (char c in str)
            {
                if (c != '0' && c != '1')
                    throw new FormatException();

                res <<= 1;
                res += c == '1' ? 1 : 0;
            }

            return res;
        }

        static public string BigIntegerToBinStr(BigInteger number)
        {
            if (number == 0)
                return "0";

            var result = new StringBuilder();
            while (number != 0)
            {
                result.Insert(0, GetBit(number, 0) == 1 ? '1' : '0');
                number >>= 1;
            }

            return result.ToString();
        }
    }

}