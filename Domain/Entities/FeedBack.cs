using System.ComponentModel.DataAnnotations;


namespace Domain.Entities
{
    public class FeedBack
    {
         [Required(ErrorMessage="Пожалуйста введите имя")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Введите email")]
        [RegularExpression(@"^[a-zA-Z0-9.-]{1,20}@[a-zA-Z0-9.-]{1,20}\.[A-Za-z]{2,4}", ErrorMessage = "Неверный формат Email")]
        public string FeedBackEmail { get; set; }
        //public string ShippingCapcha { get; set; } ????!!!!!!!!!!!!

        [Required(ErrorMessage = "Пожалуйста введите Ваш вопрос")]
        public string Question { get; set; }
    }
}