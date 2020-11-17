namespace Crypto
{
    public interface ICoder
    {
        byte[] Encode(byte[] message);
        byte[] Decode(byte[] message);
    }
}
