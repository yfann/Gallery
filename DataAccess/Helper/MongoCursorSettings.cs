using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;

namespace DataAccess.Helper
{
    public class MongoCursorSettings
    {
        private int m_skip = -1;

        public int Skip
        {
            get { return m_skip; }
            set { m_skip = value; }
        }

        private int m_limit = -1;

        public int Limit
        {
            get { return m_limit; }
            set { m_limit = value; }
        }

        private IMongoFields m_fields = null;

        public IMongoFields Fields
        {
            get { return m_fields; }
            set { m_fields = value; }
        }

        private IMongoSortBy m_sortBy = null;

        public IMongoSortBy SortBy
        {
            get { return m_sortBy; }
            set { m_sortBy = value; }
        }

        internal void Set(MongoCursor cursor)
        {
            if (cursor == null)
                return;

            if (Skip != -1)
            {
                cursor.Skip = Skip;
            }
            if (Limit != -1)
            {
                cursor.Limit = Limit;
            }
            if (Fields != null)
            {
                cursor.Fields = Fields;
            }
            if (SortBy != null)
            {
                cursor.SetSortOrder(SortBy);
            }
        }

        #region 链式方法

        public static MongoCursorSettings Create()
        {
            return new MongoCursorSettings();
        }

        public MongoCursorSettings SetSkip(int skip)
        {
            this.Skip = skip;
            return this;
        }

        public MongoCursorSettings SetLimit(int limit)
        {
            this.Limit = limit;
            return this;
        }

        public MongoCursorSettings SetFields(IMongoFields fields)
        {
            this.Fields = fields;
            return this;
        }

        public MongoCursorSettings SetFields(params string[] names)
        {
            this.Fields = MongoDB.Driver.Builders.Fields.Include(names);
            return this;
        }

        public MongoCursorSettings SetSortOrder(IMongoSortBy sortBy)
        {
            this.SortBy = sortBy;
            return this;
        }

        #endregion 链式方法
    }
}