
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ARQ.Maqueta.Entities
{
    public class EntitiesDBBase : DbContext
    {

		public static char CompositeKeySeparator = '^';

		public DbModelBuilder ModelBuilder { get; set; }

        public EntitiesDBBase() { }

        public EntitiesDBBase(string nameOrConnectionString): base(nameOrConnectionString) { }

        public IDbSet<Flight> FlightSet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {   
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
			ConfigureMapping(modelBuilder);
            base.OnModelCreating(modelBuilder);
			this.ModelBuilder = modelBuilder;
        }

		public virtual void ConfigureMapping(DbModelBuilder modelBuilder)
		{
			new DbMappings().Configure(modelBuilder);
		}
		
		public override int SaveChanges()
        {
			try
            {
                return base.SaveChanges();
            }            
            catch (DbEntityValidationException ex)
            { 
                StringBuilder validationErrors=new StringBuilder();
                foreach(DbEntityValidationResult dbExc in ex.EntityValidationErrors)
                    foreach(DbValidationError valErr in dbExc.ValidationErrors)
                        validationErrors.AppendLine(valErr.PropertyName + ": " + valErr.ErrorMessage);
                throw new Exception(validationErrors.ToString());
            }
        }

		public virtual T EntryWithState<T>(T entity, EntityState state) where T : class
        {
            if (entity == null)
                return null;

            var objContext = ((IObjectContextAdapter)this).ObjectContext;
            var objSet = objContext.CreateObjectSet<T>();
            var entityKey = objContext.CreateEntityKey(objSet.EntitySet.Name, entity);

            object foundState;

            var exists = objContext.TryGetObjectByKey(entityKey, out foundState);
            if (exists)
            {
                objContext.ObjectStateManager.ChangeObjectState(foundState, state);
                return (foundState as T);
            }
            else
            {
                DbEntityEntry entry = this.Entry(entity);
                entry.State = state;
                return entry.Entity as T;
            }
        }
    }
}
