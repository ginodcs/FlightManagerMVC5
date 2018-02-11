using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ARQ.Maqueta.Entities;

namespace ARQ.Maqueta.Services
{
    public abstract class Service
    {
        public IEntitiesDB EntitiesDB { get; set; }

        public Service(IEntitiesDB entitiesDB)
        {
            EntitiesDB = entitiesDB;
            
        }
    }
}
