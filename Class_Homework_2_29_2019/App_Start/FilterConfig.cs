using System.Web;
using System.Web.Mvc;

namespace Class_Homework_2_29_2019
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
