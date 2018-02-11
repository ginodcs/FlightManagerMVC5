using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using ARQ.Maqueta.Entities;

namespace ARQ.Maqueta.Services.Tests
{
	// <summary>
    ///Se trata de una clase de prueba para ICommentService y se pretende que
    ///contenga todas las pruebas unitarias ICommentService.
    ///</summary>
    [TestClass()]
    public abstract class ServiceTest
    {   
       private TestContext testContextInstance;

        /// <summary>
        ///Obtiene o establece el contexto de la prueba que proporciona
        ///la información y funcionalidad para la ejecución de pruebas actual.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        public ServiceTest()
        {
            TestInitialize();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            if (Database.Exists(typeof(EntitiesDB).Name))
                //Recreates the DB if the model changes
                Database.SetInitializer<EntitiesDB>(new DatabaseChangedInitializer());
            else
                //This is the default strategy.  It creates the DB only if it doesn't exist
                Database.SetInitializer(new DatabaseNotFoundInitializer());
        }
	}
}
