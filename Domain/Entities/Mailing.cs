using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Domain.Entities
{
    public class Mailing
    {
        [Key]
        public int MailingID { get; set; }

         
        public DateTime UpdateDate { get; set; }

         [Required(ErrorMessage = "Введите тему рассылки")]
         [MaxLength(150, ErrorMessage = "Количество символов в заголовке не должно превышать 300 ")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Напишите тело письма")]
        public string Body { get; set; }

        public bool IsSent { get; set; }
    }
}
