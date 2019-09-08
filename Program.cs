using System;
using System.IO;
using System.Drawing;
using ImageFilters.ImageUtils;

namespace ImageFilters
{
    class Program
    {
        static void Main(string[] args)
        {
            String filepath = "img/";

            // Apply grayscale filter
            Filters filters = new Filters();

            // Grayscale
            Image grayscale = filters.GrayscaleFilter(Path.GetFullPath(filepath + "original.png"));
            filters.SaveImage(grayscale, Path.GetFullPath(filepath + "grayscale.png"));
            grayscale.Dispose();

            // Halftone
            Image halftone = filters.HalftoneFilter(Path.GetFullPath(filepath + "original.png"));
            filters.SaveImage(halftone, Path.GetFullPath(filepath + "halftone.png"));
            halftone.Dispose();

            // Dithering
            Image dither = filters.DitherFilter(Path.GetFullPath(filepath + "original.png"));
            filters.SaveImage(dither, Path.GetFullPath(filepath + "dither.png"));
            dither.Dispose();
        }
    }
}
