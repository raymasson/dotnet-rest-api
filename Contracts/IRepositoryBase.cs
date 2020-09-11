using System;
using System.Linq;
using System.Linq.Expressions;

namespace Contracts
{
	public interface IRepositoryBase<T>
	{
		IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
	}
}