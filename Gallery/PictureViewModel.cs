using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gallery.Base;
using System.IO;
using BLL;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Windows;
namespace Gallery
{
    public class PictureViewModel:BaseViewModel
    {
        private Stream _pictureSteam;
        private GalleryBLL _galleryBLL;
        public PictureViewModel(string fileName)
        {

            PictureName = fileName;
            _galleryBLL = GalleryBLL.GetBLL();
            using (_pictureSteam = new MemoryStream())
            {
                _galleryBLL.GetFile(_pictureSteam, fileName);
                Bitmap map = new Bitmap(_pictureSteam);
                Picture = Imaging.CreateBitmapSourceFromHBitmap(map.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                PicWidth = Picture.Width;
                PicHeight = Picture.Height;
                PicInfo = string.Format("   {0} × {1}   {2}", Picture.Width, Picture.Height, Picture.Format);
            }
        }

        public string PicInfo
        {
            get
            {
                return GetValue(() => PicInfo);
            }
            set
            {
                SetValue(() => PicInfo, value);
            }
        }

        public double PicWidth
        {
            get
            {
                return GetValue(() => PicWidth);
            }
            set
            {
                SetValue(() => PicWidth, value);
            }
        }

        public double PicHeight
        {
            get
            {
                return GetValue(() => PicHeight);
            }
            set
            {
                SetValue(() => PicHeight, value);
            }
        }

        public BitmapSource Picture
        {
            get
            {
                return GetValue(() => Picture);
            }
            set
            {
                SetValue(() => Picture, value);
            }
        }

        public string PictureName 
        {
            get
            {
                return GetValue(() => PictureName);
            }
            set
            {
                SetValue(() => PictureName, value);
            }
        }
    }
}
