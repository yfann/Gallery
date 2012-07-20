using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using MongoDB.Driver;

namespace BLL
{
    public abstract class BaseBLL<TDAL, TDocument> where TDAL : BaseDAL<TDocument>, new()
    {
        protected TDAL dal = new TDAL();

        public TDocument FindOne(IMongoQuery query, Action<TDocument> preprocess)
        {
            return dal.FindOne(query, preprocess);
        }

        public virtual IList<TDocument> FindAll()
        {
            return dal.FindAll();
        }

        public virtual void Save(TDocument t)
        {
            dal.Save(t);
        }
    }
}