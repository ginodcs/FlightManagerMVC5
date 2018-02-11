using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ARQ.Maqueta.Entities;

namespace ARQ.Maqueta.Services.Tests
{
    public partial class UsuarioServiceUtil
    {
		static internal IUsuarioService CreateUsuarioService()
        {
            IUsuarioService target = new UsuarioService(DataContextUtils.GetDataContext());
            return target;
        }
			#region Usuario Test
 
        public static Usuario CreateUsuario()
        {
            Usuario result = GetUsuario();

            IUsuarioService service = CreateUsuarioService();
			service.Add(result);

			result = service.Find(result.Id);
            return result;
        }
		
		public static Usuario CreateUsuarioFull()
        {
            Usuario result = GetUsuarioFull();

            IUsuarioService service = CreateUsuarioService();
			service.Add(result);

			result = service.Find(result.Id);
            return result;
        }
				
        public static Usuario GetUsuario()
        {
            Usuario result = new Usuario();
            result.Id = new Random().Next(32000);
            result.Nombre = Guid.NewGuid().ToString();
            result.Apellidos = Guid.NewGuid().ToString();
            result.Dni = Guid.NewGuid().ToString();
            result.Activo = true;
 
            return result;
            
        }
		
        public static Usuario GetUsuarioFull()
        {
            Usuario result = GetUsuario();
            result.FechaNacimiento = DateTime.Now;
 
            return result;
            
        }
		
		#endregion Usuario Test
	

	}
}
