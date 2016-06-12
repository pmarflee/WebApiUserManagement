using AutoMapper;
using System.Collections.Generic;

namespace WebApiUserManagement.Mapping
{
    public static class DynamicListMapper
    {
        public static IList<T> Map<T>(dynamic input, IMapper mapper)
        {
            var results = new List<T>();

            foreach (var item in input)
            {
                results.Add(mapper.Map<T>(item));
            }

            return results;
        }
    }
}