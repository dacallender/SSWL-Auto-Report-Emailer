using System;
using System.Linq;
using System.Linq.Expressions;

namespace SiriusScientific.Core.Containers
{
	public interface IRepository<T>
	{
		T Insert( T entity );
		void Delete( T entity );
		T Update( T entity );
		IQueryable<T> SearchFor( Expression<Func<T, bool>> predicate );
		IQueryable<T> GetAll();
		T GetById( int id );
	}
}
