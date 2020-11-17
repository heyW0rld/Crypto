namespace ModeWork
{
    public interface ICoderBlok
    {
        public int Size { get; }
        byte[] EncodeBlok(byte[] message);
        byte[] DecodeBlok(byte[] message);
    }
}
