using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Helper;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataAccess
{
    /// <summary>
    /// 数据访问层基类
    /// </summary>
    /// <typeparam name="T">文档实体类</typeparam>
    public abstract class BaseDAL<TDocument>
    {
        protected internal string CollectionName { set; get; }

        /// <summary>
        /// 设置集合名
        /// </summary>
        protected abstract string SetCollectionName();

        private MongoCollection<TDocument> m_collection;

        /// <summary>
        /// 根据CollectionName得到MongoCollection对象
        /// </summary>
        protected internal MongoCollection<TDocument> Collection
        {
            get
            {
                if (m_collection == null)
                {
                    CollectionName = SetCollectionName();
                    m_collection = MongoHelper.GetDatabase("yfann").GetCollection<TDocument>(CollectionName);
                }
                return m_collection;
            }
        }

        /// <summary>
        /// 根据query条件得到一个文档对象
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="preprocess">预处理方法</param>
        /// <returns></returns>
        public TDocument FindOne(IMongoQuery query, Action<TDocument> preprocess)
        {
            var document = Collection.FindOne(query);
            if (preprocess != null)
            {
                preprocess(document);
            }
            return document;
        }

        /// <summary>
        /// 把MongoCursor转换成IList类型
        /// </summary>
        /// <param name="cursor">文档游标</param>
        /// <param name="preprocess">预处理方法</param>
        /// <returns></returns>
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

        /// <summary>
        /// 根据query查询集合
        /// </summary>
        /// <param name="query">条件</param>
        /// <param name="preprocess">预处理方法</param>
        /// <returns></returns>
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