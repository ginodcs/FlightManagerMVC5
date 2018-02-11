using System;
using System.Data.Entity;

namespace ARQ.Maqueta.Entities
{
    public class DbIntializer<T> : IDatabaseInitializer<EntitiesDB> where T : IDatabaseInitializer<EntitiesDB>
    {
        private IDatabaseInitializer<EntitiesDB> initializer;

        public DbIntializer()
        {
            this.initializer = (T)Activator.CreateInstance(typeof(T)) as IDatabaseInitializer<EntitiesDB>;
        }

        public void InitializeDatabase(EntitiesDB context)
        {
            this.initializer.InitializeDatabase(context);
            //ScriptManager.AddObjectsRules(context);
            this.Seed(context);
        }

        private bool ConstraintExist(Type table, string constraintName)
        {
            return false;
        }

        protected virtual void Seed(EntitiesDB context)
        {
            DatabaseInitializerHelper.Seed(context);
        }
    }
}
