using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class UserRole
    {
        //[Column(IsPrimaryKey = true, AutoSync = AutoSync.OnInsert, IsDbGenerated = true)]
        [Key]
        public int UserRoleID { get; set; }

        public int UserID { get; set; }

        public int RoleID { get; set; }

        public virtual Role Role { get; set; }

        public virtual User User { get; set; }

        /*private EntityRef<Role> _Role;
        //[Association(Storage = "_Role", ThisKey = "RoleId", OtherKey = "RoleId")]
        public Role Role
        {
            set
            {
                _Role.Entity = value;
            }
            get
            {
                return _Role.Entity;
            }
        }*/
    }
}
