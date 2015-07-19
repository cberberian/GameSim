using System;
using System.Linq.Expressions;
using SimGame.Domain;

namespace SimGame.Data.Entity
{
    public class RepositoryRequest<T>
    {
        public Expression<Func<T, bool>> Expression { get; set; }
    }
}