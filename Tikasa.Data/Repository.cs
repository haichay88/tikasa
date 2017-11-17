using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;

namespace Tikasa.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private tikasaEntities dataContext;
        private readonly IDbSet<TEntity> dbset;
        public Repository(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            dbset = DataContext.Set<TEntity>();
        }

        private readonly ObjectContext _objContext;

        private ObjectSet<TEntity> _objSet;
        private ObjectSet<TEntity> ObjSet
        {
            get
            {
                if (_objSet == null)
                {
                    _objSet = _objContext.CreateObjectSet<TEntity>();
                }
                return _objSet;
            }
        }

        protected IEnumerable<TEntity> ExecQuery<TEntity>(string query, params object[] paras)
        {
            return this._objContext.ExecuteStoreQuery<TEntity>(query, paras);
        }



        protected IDatabaseFactory DatabaseFactory
        {
            get;
            private set;
        }

        protected tikasaEntities DataContext
        {
            get { return dataContext ?? (dataContext = DatabaseFactory.Get()); }
        }
        public virtual void Add(TEntity entity)
        {
            dbset.Add(entity);
        }
        public virtual void Update(TEntity entity)
        {
            dbset.Attach(entity);
            dataContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }
        public virtual void Delete(TEntity entity)
        {
            dbset.Remove(entity);
        }
        public virtual void Delete(Expression<Func<TEntity, bool>> where)
        {
            IEnumerable<TEntity> objects = dbset.Where<TEntity>(where).AsEnumerable();
            foreach (TEntity obj in objects)
                dbset.Remove(obj);
        }
        public virtual TEntity GetById(long id)
        {
            return dbset.Find(id);
        }
        public virtual TEntity GetById(string id)
        {
            return dbset.Find(id);
        }
        public virtual IEnumerable<TEntity> GetAll()
        {
            return dbset.ToList();
        }
        public virtual IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where)
        {
            return dbset.Where(where).ToList();
        }
        public TEntity Get(Expression<Func<TEntity, bool>> where)
        {
            return dbset.Where(where).FirstOrDefault<TEntity>();
        }
        public IQueryable<TEntity> GetQueryable()
        {
            return this.dataContext.Set<TEntity>();
        }
    }
}