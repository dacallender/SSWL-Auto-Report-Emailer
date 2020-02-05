using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;

namespace SiriusScientific.Core.Containers
{
	public abstract class Repository<T> : IRepository<T> where T : class
	{
		protected readonly DbSet<T> DbSet;

		protected readonly DbContext Context;
		public Repository( DbContext  dataContext )
		{
			Context = dataContext;

			DbSet = dataContext.Set<T>();
		}

        #region IRepository<T> Members
		public virtual T Insert( T entity )
		{
			var result = DbSet.Add(entity);

			return result;
		}

		public virtual T Update( T entity)
		{
			DbSet.AddOrUpdate(entity);

			return entity;
		}

		public virtual void Delete( T entity )
		{
			DbSet.Remove(entity);
		}

		public virtual IQueryable<T> SearchFor( Expression<Func<T, bool>> predicate )
		{
			return DbSet.Where(predicate);
		}

		public IQueryable<T> GetAll()
		{
			return DbSet;
		}

		public T GetById( int id )
		{
			return DbSet.Find(id);
		}

        #endregion
	}
}
