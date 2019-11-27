using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Domain.Entities
{
        //[Table(Name = "tblRoles")]
        public class Role
        {
           
            public int RoleID { get; set; }

            [Required(ErrorMessage = "Пожалуйста, введите название роли")]
            public string RoleName { get; set; }


            public virtual ICollection<UserRole> UserRoles { get; set; }

         //   public virtual User User { get; set; }
           
         //   public string RoleDescription { get; set; }
        }

        
    }

