using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.ApplicationModel.Wallet;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace _10Pass
{
    public static class HelperMethods {

        public static async Task<BitmapImage> GetBitmapImageAsync(StorageFile storageFile)
        {
            BitmapImage bitmapImage = new BitmapImage();
            FileRandomAccessStream stream = (FileRandomAccessStream)await storageFile.OpenAsync(FileAccessMode.Read);
            bitmapImage.SetSource(stream);
            return bitmapImage;
        }

        static int HexToInt(string hex)
        {
            int returnval = 0;
                for(int i = 0; i<hex.Length;i++)
                {
                    int b = 0;
                    if (!int.TryParse(hex[i]+"",out b)) {
                        switch ((hex[i]+"").ToLower())
                        {
                            case "a": b = 10; break;
                            case "b": b = 11; break;
                            case "c": b = 12; break;
                            case "d": b = 13; break;
                            case "e": b = 14; break;
                            case "f": b = 15; break;
                            default:  throw new ArgumentOutOfRangeException();
                        }
                    }
                    b = (byte)(b*Math.Pow(16, hex.Length - 1 - i));
                    returnval += b;
                }

            return returnval;
        }

        /// <summary>
        /// A function that gets a color object from an encoded rgb string.
        /// </summary>
        /// <param name="rgb">A string containing three values 0-255</param>
        /// <returns>The decoded color; Windows.UI.Color</returns>
        public static Windows.UI.Color getColorFromRGBString(string rgb)
        {
            string str = Regex.Replace(rgb, @"([A-Za-z]|\(|\))", "");
            int r, g, b = 0;
            // R VALUE
            if (int.TryParse(Regex.Split(rgb, @"\D+")[1], out r))
            {
                if(r>255 || r<0)
                    throw new ArgumentOutOfRangeException("r value is not a byte.");
                else
                {
                    // G VALUE
                    if (int.TryParse(Regex.Split(rgb, @"\D+")[2], out g))
                    {
                        if (g > 255 || g < 0)
                            throw new ArgumentOutOfRangeException("g value is not a byte.");
                        else
                        {
                            if (int.TryParse(Regex.Split(rgb, @"\D+")[3], out b))
                            {
                                if (b > 255 || b < 0)
                                    throw new ArgumentOutOfRangeException("b value is not a byte.");
                            }
                            else
                                throw new FormatException("b value contains invalid characters.");
                        }
                    }
                    else
                        throw new FormatException("g value contains invalid characters.");
                }
            }
            else
                throw new FormatException("r value contains invalid characters.");

            return Windows.UI.Color.FromArgb(255,(byte)r, (byte)g, (byte)b);
        }

        public static Windows.UI.Color getColorFromHex(string hexcode)
        {
            byte a, r, g, b = 0;
            hexcode = Regex.Replace(hexcode, @"\#", "");

            if (hexcode.Length == 8)
            {
                a = (byte)HexToInt(new string(new char[] { hexcode[0], hexcode[1] }));
                r = (byte)HexToInt(new string(new char[] { hexcode[2], hexcode[3] }));
                g = (byte)HexToInt(new string(new char[] { hexcode[4], hexcode[5] }));
                b = (byte)HexToInt(new string(new char[] { hexcode[6], hexcode[7] }));
            }
            else if (hexcode.Length == 6)
            {
                a = 255;
                r = (byte)HexToInt(new string(new char[] { hexcode[0], hexcode[1] }));
                g = (byte)HexToInt(new string(new char[] { hexcode[2], hexcode[3] }));
                b = (byte)HexToInt(new string(new char[] { hexcode[4], hexcode[5] }));
            }
            else throw new FormatException();

            return Windows.UI.Color.FromArgb(a, r, g, b);
        }

        /// <summary>
        /// A method to clear all files from the app's temp folder.
        /// </summary>
        static async public void clearTempFiles()
        {
            IReadOnlyList<Windows.Storage.StorageFile> files = await Windows.Storage.ApplicationData.Current.TemporaryFolder.GetFilesAsync();
            if (files.Count != 0)
            {
                foreach (Windows.Storage.StorageFile fi in files)
                {
                    await fi.DeleteAsync();
                }
            }
        }
    }

}