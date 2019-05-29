using ImageMagick;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace WDSE.Helpers
{
    public static class Adapter
    {

        public static MagickGeometry ToMagickGeometry(this Rectangle rectangle )
        {
            return new MagickGeometry(
                x: rectangle.X,
                y: rectangle.Y,
                width: rectangle.Width,
                height: rectangle.Height
                );
        }

    }
}
