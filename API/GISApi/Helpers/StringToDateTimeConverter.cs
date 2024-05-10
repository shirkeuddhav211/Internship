using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GISApi.Helpers
{
    public class StringToDateTimeConverter : ITypeConverter<string, DateTime>
    {
        public DateTime Convert(string source, DateTime destination, ResolutionContext context)
        {
            object objDateTime = source;
            DateTime dateTime;

            if (objDateTime == null)
            {
                return default(DateTime);
            }

            if (DateTime.TryParse(objDateTime.ToString(), out dateTime))
            {
                return dateTime;
            }

            return default(DateTime);
        }
    }
}
