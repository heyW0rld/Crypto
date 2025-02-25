﻿using System.Collections.Generic;
using System.Linq;
using Crypto;

namespace ModeWork
{
    public class OFB : ICoder
    {
        private ICoderBlok _algorithm;
        private byte[] _c0;

        public OFB(ICoderBlok algorithm, byte[] c0)
        {
            _c0 = c0;
            _algorithm = algorithm;
        }

        public byte[] Encode(byte[] message)
        {

            var m = (byte[])message.Clone();
            var c = new List<byte>();

            byte[] prev = _c0;

            for (int i = 0; i < message.Length; i += _algorithm.Size)
            {
                c.AddRange(_algorithm.EncodeBlok(prev));
                prev = c.Skip(i).Take(_algorithm.Size).ToArray();
                for (int j = 0; j < _algorithm.Size; j++)
                    c[i + j] ^= m[i + j];
            }

            return c.ToArray();
        }

        public byte[] Decode(byte[] code)
        {
            return Encode(code);
        }
    }
}
