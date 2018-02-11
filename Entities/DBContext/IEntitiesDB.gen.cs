using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace ARQ.Maqueta.Entities
{
    public partial interface IEntitiesDB
    {
        IDbSet<Flight> FlightSet { get; set; }

		DbEntityEntry Entry(object entity);

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

		T EntryWithState<T>(T entity, EntityState state) where T : class;

		int SaveChanges();
    }
}
