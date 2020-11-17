using System.Collections.Generic;
using System.Linq;
using Crypto;

namespace ModeWork
{ 
    public class CBC : ICoder
    {
        private ICoderBlok _algorithm;
        private byte[] _c0;

        public CBC(ICoderBlok algorithm, byte[] c0)
        {
            _c0 = c0;
            _algorithm = algorithm;
        }

        public byte[] Encode(byte[] message)
        {
            var m = (byte[])message.Clone();
            var c = new List<byte>();

            var prev = _c0;
            for (int i = 0; i < message.Length; i += _algorithm.Size)
            {
                for(int j = 0; j < _algorithm.Size; j++)
                    m[i+j] ^= prev[j];

                c.AddRange(_algorithm.EncodeBlok(message.Skip(i).Take(_algorithm.Size).ToArray()));
                prev = c.Skip(i).Take(_algorithm.Size).ToArray();
            }

            return c.ToArray();
        }

        public byte[] Decode(byte[] code)
        {
            var c = (byte[])code.Clone();
            var m = new List<byte>();
            

            var prev = _c0;
            for (int i = 0; i < code.Length; i += _algorithm.Size)
            {
                for (int j = 0; j < _algorithm.Size; j++)
                    c[i + j] ^= prev[j];

                m.AddRange(_algorithm.DecodeBlok(code.Skip(i).Take(_algorithm.Size).ToArray()));
                prev = m.Skip(i).Take(_algorithm.Size).ToArray();
            }

            return m.ToArray();
        }
    }
}
