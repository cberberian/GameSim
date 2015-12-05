using System;
using System.Collections.Generic;
using System.Linq;

namespace SimGame.Data.Mock
{
    public class FakeDbSet<T> : System.Data.Entity.IDbSet<T> where T : class
    {
        private readonly List<T> _list;

        public FakeDbSet()
        {
            _list = new List<T>();
        }

        public FakeDbSet(IEnumerable<T> contents)
        {
            this._list = contents.ToList();
        }

        #region IDbSet<T> Members

        public T Add(T entity)
        {
            this._list.Add(entity);
            return entity;
        }

        public T Attach(T entity)
        {
            this._list.Add(entity);
            return entity;
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            throw new NotImplementedException();
        }

        public T Create()
        {
            throw new NotImplementedException();
        }

        public T Find(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public System.Collections.ObjectModel.ObservableCollection<T> Local
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public T Remove(T entity)
        {
            this._list.Remove(entity);
            return entity;
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return this._list.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this._list.GetEnumerator();
        }

        #endregion

        #region IQueryable Members

        public Type ElementType
        {
            get { return this._list.AsQueryable().ElementType; }
        }

        public System.Linq.Expressions.Expression Expression
        {
            get { return this._list.AsQueryable().Expression; }
        }

        public IQueryProvider Provider
        {
            get { return this._list.AsQueryable().Provider; }
        }

        #endregion
    }
}