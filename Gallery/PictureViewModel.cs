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
        private IList<string> _picList;
        private bool _canScroll;
        public PictureViewModel(string fileName,IList<string> picList)
        {
            _galleryBLL = GalleryBLL.GetBLL();
            _picList = picList;
            if (_picList != null && _picList.Count > 0)
            {
                _canScroll = true;
            }
            GetPicture(fileName);
        }

        public void GetPicture(string fileName)
        {
            PictureName = fileName;
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

        public double TT
        {
             
            get
            {
                return GetValue(() =>TT);
            }
            set
            {
                SetValue(() =>TT, value);
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

        public void NextPicture()
        {
            if (_canScroll)
            {
                int pos=_picList.IndexOf(PictureName);
                if (pos >-1 && (pos + 1) < _picList.Count)
                {
                    GetPicture(_picList[pos+1]);
                }
            }
        }
        public void PreviousPicture()
        {
            if (_canScroll)
            {
                int pos = _picList.IndexOf(PictureName);
                if (pos > 0 && (pos - 1) >-1)
                {
                    GetPicture(_picList[pos - 1]);
                }
            }
        }
    }
}
