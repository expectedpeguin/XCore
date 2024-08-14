namespace XCore.Client.Resources.Helpers
{
    public class XColors
    {
        public byte R { get; }
        public byte G { get; }
        public byte B { get; }
        public byte A { get; }

        public XColors(byte r, byte g, byte b, byte a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public static XColors White => new XColors(255, 255, 255, 255);
        public static XColors Gray => new XColors(128, 128, 128, 255);
    }
}