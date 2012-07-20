using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

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

        public Bitmap Pic { get; set; }

        [BsonIgnore]
        public string path { get; set; }
    }
}