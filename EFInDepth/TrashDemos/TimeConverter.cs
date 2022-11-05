using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFInDepth.TrashDemos
{
    public static class TimeConverter
    {
        public static void GetCurrentTypeInfo()
        {
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            var egyptTimeZone = TimeZoneInfo.GetSystemTimeZones().FirstOrDefault(x => x.DisplayName.ToLower().Contains("cairo"));



            DateTime datNowLocal = DateTime.Now;
            Console.WriteLine("Converting {0}, Kind {1}:", datNowLocal, datNowLocal.Kind);
            
            Console.WriteLine("ConvertTimeToUtc: {0}, Kind {1}", TimeZoneInfo.ConvertTimeToUtc(datNowLocal), 
                TimeZoneInfo.ConvertTimeToUtc(datNowLocal).Kind);

            Console.WriteLine($"Back to the local time zone time : {TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneInfo)}");
            Console.WriteLine();
        }
    }
}
