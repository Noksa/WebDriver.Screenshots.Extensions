using System.Collections.Generic;
using ImageMagick;
using System.Drawing;
using System.Linq;

namespace WDSE.Compare
{
    public static class WdseImageComparer
    {
        public static byte[] CompareAndGetImage(Bitmap image1, Bitmap image2)
        {
            var list = CompareImages(new MagickImage(image1), new MagickImage(image2)).ToList();
            var arr = ((IMagickImage)list[1]).ToByteArray();
            return arr;
        }

        public static byte[] CompareAndGetImage(string pathToImage1, string pathToImage2)
        {
            var list = CompareImages(new MagickImage(pathToImage1), new MagickImage(pathToImage2)).ToList();
            var arr = ((IMagickImage) list[1]).ToByteArray();
            return arr;
        }

        public static byte[] CompareAndGetImage(IMagickImage image1, IMagickImage image2)
        {
            var list = CompareImages(image1, image2).ToList();
            var arr = ((IMagickImage)list[1]).ToByteArray();
            return arr;
        }

        public static bool Compare(IMagickImage image1, IMagickImage image2)
        {
            var list = CompareImages(image1, image2).ToList();
            var res = (double) list[0];
            return !(res > 0);
        }

        public static bool Compare(Bitmap image1, Bitmap image2)
        {
            var list = CompareImages(new MagickImage(image1), new MagickImage(image2)).ToList();
            var res = (double) list[0];
            return !(res > 0);
        }

        public static bool Compare(string pathToImage1, string pathToImage2)
        {
            var list = CompareImages(new MagickImage(pathToImage1), new MagickImage(pathToImage2)).ToList();
            var res = (double) list[0];
            return !(res > 0);
        }


        private static IEnumerable<object> CompareImages(IMagickImage image1, IMagickImage image2)
        {
            using (image1)
            {
                using (image2)
                {
                    using (var imgWithDiff = new MagickImage())
                    {
                        var compareSettings = new CompareSettings
                        {
                            HighlightColor = new MagickColor(Color.Red),
                            Metric = ErrorMetric.Absolute
                        };
                        image1.ColorFuzz = new Percentage(3);
                        var doubleRes = image1.Compare(image2, compareSettings, imgWithDiff);
                        yield return doubleRes;
                        yield return new MagickImage(imgWithDiff);
                    }
                }
            }
        }
    }
}