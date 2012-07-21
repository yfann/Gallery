using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using Models;


namespace BLL
{
    public class GalleryBLL : BaseBLL<GalleryDAL, GalleryModel>
    {
        private static GalleryBLL _bll;
        public static GalleryBLL GetBLL()
        {
            if (_bll == null)
                _bll = new GalleryBLL();
            return _bll;
        }

        public override void Save(GalleryModel m)
        {
            base.UploadFile(m.path,m.ImageName);
            base.Save(m);
        }
    }
}