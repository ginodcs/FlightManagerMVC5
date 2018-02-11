using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARQ.Maqueta.Entities;

namespace ARQ.Maqueta.Services.Tests
{
    public static class DataContextUtils
    {
        private static EntitiesDB _entities = new EntitiesDB();
        public static EntitiesDB GetDataContext()
        {
            return _entities ?? (_entities = new EntitiesDB());
        }
    }
}
