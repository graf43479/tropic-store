using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace WebUI.Models
{
    public class UserRoleViewModel
    {
        public int UserRoleID { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Введите логин")]
        public string Login { get; set; }

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

        public int RoleID { get; set; }
        public int UserID { get; set; }

        public string RoleName { get; set; }

        public int SelectedRoleID { get; set; }

        public IEnumerable<Role> Roles { get; set; }
    }
}