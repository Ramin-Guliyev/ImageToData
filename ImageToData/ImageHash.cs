using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ImageToData
{
    public static class ImageHash
    {
        private static byte[] bitCounts = new byte[256]
        {
        0, 1, 1, 2, 1, 2, 2, 3, 1, 2,
        2, 3, 2, 3, 3, 4, 1, 2, 2, 3,
        2, 3, 3, 4, 2, 3, 3, 4, 3, 4,
        4, 5, 1, 2, 2, 3, 2, 3, 3, 4,
        2, 3, 3, 4, 3, 4, 4, 5, 2, 3,
        3, 4, 3, 4, 4, 5, 3, 4, 4, 5,
        4, 5, 5, 6, 1, 2, 2, 3, 2, 3,
        3, 4, 2, 3, 3, 4, 3, 4, 4, 5,
        2, 3, 3, 4, 3, 4, 4, 5, 3, 4,
        4, 5, 4, 5, 5, 6, 2, 3, 3, 4,
        3, 4, 4, 5, 3, 4, 4, 5, 4, 5,
        5, 6, 3, 4, 4, 5, 4, 5, 5, 6,
        4, 5, 5, 6, 5, 6, 6, 7, 1, 2,
        2, 3, 2, 3, 3, 4, 2, 3, 3, 4,
        3, 4, 4, 5, 2, 3, 3, 4, 3, 4,
        4, 5, 3, 4, 4, 5, 4, 5, 5, 6,
        2, 3, 3, 4, 3, 4, 4, 5, 3, 4,
        4, 5, 4, 5, 5, 6, 3, 4, 4, 5,
        4, 5, 5, 6, 4, 5, 5, 6, 5, 6,
        6, 7, 2, 3, 3, 4, 3, 4, 4, 5,
        3, 4, 4, 5, 4, 5, 5, 6, 3, 4,
        4, 5, 4, 5, 5, 6, 4, 5, 5, 6,
        5, 6, 6, 7, 3, 4, 4, 5, 4, 5,
        5, 6, 4, 5, 5, 6, 5, 6, 6, 7,
        4, 5, 5, 6, 5, 6, 6, 7, 5, 6,
        6, 7, 6, 7, 7, 8
        };

        private static uint BitCount(ulong num)
        {
            uint num2 = 0u;
            while (num != 0)
            {
                num2 += bitCounts[num & 0xFF];
                num >>= 8;
            }
            return num2;
        }

        public static ulong AverageHash(Image image)
        {
            Bitmap bitmap = new Bitmap(8, 8, PixelFormat.Format32bppRgb);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.DrawImage(image, 0, 0, 8, 8);
            byte[] array = new byte[64];
            uint num = 0u;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    uint num2 = (uint)bitmap.GetPixel(j, i).ToArgb();
                    uint num3 = (num2 & 0xFF0000) >> 16;
                    num3 += (num2 & 0xFF00) >> 8;
                    num3 += num2 & 0xFF;
                    num3 /= 12u;
                    array[j + i * 8] = (byte)num3;
                    num += num3;
                }
            }
            num /= 64u;
            ulong num4 = 0uL;
            for (int k = 0; k < 64; k++)
            {
                if (array[k] >= num)
                {
                    num4 |= (ulong)(1L << 63 - k);
                }
            }
            return num4;
        }

        public static ulong AverageHash(string path)
        {
            return AverageHash(new Bitmap(path));
        }

        public static double Similarity(ulong hash1, ulong hash2)
        {
            return (double)((64 - BitCount(hash1 ^ hash2)) * 100) / 64.0;
        }

        public static double Similarity(Image image1, Image image2)
        {
            ulong hash = AverageHash(image1);
            ulong hash2 = AverageHash(image2);
            return Similarity(hash, hash2);
        }

        public static double Similarity(string path1, string path2)
        {
            ulong hash = AverageHash(path1);
            ulong hash2 = AverageHash(path2);
            return Similarity(hash, hash2);
        }
    }
}
