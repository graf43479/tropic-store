using System;
using System.ComponentModel.DataAnnotations;


namespace Domain.Entities
{
    public class NLog_Error
    {
        [Key]
        public int ID { get; set; }

        //[DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)] //не работает
        public DateTime Time_stamp { get; set; }

        public string Host { get; set; }

        public string Type { get; set; }

        public string Source { get; set; }

        public string Message { get; set; }

        public string Level { get; set; }

        public string Logger { get; set; }

        public string Stacktrace { get; set; }

        public string Allxml { get; set; }
    
    }
}
