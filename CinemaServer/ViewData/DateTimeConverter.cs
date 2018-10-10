using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Web.ViewData
{
    //не работает
    public class DateTimeConverter : IsoDateTimeConverter
    {
        
        public DateTimeConverter() => base.DateTimeFormat = "dd.MM.yy HH:mm";


        
        public override object ReadJson(JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {

            DateTime? myDate = DateTime.ParseExact(reader.Value.ToString().Trim(' '), "dd.MM.yy HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            return myDate;

        }

    }
}
