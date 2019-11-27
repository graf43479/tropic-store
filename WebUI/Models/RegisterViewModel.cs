﻿using System;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using System.Web.Mvc;


namespace WebUI.Models
{
    public class RegisterViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int UserID { get; set; }

        /*[PlaceHolder("Enter title here")]
        [Display(Prompt = "numbers only")]*/
        [Required(ErrorMessage = "Необходимо корректно заполнить поле Имя")]
        public string UserName { get; set; } 
        
        [Required(ErrorMessage = "Необходимо корректно заполнить поле Логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Необходимо корректно заполнить поле Пароль")]
        [DataType(DataType.Password)]
        [MinLength(7, ErrorMessage = "Длина пароля должна быть более 6 символов")]
        public string Password { get; set; }
        
       // [Required(ErrorMessage = "Длина пароля должна быть более 6 символов")]
        [MinLength(7, ErrorMessage = "Длина пароля должна быть более 6 символов")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Пароль подтвержден неверно")]
        public string ConfirmPassword { get; set; }

      //  [Required(ErrorMessage = "Старый пароль указан неверно!")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }


        [Required(ErrorMessage = "Неверно указан номер телефона")]
        public string Phone { get; set; }
        
        [Required(ErrorMessage = "Необходимо заполнить поле Email")]
        [RegularExpression(@"^[a-zA-Z0-9.-]{1,20}@[a-zA-Z0-9.-]{1,20}\.[A-Za-z]{2,4}", ErrorMessage = "Неверный формат Email")]
        public string Email { get; set; }


        public DateTime Created { get; set; }

        public bool Mailing { get; set; }

        public string PasswordSalt { get; set; }
        [DefaultValue(false)]
        public bool IsActivated { get; set; }
    }
}


 
