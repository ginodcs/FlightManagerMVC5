using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ARQ.Maqueta.Entities;

namespace ARQ.Maqueta.Services.Tests
{
    public partial class DepartamentoServiceUtil
    {
		static internal IDepartamentoService CreateDepartamentoService()
        {
            IDepartamentoService target = new DepartamentoService(DataContextUtils.GetDataContext());
            return target;
        }
			#region Departamento Test
 
        public static Departamento CreateDepartamento(Departamento departamento1 = null)
        {
            Departamento result = GetDepartamento();
			
			if (departamento1 != null)
            {
                result.Departamento1 = departamento1;
            }

            IDepartamentoService service = CreateDepartamentoService();
			service.Add(result);

			result = service.Find(result.Id);
            return result;
        }
		
		public static Departamento CreateDepartamentoFull(Departamento departamento1 = null)
        {
            Departamento result = GetDepartamentoFull();
			
			if (departamento1 != null)
            {
                result.Departamento1 = departamento1;
            }

            IDepartamentoService service = CreateDepartamentoService();
			service.Add(result);

			result = service.Find(result.Id);
            return result;
        }
				
        public static Departamento GetDepartamento()
        {
            Departamento result = new Departamento();
            result.Id = new Random().Next(32000);
            result.Nombre = Guid.NewGuid().ToString();
 
            return result;
            
        }
		
        public static Departamento GetDepartamentoFull()
        {
            Departamento result = GetDepartamento();
        	result.Departamento1 = DepartamentoServiceUtil.CreateDepartamento(); 
			//No es optional
        	result.Departamento1Id = result.Departamento1.Id; 
 
            return result;
            
        }
		
		#endregion Departamento Test
	

	}
}
