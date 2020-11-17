using Crypto;
using System.Linq;
using System.Collections.Generic;

namespace ModeWork
{
    public class ECB: ICoder
    {
        private ICoderBlok _algorithm;

        public ECB(ICoderBlok algorithm)
        {
            _algorithm = algorithm;
        }

        public byte[] Encode(byte[] message)
        {
            var c = new List<byte>();

            for (int i = 0; i < message.Length; i+=_algorithm.Size)
                c.AddRange(_algorithm.EncodeBlok(message.Skip(i).Take(_algorithm.Size).ToArray()));

            return c.ToArray();
        }

        public byte[] Decode(byte[] bloks)
        {
            var m = new List<byte>();

            for (int i = 0; i < bloks.Length; i += _algorithm.Size)
                m.AddRange(_algorithm.DecodeBlok(bloks.Skip(i).Take(_algorithm.Size).ToArray()));

            return m.ToArray();
        }
    }
}
