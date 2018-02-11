using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ARQ.Maqueta.Entities;

namespace ARQ.Maqueta.Services.Tests
{
	// <summary>
    ///Se trata de una clase de prueba para IUsuarioService y se pretende que
    ///contenga todas las pruebas unitarias IUsuarioService.
    ///</summary>
    [TestClass()]
    public partial class UsuarioServiceTest : ServiceTest
    {
		#region Usuario Test
				
        [TestMethod()]
        public void FindUsuarioTest()
        {
			IUsuarioService target = UsuarioServiceUtil.CreateUsuarioService();
            Usuario expected = UsuarioServiceUtil.CreateUsuario(); 
            Usuario actual = target.Find(expected.Id);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void FindAllUsuarioTest()
        {
			IUsuarioService target = UsuarioServiceUtil.CreateUsuarioService();  
            int expected = target.SearchCount("");  
            List<Usuario> actual = target.FindAll();
            Assert.AreEqual(expected, actual.Count);
        }
	
        [TestMethod()]
        public void SearchPagedUsuarioTest()
        {
            IUsuarioService target = UsuarioServiceUtil.CreateUsuarioService(); 
            int pageIndex = 1; 
            int pageCount = 10;
			Usuario expected = UsuarioServiceUtil.CreateUsuarioFull();
            List<Usuario> actual = target.Search(expected.Dni, pageIndex, pageCount);
			int count = target.SearchCount(expected.Dni);
			Assert.IsTrue(actual.Count <= count);
			Assert.IsTrue(actual.Count <= 10);
        }

        [TestMethod()]
        public void AddUsuarioTest()
        {
            IUsuarioService target = UsuarioServiceUtil.CreateUsuarioService(); 
           
			Usuario elementAdded = UsuarioServiceUtil.CreateUsuario();

			elementAdded = target.Find(elementAdded.Id);
			Assert.IsNotNull(elementAdded);
        }

        [TestMethod()]
        public void ChangeUsuarioTest()
        {
			Usuario usuario = UsuarioServiceUtil.CreateUsuario();

            IUsuarioService target = UsuarioServiceUtil.CreateUsuarioService();
            usuario = target.Find(usuario.Id);
			
			usuario.Nombre = "Changed Nombre";
			target.Change(usuario);

			target = UsuarioServiceUtil.CreateUsuarioService();
            usuario = target.Find(usuario.Id);

            Assert.IsTrue(usuario.Nombre == "Changed Nombre");
        }

        [TestMethod()]
        public void RemoveUsuarioTest()
        {
            IUsuarioService target = UsuarioServiceUtil.CreateUsuarioService(); 
            Usuario pUsuario = UsuarioServiceUtil.CreateUsuario();
            target.Remove(pUsuario);
            pUsuario = target.Find(pUsuario.Id);
			Assert.IsNull(pUsuario);
        }
		

        [TestMethod()]
        public void AddPertenecenATest()
        {
            Usuario usuarios = UsuarioServiceUtil.CreateUsuario();
            Departamento pertenecenA = DepartamentoServiceUtil.CreateDepartamento();

            IUsuarioService usuariosService = UsuarioServiceUtil.CreateUsuarioService();
            usuariosService.AddPertenecenA(usuarios, pertenecenA);

            IDepartamentoService pertenecenAService = DepartamentoServiceUtil.CreateDepartamentoService();
            pertenecenA = pertenecenAService.Find(pertenecenA.Id);
			
            Assert.IsTrue(pertenecenA.Usuarios.Any(t => usuarios.Id == t.Id));
        }
		
        [TestMethod()]
        public void RemovePertenecenATest()
        {
            Usuario usuarios = UsuarioServiceUtil.CreateUsuario();
            Departamento pertenecenA = DepartamentoServiceUtil.CreateDepartamento();

			IUsuarioService usuariosService = UsuarioServiceUtil.CreateUsuarioService();
			usuariosService.AddPertenecenA(usuarios, pertenecenA);
			
            usuariosService = UsuarioServiceUtil.CreateUsuarioService();
            usuariosService.RemovePertenecenA(usuarios, pertenecenA);

            IDepartamentoService pertenecenAService = DepartamentoServiceUtil.CreateDepartamentoService();
            pertenecenA = pertenecenAService.Find(pertenecenA.Id);

            Assert.IsTrue(!pertenecenA.Usuarios.Any(t => usuarios.Id == t.Id));
        }
		#endregion Usuario Test

	}
}
