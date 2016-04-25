using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineDietManager.Domain
{
    public static class MyApp
    {
        private static void Main()
        {
            using (var dbCont = new OnlineDietManagerContext())
            {
                var v = dbCont.Courses.Where(c => c.ID == 2);
                foreach (var item in v)
                {
                    Console.WriteLine(item);
                }
            }
        }
    }
}
