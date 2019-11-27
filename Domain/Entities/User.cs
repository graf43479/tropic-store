using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Domain.Entities
{
    public class User
    {
        public int UserID { get; set; }
        
        [Required(ErrorMessage = "Введите имя")]
        public string UserName { get; set; }
        
        [Required(ErrorMessage = "Введите логин")]
        public string Login { get; set; }
        
        [Required(ErrorMessage = "Введите пароль")]
        [MinLength(7, ErrorMessage = "Длинна пароля должна быть более 6 символов")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Введите email")]
        [RegularExpression(@"^[a-zA-Z0-9.-]{1,20}@[a-zA-Z0-9.-]{1,20}\.[A-Za-z]{2,4}", ErrorMessage = "Неверный формат Email")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Введите номер телефона")]
        public string Phone { get; set; }

        //[Required(ErrorMessage = "*")]
        public bool IsActive { get; set; }

        //[Required(ErrorMessage = "*")]
        [DataType(DataType.Date)]
        // [CheckDateInPast(ErrorMessage = "Your date cannot be in the future.")]
//        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; }

        public bool Mailing { get; set; }

        public string PasswordSalt { get; set; }
        [DefaultValue(false)]
        public bool IsActivated { get; set; }

        public string NewEmailKey { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }

       // public virtual Role Role { get; set; }

        //public virtual Role Role { get; set; }
    }
}


