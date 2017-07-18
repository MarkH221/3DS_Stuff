using System;
using System.IO;

namespace OMASSumma
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                if (!File.Exists(args[0])) return;
                UInt32 sum = 0;
                using (var o = new FileStream(args[0], FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
                {
                    o.Position = 0x10;
                    byte[] buf = new byte[4];
                    for (int i = 0; i < o.Length - 0x10; i += 4)
                    {
                        o.Read(buf, 0, 4);
                        sum += BitConverter.ToUInt32(buf, 0);
                    }
                    Console.WriteLine("New Checksum: {0}", sum.ToString("X8"));
                    o.Position = 0xc;
                    o.Write(BitConverter.GetBytes(sum), 0, 4);
                }
                Console.Beep();
            }
            catch (Exception)
            {
                Console.Beep();
            }
        }
    }
}