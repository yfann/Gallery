using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;

namespace DataAccess
{
    internal class GalleryDAL : BaseDAL<GalleryModel>
    {
        protected override string SetCollectionName()
        {
            return "gallery";
        }
    }
}