using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageFilters.ImageUtils
{
    public class Filters
    {
        #region BasicFilters
        /**
         * Dither uses solid colours to display images, 
         * and it is an old compression technique used by offset printing.  
         */
        public Image DitherFilter(string filepath) {
            // Load Image
            Image original = new Bitmap(filepath);

            // Image Size
            int width = original.Width;
            int height = original.Height;

            // Create new Image bitmap
            Image dither = CreateBlankImage(width, height);

            // Process each region into a dither pixel (2x2)
            for (int i = 0; i < width; i+=2) 
            {
                for (int j = 0; j < height; j+=2)
                {
                    // Get Pixels
                    Color p1 = GetPixelColor(original, i, j);
                    Color p2 = GetPixelColor(original, i, j + 1);
                    Color p3 = GetPixelColor(original, i + 1, j);
                    Color p4 = GetPixelColor(original, i + 1, j + 1);

                    // Colour Saturation by RGB channel
                    int red   = (p1.R + p2.R + p3.R + p4.R) / 4;
                    int green = (p1.G + p2.G + p3.G + p4.G) / 4;
                    int blue  = (p1.B + p2.B + p3.B + p4.B) / 4;

                    // Results by channel
                    int[] r = new int[]{0, 0, 0, 0};
                    int[] g = new int[]{0, 0, 0, 0};
                    int[] b = new int[]{0, 0, 0, 0};

                    // Get Quadrant Colour
                    for (int quadrant = 0; quadrant < 4; quadrant++) {
                        r[quadrant] = DitherSaturation(red, quadrant);
                        g[quadrant] = DitherSaturation(green, quadrant);
                        b[quadrant] = DitherSaturation(blue, quadrant);
                    }

                    // Set Dithered Colours
                    SetPixelColor(dither, Color.FromArgb(r[0], g[0], b[0]), i, j);
                    SetPixelColor(dither, Color.FromArgb(r[1], g[1], b[1]), i, j + 1);
                    SetPixelColor(dither, Color.FromArgb(r[2], g[2], b[2]), i + 1, j);
                    SetPixelColor(dither, Color.FromArgb(r[3], g[3], b[3]), i + 1, j + 1);
                }
            }

            // Dispose Image
            original.Dispose();

            // Return dithered Image
            return dither;
        }

        /**
         * The halftoning filter is the traditional method of printing images. 
         * It is a reprographic technique that simulates continuous tone imagery through the use of dots.
         */
        public Image HalftoneFilter(string filepath)
        {
            // Load Image
            Image original = new Bitmap(filepath);

            // Image Size
            int width = original.Width;
            int height = original.Height;

            // Create new Image bitmap
            Image halftone = CreateBlankImage(width, height);

            // Process each region into a halftone pixel (2x2)
            for (int i = 0; i < width; i+=2) 
            {
                for (int j = 0; j < height; j+=2)
                {
                    // Get Pixels
                    Color p1 = GetPixelColor(original, i, j);
                    Color p2 = GetPixelColor(original, i, j + 1);
                    Color p3 = GetPixelColor(original, i + 1, j);
                    Color p4 = GetPixelColor(original, i + 1, j + 1);

                    // Transform to grayscale
                    int gray1 = (int)((p1.R * 0.299) + (p1.G * 0.587) + (p1.B * 0.114));
                    int gray2 = (int)((p2.R * 0.299) + (p2.G * 0.587) + (p2.B * 0.114));
                    int gray3 = (int)((p3.R * 0.299) + (p3.G * 0.587) + (p3.B * 0.114));
                    int gray4 = (int)((p4.R * 0.299) + (p4.G * 0.587) + (p4.B * 0.114));
                    
                    // Saturation Percentage
                    int saturation = (gray1 + gray2 + gray3 + gray4) / 4;

                    // Draw white/black depending on saturation
                    if (saturation > 223)
                    {
                        SetPixelColor(halftone, Color.White, i, j);
                        SetPixelColor(halftone, Color.White, i, j + 1);
                        SetPixelColor(halftone, Color.White, i + 1, j);
                        SetPixelColor(halftone, Color.White, i + 1, j + 1);
                    }
                    else if (saturation > 159)
                    {
                        SetPixelColor(halftone, Color.White, i, j);
                        SetPixelColor(halftone, Color.Black, i, j + 1);
                        SetPixelColor(halftone, Color.White, i + 1, j);
                        SetPixelColor(halftone, Color.White, i + 1, j + 1);
                    }
                    else if (saturation > 95)
                    {
                        SetPixelColor(halftone, Color.White, i, j);
                        SetPixelColor(halftone, Color.Black, i, j + 1);
                        SetPixelColor(halftone, Color.Black, i + 1, j);
                        SetPixelColor(halftone, Color.White, i + 1, j + 1);
                    }
                    else if (saturation > 32)
                    {
                        SetPixelColor(halftone, Color.Black, i, j);
                        SetPixelColor(halftone, Color.White, i, j + 1);
                        SetPixelColor(halftone, Color.Black, i + 1, j);
                        SetPixelColor(halftone, Color.Black, i + 1, j + 1);
                    }
                    else
                    {
                        SetPixelColor(halftone, Color.Black, i, j);
                        SetPixelColor(halftone, Color.Black, i, j + 1);
                        SetPixelColor(halftone, Color.Black, i + 1, j);
                        SetPixelColor(halftone, Color.Black, i + 1, j + 1);
                    }
                }
            }

            // Dispose Image
            original.Dispose();

            // Return grayscale Image
            return halftone;
        }

        /**
         * A better choice for grayscale is the ITU-R Recommendation BT.601-7, 
         * which specifies methods for digitally coding video signals by normalizing the values.
         */
        public Image GrayscaleFilter(string filepath)
        {
            // Load Image
            Image original = new Bitmap(filepath);

            // Image Size
            int width = original.Width;
            int height = original.Height;

            // Create new Image bitmap
            Image grayscale = CreateBlankImage(width, height);

            // Process each pixel into grayscale
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    // Get Pixel
                    Color pixel = GetPixelColor(original, i, j);

                    // Get R, G, B values (This are integers from 0 to 255)
                    int red = pixel.R;
                    int green = pixel.G;
                    int blue = pixel.B;

                    // Transform to grayscale
                    int gray = (int)((red * 0.299) + (green * 0.587) + (blue * 0.114));

                    // Set Pixel in new image
                    SetPixelColor(grayscale, Color.FromArgb(gray, gray, gray), i, j);
                }
            }

            // Dispose Image
            original.Dispose();

            // Return grayscale Image
            return grayscale;
        }
        #endregion

        #region ImageIO
        /**
         * Save Image as PNG
         */
        public void SaveImage(Image image, String filepath)
        {
            image.Save(filepath, ImageFormat.Png);
        }

        /**
         * Create Blank Image
         */
        public Image CreateBlankImage(int Width, int Height)
        {
            Image BlankBitmap = new Bitmap(Width, Height);
            ((Bitmap)BlankBitmap).SetPixel(0, 0, Color.White);
            return BlankBitmap;
        }
        #endregion

        #region PixelManipulation
        /**
         * Return colour value depending on quadrant and saturation
         */ 
        public int DitherSaturation(int color, int quadrant)
        {
            if (color > 223) 
            {
                return 255;
            }
            else if (color > 159) 
            {
                if (quadrant != 1)
                {
                    return 255;
                }

                return 0;
            }
            else if (color > 95) 
            {
                if (quadrant == 0 || quadrant == 3)
                {
                    return 255;
                }

                return 0;
            }
            else if (color > 32) 
            {
                if (quadrant == 1)
                {
                    return 255;
                }

                return 0;
            }
            else 
            {
                return 0;
            }
        }

        /**
         * Return the pixel colour of an image
         */
        public Color GetPixelColor(Image image, int x, int y)
        {
            return ((Bitmap)image).GetPixel(x, y);
        }

        /**
         * Set the pixel colour of an image
         */
        public void SetPixelColor(Image image, Color color, int x, int y)
        {
            ((Bitmap)image).SetPixel(x, y, color);
        }
        #endregion
    }
}
