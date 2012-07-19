using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace Models
{
    [Serializable]
    public class GalleryModel
    {
        public ObjectId Id { get; set; }

        public string ImageName { get; set; }

        public string ExtName { get; set; }

        public IEnumerable<string> Tags { get; set; }

        public DateTime CreateTime { get; set; }
    }
}