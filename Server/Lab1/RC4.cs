using System.Linq;

namespace Crypto.Lab1
{
    public class RC4 : ICoder
    {
        byte[] S;
        int _x = 0;
        int _y = 0;
        int _size;

        public RC4(byte[] key)
        {
            S = new byte[256];
            _size = key.Length;

            int keyLength = key.Length;

            for (int i = 0; i < 256; i++)
            {
                S[i] = (byte)i;
            }

            int j = 0;
            for (int i = 0; i < 256; i++)
            {
                j = (j + S[i] + key[i % keyLength]) % 256;
                (S[i], S[j]) = (S[j], S[i]);
            }
        }

        private byte KeyItem()
        {
            _x = (_x + 1) % 256;
            _y = (_y + S[_x]) % 256;

            (S[_x], S[_y]) = (S[_y], S[_x]);

            return S[(S[_x] + S[_y]) % 256];
        }

        public byte[] Encode(byte[] dataB)
        {
            byte[] data = dataB.Take(_size).ToArray();

            byte[] cipher = new byte[data.Length];

            for (int m = 0; m < data.Length; m++)
            {
                cipher[m] = (byte)(data[m] ^ KeyItem());
            }

            return cipher;
        }

        public byte[] Decode(byte[] dataB)
        {
            return Encode(dataB);
        }
    }
}

