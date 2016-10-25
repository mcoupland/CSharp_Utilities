using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Windows;

namespace Utilities
{
    public class Images
    {
        #region Scale Image Methods & Overloads
        public static Bitmap ScalePicture(FileInfo picturesource, ushort maxdimension)
        {
            return ScalePicture(picturesource.FullName, maxdimension);
        }
        public static Bitmap ScalePicture(string picturesource, ushort maxdimension)
        {
            using (Bitmap picture_from_file = Bitmap.FromFile(picturesource) as Bitmap)
            {
                if (picture_from_file.Height > picture_from_file.Width) { return ScalePortraitPicture(picturesource, maxdimension); }
                else if (picture_from_file.Height < picture_from_file.Width) { return ScaleLandscapePicture(picturesource, maxdimension); }
            }
            throw new Utilities.Exceptions.PictureManipulationException(
                string.Format(
                    @"Unknown error occured in {0}, picturesource:{1}, maxdimension:{2}",
                    "ScalePicture",
                    picturesource,
                    maxdimension
                )
            );
        }
        private static Bitmap ScalePortraitPicture(FileInfo picturesource, ushort maxdimension)
        {
            return ScalePortraitPicture(picturesource.FullName, maxdimension);
        }
        private static Bitmap ScalePortraitPicture(string picturesource, ushort maxdimension)
        {
            using (Bitmap picture_from_file = Bitmap.FromFile(picturesource) as Bitmap)
            {
                float width = maxdimension;
                float owidth = (float)picture_from_file.Width;
                float multiplier = width / owidth;
                float height = (float)picture_from_file.Height * multiplier;
                ushort scale_width = Utilities.Numbers.GetIntFloor(width);
                ushort scale_height = Utilities.Numbers.GetIntFloor(height);
                return new Bitmap(picture_from_file, new System.Drawing.Size(scale_width, scale_height));
            }
        }
        private static Bitmap ScaleLandscapePicture(FileInfo picturesource, ushort maxdimension)
        {
            return ScaleLandscapePicture(picturesource.FullName, maxdimension);
        }
        private static Bitmap ScaleLandscapePicture(string picturesource, ushort maxdimension)
        {
            using (Bitmap picture_from_file = Bitmap.FromFile(picturesource) as Bitmap)
            {
                float height = maxdimension;
                float oheight = (float)picture_from_file.Height;
                float multiplier = height / oheight;
                float width = (float)picture_from_file.Width * multiplier;
                ushort scale_width = Utilities.Numbers.GetIntFloor(width);
                ushort scale_height = Utilities.Numbers.GetIntFloor(height);
                return new Bitmap(picture_from_file, new System.Drawing.Size(scale_width, scale_height));
            }
        }
        #endregion

        #region Crop Image Methods & Overloads
        public static Bitmap CropCapture(FileInfo picturefile, ushort maxdimension)
        {
            return CropCapture(Bitmap.FromFile(picturefile.FullName) as Bitmap, maxdimension);
        }
        public static Bitmap CropCapture(string picturefile, ushort maxdimension)
        {
            return CropCapture(Bitmap.FromFile(picturefile) as Bitmap, maxdimension);
        }
        public static Bitmap CropCapture(Bitmap picture_bitmap, ushort maxdimension)
        {
            using (picture_bitmap)
            {
                if (picture_bitmap.Width > picture_bitmap.Height) { return CropLandscapePicture(picture_bitmap, maxdimension); }
                else { return CropPortraitPicture(picture_bitmap, maxdimension); }
            }
        }
        private static Bitmap CropLandscapePicture(Bitmap picture, ushort maxdimension)
        {
            using (picture)
            {
                // Int32Rect needs reference to WindowsBase.dll
                Int32Rect converted_crop = new Int32Rect();
                converted_crop.X = (picture.Width - maxdimension) / 2;
                converted_crop.Y = 0;
                Rectangle crop_box = new Rectangle(converted_crop.X, converted_crop.Y, maxdimension, maxdimension);
                return picture.Clone(crop_box, picture.PixelFormat);
            }
        }
        private static Bitmap CropPortraitPicture(Bitmap picture, ushort maxdimension)
        {
            using (picture)
            {
                Int32Rect converted_crop = new System.Windows.Int32Rect();
                converted_crop.X = 0;
                converted_crop.Y = (picture.Height - maxdimension) / 2; ;
                Rectangle crop_box = new Rectangle(converted_crop.X, converted_crop.Y, maxdimension, maxdimension);
                return picture.Clone(crop_box, picture.PixelFormat);
            }
        }
        #endregion

        #region Sort Images
        /*
         * Contrived properties to show/implement sorting
         */
        public ushort ID;
        public string Name;
        public class CompareFileInfo : IComparer<FileInfo>
        {
            public int Compare(FileInfo s1, FileInfo s2)
            {
                return (string.Compare(s1.Name, s2.Name));
            }
        }
        public void ContrivedSort()
        {
            List<FileInfo> fileinfos = new List<FileInfo>();
            for (ushort i = 0; i < 20; i++)
            {
                FileInfo fileinfo_instance = new FileInfo(Guid.NewGuid().ToString());
                fileinfos.Add(fileinfo_instance);
            }
            fileinfos.Sort(new CompareFileInfo());
        }
        #endregion
        
        #region Overloaded Methods to Get BitmapSource for WPF Image Control
        public static System.Windows.Media.ImageSource BitmapSourceFromString(string source)
        {
            return BitmapSourceFromUri(new Uri(source));
        }
        public static System.Windows.Media.ImageSource BitmapSourceFromUri(Uri source)
        {
            var bitmap_image = new System.Windows.Media.Imaging.BitmapImage();
            bitmap_image.BeginInit();
            bitmap_image.UriSource = source;
            bitmap_image.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad;
            bitmap_image.CreateOptions = System.Windows.Media.Imaging.BitmapCreateOptions.IgnoreImageCache;
            bitmap_image.EndInit();
            return bitmap_image;
            throw new FileNotFoundException(string.Format(@"Argument source {0} does not identify an existing file", source.OriginalString));
        }
        #endregion
    }
}
