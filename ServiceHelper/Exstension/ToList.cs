using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHelper.Exstension
{
    public static class ToList
    {
        public static List<T> ToListService<T>(this T[] obj )
        {
            List<T> result = new List<T>();

            foreach (var item in obj)
            {
                result.Add(item);
            }

            return result;
        }

    }
}
