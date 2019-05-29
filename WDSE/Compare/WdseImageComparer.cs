using ImageMagick;
using System.Collections.Generic;
using System.Linq;

namespace WDSE.Compare
{
    /// <summary>
    ///     Images comparer class
    /// </summary>
    public static class WdseImageComparer
    {
        #region Privates

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
                            HighlightColor = new MagickColor(MagickColor.FromRgb(255,0,0)),
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

        #endregion

        #region Compare and get image

        ///// <summary>
        /////     Compares two images and returns an image which show the difference between.
        ///// </summary>
        ///// <param name="image1">Image #1 to compare</param>
        ///// <param name="image2">Image #2 to compare</param>
        ///// <returns></returns>
        //public static byte[] CompareAndGetImage(Bitmap image1, Bitmap image2)
        //{
        //    var list = CompareImages(new MagickImage(image1), new MagickImage(image2)).ToList();
        //    var arr = ((IMagickImage) list[1]).ToByteArray();
        //    return arr;
        //}

        /// <summary>
        ///     Compares two images and returns an image which show the difference between.
        /// </summary>
        /// <param name="pathToImage1">Path to Image #1 to compare</param>
        /// <param name="pathToImage2">Path to Image #2 to compare</param>
        /// <returns></returns>
        public static byte[] CompareAndGetImage(string pathToImage1, string pathToImage2)
        {
            var list = CompareImages(new MagickImage(pathToImage1), new MagickImage(pathToImage2)).ToList();
            var arr = ((IMagickImage) list[1]).ToByteArray();
            return arr;
        }

        /// <summary>
        ///     Compares two images and returns an image which show the difference between.
        /// </summary>
        /// <param name="image1">Image #1 to compare</param>
        /// <param name="image2">Image #2 to compare</param>
        /// <returns></returns>
        public static byte[] CompareAndGetImage(IMagickImage image1, IMagickImage image2)
        {
            var list = CompareImages(image1, image2).ToList();
            var arr = ((IMagickImage) list[1]).ToByteArray();
            return arr;
        }

        public static byte[] CompareAndGetImageIfNotSame(IMagickImage image1, IMagickImage image2)
        {
            var list = CompareImages(image1, image2).ToList();
            var res = (double) list[0];
            if (!(res > 0)) return null;
            var arr = ((IMagickImage) list[1]).ToByteArray();
            return arr;
        }

        #endregion

        #region Boolean compare

        /// <summary>
        ///     Compares two images. Returns true if images are same.
        /// </summary>
        /// <param name="image1">Image #1 to compare</param>
        /// <param name="image2">Image #2 to compare</param>
        /// <returns></returns>
        public static bool Compare(IMagickImage image1, IMagickImage image2)
        {
            var list = CompareImages(image1, image2).ToList();
            var res = (double) list[0];
            return !(res > 0);
        }

        ///// <summary>
        /////     Compares two images. Returns true if images are same.
        ///// </summary>
        ///// <param name="image1">Image #1 to compare</param>
        ///// <param name="image2">Image #2 to compare</param>
        ///// <returns></returns>
        //public static bool Compare(Bitmap image1, Bitmap image2)
        //{
        //    var list = CompareImages(new MagickImage(image1), new MagickImage(image2)).ToList();
        //    var res = (double) list[0];
        //    return !(res > 0);
        //}

        /// <summary>
        ///     Compares two images. Returns true if images are same.
        /// </summary>
        /// <param name="pathToImage1">Path to Image #1 to compare</param>
        /// <param name="pathToImage2">Path to Image #2 to compare</param>
        /// <returns></returns>
        public static bool Compare(string pathToImage1, string pathToImage2)
        {
            var list = CompareImages(new MagickImage(pathToImage1), new MagickImage(pathToImage2)).ToList();
            var res = (double) list[0];
            return !(res > 0);
        }

        #endregion
    }
}