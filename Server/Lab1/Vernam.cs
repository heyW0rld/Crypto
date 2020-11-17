namespace Crypto.Lab1
{
    public class Vernam : ICoder
    {
        byte[] _key;

        public Vernam(byte[] key)
        {
            _key = key;
        }

        public byte[] Encode(byte[] data)
        {
            byte[] result = new byte[data.Length];

            for (int i = 0; i < data.Length; i++)
                result[i] = (byte)(data[i] ^ _key[i]);

            return result;
        }

        public byte[] Decode(byte[] data)
        {
            return Encode(data);
        }
    }
}
