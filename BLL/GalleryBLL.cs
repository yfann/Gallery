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
        public override void Save(GalleryModel m)
        {
            base.Save(m);
        }
    }
}