using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ARQ.Maqueta.Entities;
using Newtonsoft.Json;

namespace ARQ.Maqueta.Services.Tests
{
	// <summary>
    ///Se trata de una clase de prueba para IDepartamentoService y se pretende que
    ///contenga todas las pruebas unitarias IDepartamentoService.
    ///</summary>
    [TestClass()]
    public partial class DepartamentoServiceTest : ServiceTest
    {
		#region Departamento Test
				
        [TestMethod()]
        public void FindDepartamentoTest()
        {
			IDepartamentoService target = DepartamentoServiceUtil.CreateDepartamentoService();
            Departamento expected = DepartamentoServiceUtil.CreateDepartamento(); 
            Departamento actual = target.Find(expected.Id);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void FindAllDepartamentoTest()
        {
			IDepartamentoService target = DepartamentoServiceUtil.CreateDepartamentoService();  
            int expected = target.SearchCount("");  
            List<Departamento> actual = target.FindAll();
            Assert.AreEqual(expected, actual.Count);
        }
	
        [TestMethod()]
        public void SearchPagedDepartamentoTest()
        {
            IDepartamentoService target = DepartamentoServiceUtil.CreateDepartamentoService(); 
            int pageIndex = 1; 
            int pageCount = 10;
			Departamento expected = DepartamentoServiceUtil.CreateDepartamentoFull();
            List<Departamento> actual = target.Search(expected.Nombre, pageIndex, pageCount);
			int count = target.SearchCount(expected.Nombre);
			Assert.IsTrue(actual.Count <= count);
			Assert.IsTrue(actual.Count <= 10);
        }

        [TestMethod()]
        public void AddDepartamentoTest()
        {
            IDepartamentoService target = DepartamentoServiceUtil.CreateDepartamentoService(); 
           
			Departamento elementAdded = DepartamentoServiceUtil.CreateDepartamento();

			elementAdded = target.Find(elementAdded.Id);
			Assert.IsNotNull(elementAdded);
        }

        [TestMethod()]
        public void ChangeDepartamentoTest()
        {
			Departamento departamento = DepartamentoServiceUtil.CreateDepartamento();

            IDepartamentoService target = DepartamentoServiceUtil.CreateDepartamentoService();
            departamento = target.Find(departamento.Id);
			
			departamento.Nombre = "Changed Nombre";
			target.Change(departamento);

			target = DepartamentoServiceUtil.CreateDepartamentoService();
            departamento = target.Find(departamento.Id);

            Assert.IsTrue(departamento.Nombre == "Changed Nombre");
        }

        [TestMethod()]
        public void RemoveDepartamentoTest()
        {
            IDepartamentoService target = DepartamentoServiceUtil.CreateDepartamentoService(); 
            Departamento pDepartamento = DepartamentoServiceUtil.CreateDepartamento();
            target.Remove(pDepartamento);
            pDepartamento = target.Find(pDepartamento.Id);
			Assert.IsNull(pDepartamento);
        }
		

		// Navigation properties management (one-to-many) / (many-to-many)

        [TestMethod()]
        public void AddDepartamentosTest()
        {
            Departamento departamento1 = DepartamentoServiceUtil.CreateDepartamento();
            Departamento departamentos = DepartamentoServiceUtil.CreateDepartamento(departamento1);

           var serialized = JsonConvert.SerializeObject(new[] { JsonConvert.SerializeObject(departamento1),
                                            JsonConvert.SerializeObject(departamentos) });
           

            IDepartamentoService departamento1Service = DepartamentoServiceUtil.CreateDepartamentoService();
            departamento1Service.AddDepartamentos(departamento1, departamentos);

            IDepartamentoService departamentosService = DepartamentoServiceUtil.CreateDepartamentoService();
            departamentos = departamentosService.Find(departamentos.Id);
			
			Assert.IsTrue(departamentos.Departamento1.Id == departamento1.Id);
        }

        [TestMethod()]
        public void RemoveDepartamentosTest()
        {
            Departamento departamento1 = DepartamentoServiceUtil.CreateDepartamento();
            Departamento departamentos = DepartamentoServiceUtil.CreateDepartamento(departamento1);

        

            IDepartamentoService departamento1Service = DepartamentoServiceUtil.CreateDepartamentoService();
			departamento1Service.AddDepartamentos(departamento1, departamentos);
			
            departamento1Service = DepartamentoServiceUtil.CreateDepartamentoService();
            departamento1Service.RemoveDepartamentos(departamentos, departamentos);

            IDepartamentoService departamentosService = DepartamentoServiceUtil.CreateDepartamentoService();
            departamentos = departamentosService.Find(departamentos.Id);
			Assert.IsTrue(departamentos.Departamento1 == null);
        }


        [TestMethod()]
        public void AddUsuariosTest()
        {
            Departamento pertenecenA = DepartamentoServiceUtil.CreateDepartamento();
            Usuario usuarios = UsuarioServiceUtil.CreateUsuario();

            IDepartamentoService pertenecenAService = DepartamentoServiceUtil.CreateDepartamentoService();
            pertenecenAService.AddUsuarios(pertenecenA, usuarios);

            IUsuarioService usuariosService = UsuarioServiceUtil.CreateUsuarioService();
            usuarios = usuariosService.Find(usuarios.Id);
			
            Assert.IsTrue(usuarios.PertenecenA.Any(t => pertenecenA.Id == t.Id));
        }
		
        [TestMethod()]
        public void RemoveUsuariosTest()
        {
            Departamento pertenecenA = DepartamentoServiceUtil.CreateDepartamento();
            Usuario usuarios = UsuarioServiceUtil.CreateUsuario();

			IDepartamentoService pertenecenAService = DepartamentoServiceUtil.CreateDepartamentoService();
			pertenecenAService.AddUsuarios(pertenecenA, usuarios);
			
            pertenecenAService = DepartamentoServiceUtil.CreateDepartamentoService();
            pertenecenAService.RemoveUsuarios(pertenecenA, usuarios);

            IUsuarioService usuariosService = UsuarioServiceUtil.CreateUsuarioService();
            usuarios = usuariosService.Find(usuarios.Id);

            Assert.IsTrue(!usuarios.PertenecenA.Any(t => pertenecenA.Id == t.Id));
        }
		#endregion Departamento Test

	}
}
