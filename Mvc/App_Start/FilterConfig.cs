using System.Web;
using System.Web.Mvc;

namespace ARQ.Maqueta.Presentation.Mvc
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
         
            // Uncomment this line to make this application private:
            filters.Add(new AuthorizeAttribute());            
        }

    }
}