using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Helper;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System.IO;

namespace DataAccess
{

    public abstract class BaseDAL<TDocument>
    {
        protected internal string CollectionName { set; get; }

        protected abstract string SetCollectionName();

        private MongoCollection<TDocument> m_collection;

        protected internal MongoCollection<TDocument> Collection
        {
            get
            {
                if (m_collection == null)
                {
                    CollectionName = SetCollectionName();
                    m_collection = MongoHelper.GetDatabase().GetCollection<TDocument>(CollectionName);
                }
                return m_collection;
            }
        }


        public MongoGridFSFileInfo UploadFile(string localpath, string remoteName)
        {
            MongoGridFS fs = MongoHelper.GetGridFS();

            return fs.Upload(localpath,remoteName);
        }

        public void GetFile(Stream s,string fileName)
        {
            MongoGridFS fs = MongoHelper.GetGridFS();
            fs.Download(s,fileName);
        }


        public TDocument FindOne(IMongoQuery query, Action<TDocument> preprocess)
        {
            var document = Collection.FindOne(query);
            if (preprocess != null)
            {
                preprocess(document);
            }
            return document;
        }

        protected internal IList<TDocument> CursorToList(MongoCursor<TDocument> cursor, Action<TDocument> preprocess)
        {
            IList<TDocument> list = new List<TDocument>(30);
            bool isPreprocess = preprocess != null;
            foreach (TDocument document in cursor)
            {
                var doc = document;
                if (isPreprocess)
                    preprocess(doc);
                list.Add(doc);
            }
            return list;
        }

        public IList<TDocument> Find(IMongoQuery query, MongoCursorSettings cursorSettings, Action<TDocument> preprocess)
        {
            var cursor = Collection.Find(query);
            if (cursorSettings != null)
            {
                cursorSettings.Set(cursor);
            }
            var list = CursorToList(cursor, preprocess);
            return list;
        }

        public IList<TDocument> FindAll()
        {
            var cursor = Collection.FindAll();

            var list = cursor.ToList();

            return list;
        }

        public void Save(TDocument doc)
        {
            Collection.Save<TDocument>(doc);
        }
    }
}