using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Security;
using Domain.Abstract;
using Domain.Entities;
using EntityState = System.Data.Entity.EntityState;

namespace Domain.Concrete
{
    public class EFUserRepository : IUserRepository
    {
        private EFDbContext context;

        public EFUserRepository(EFDbContext context)
        {
            this.context = context;
        }

        public IQueryable<User> UsersInfo
        {
            get { return context.Users.Include("UserRoles"); }
        }

        public void DeleteUser(User user)
        {
            context.Users.Remove(user);
            context.SaveChanges();
        }


        public IEnumerable<User> GetUsers()
        {
            return context.Users;
        }

        public User GetUserByID(int id)
        {
            return context.Users.FirstOrDefault(x => x.UserID == id);
        }

        public User GetUserByName(string login)
        {
            return context.Users.FirstOrDefault(x => x.Login == login);
        }

        public void CreateUser(string userName, string login, string password, string email, string phone,
                               bool isActivated, bool mailing, string passwordSalt)
        {
            User user = new User
                {
                    UserName = userName,
                    Login = login,
                    Email = email,
                    Phone = phone,
                    Created = DateTime.Now,
                    IsActive = true,
                    Mailing = mailing,
                    IsActivated = isActivated,
                    PasswordSalt = passwordSalt,
                    Password = password,
                    NewEmailKey = GenerateKey()
                };

            SaveUser(user);

        }

        public bool ValidateUser(string login, string password)
        {
            User user = context.Users.FirstOrDefault(x => x.Login.TrimEnd() == login);
            if (user != null && user.Password.TrimEnd() == password)
                //if (user != null && user.Password.TrimEnd() == )
                return true;
            return false;
        }

        public void SaveUser(User user)
        {
            if (user.UserID == 0)
                context.Users.Add(user);

            else
                context.Entry(user).State = EntityState.Modified;

            context.SaveChanges();
        }

        public string GetUserNameByEmail(string email)
        {
            User user = context.Users.FirstOrDefault(x => x.Email == email);
            return user != null ? user.Login : "";
        }

        public MembershipUser GetMembershipUserByName(string login)
        {
            User user = context.Users.FirstOrDefault(x => x.Login == login);
            if (user != null)
            {
                return new MembershipUser(
                    "CustomMembershipProvider",
                    user.Login,
                    user.UserID,
                    user.Email,
                    "",
                    null,
                    true,
                    false,
                    user.Created,
                    DateTime.Now,
                    DateTime.Now,
                    DateTime.Now,
                    DateTime.Now
                    );
            }
            return null;
        }

        //Генерация токена для завершения регистарции
        private static string GenerateKey()
        {
            Guid emailKey = Guid.NewGuid();

            return emailKey.ToString();
        }


        //активация юзера 
        public bool ActivateUser(string username, string key)
        {
            try
            {
                User user = UsersInfo.FirstOrDefault(x => x.Login == username);
                if (user.NewEmailKey == key)
                {
                    user.IsActivated = true;
                    user.NewEmailKey = null;

                    context.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                   return false;
            }

        }

        public bool AdminExists()
        {
            //var user = context.Users.FirstOrDefault(x => x.Login == username);
            try
            {
                var role = context.Roles.FirstOrDefault(x => x.RoleName == "admin");
                if (role != null)
                {
                    var userRole = context.UserRoles.FirstOrDefault(x => x.RoleID == role.RoleID);
                    var user = context.Users.Where(x => x.UserID == userRole.UserID);
                    
                    if (user.Count() == 0)
                    {
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception)
            {

                return false;
            }
            
            return false;
        }

        public void CreateRole(string roleName)
        {
           // CreateRole(roleName);
            var roleExists = context.Roles.FirstOrDefault(x => x.RoleName == roleName);
            if (roleExists==null)
            {
                Role role = new Role()
                    {
                        RoleName = roleName
                    };
                context.Roles.Add(role);
                context.SaveChanges();
            }
      /*      else
            {
                roleExists.RoleName = roleName;
                context.Entry(roleExists).State = EntityState.Modified;
            }
            context.SaveChanges();
            */
            /*
              if (category.CategoryID == 0)
            {

                    try
                    {
                        category.Sequence = context.Categories.Select(x => x.Sequence).Max() + 1;
                    }
                    catch (Exception)
                    {
                        category.Sequence = 1;
                    }
                
                if (category.ShortName == null)
                   
                category.ShortName = GetShortName(category.Name, context.Categories.Max(x => x.CategoryID)+1);
                
                context.Categories.Add(category);
            }
            else
            {
                context.Entry(category).State = EntityState.Modified;
            }
            context.SaveChanges();
             */

        }

        public void AddUserToRole(string username, string roleName)
        {
            User user = context.Users.FirstOrDefault(x => x.Login == username);
            Role role = context.Roles.FirstOrDefault(x => x.RoleName == roleName);
            UserRole userRole = context.UserRoles.FirstOrDefault(x => x.UserID == user.UserID);
            if (userRole == null)
                {
                    UserRole ur = new UserRole();
                    ur.RoleID = role.RoleID;
                    ur.UserID = user.UserID;
                 //   context.UserRoles.InsertOnSubmit(ur);
                    context.UserRoles.Add(ur);
                }
            else
            {
                userRole.RoleID = role.RoleID;
                context.Entry(userRole).State = EntityState.Modified;
            }
            context.SaveChanges();
            
        }

        public bool IsUserInRole(string username, string roleName)
        {
            var user = context.Users.FirstOrDefault(x => x.Login == username);
            var role = context.Roles.FirstOrDefault(x => x.RoleName == roleName);

            var userRole = context.UserRoles.FirstOrDefault(x => x.UserID == user.UserID && x.RoleID == role.RoleID);

            if (userRole != null)
            {
                return false;
            }
            return true;
        }

        public void RemoveUserFromRole(string username, string roleName)
        {
            var user = context.Users.FirstOrDefault(x => x.Login == username);
            var role = context.Roles.FirstOrDefault(x => x.RoleName == roleName);

            var userRole = context.UserRoles.FirstOrDefault(x => x.UserID == user.UserID && x.RoleID == role.RoleID);

            userRole.RoleID = 0;
            context.Entry(userRole).State = EntityState.Modified;
            context.SaveChanges();
        }

        public string[] GetRolesForUser(string username)
        {
            try
            {
                IList<string> roleNames = new List<string>();

                var user = context.Users.FirstOrDefault(x => x.Login == username);

                if (context.UserRoles.Where(x => x.UserID == user.UserID).Count() == 0)
                {
                    return null;
                }
                var userRoles = context.UserRoles.Where(x => x.UserID == user.UserID).ToList();

                if (userRoles.Count > 0)
                {
                    foreach (var r in userRoles)
                    {
                        roleNames.Add(r.Role.RoleName);
                    }
                }

                return roleNames.ToArray();
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public IQueryable<Role> Roles { get { return context.Roles.Include("UserRoles"); } }

        public IQueryable<UserRole> UserRoles { get { return context.UserRoles.Include("User").Include("Role"); } }
      
        public void DeleteRole(Role role)
        {
            context.Roles.Remove(role);
            context.SaveChanges();
        
        }



        public void SaveRole(Role role)
        {
            if (role.RoleID == 0)
            {
                context.Roles.Add(role);
            }
            else
            {
                context.Entry(role).State = EntityState.Modified;
            }
            context.SaveChanges();
        }

        //public async Task<IEnumerable<User>> GetUserListAsync()
        //{
        //    return await context.Users.ToListAsync().ConfigureAwait(false);
        //}


        //public async Task<User> GetUserByLoginAsync(string login)
        //{
        //    try
        //    {
        //        return await context.Users.FirstOrDefaultAsync(x => x.Login == login).ConfigureAwait(false);
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }

        //}

        //public async Task<User> GetUserByIDAsync(int userID)
        //{
        //        return await context.Users.FirstOrDefaultAsync(x => x.UserID == userID).ConfigureAwait(false);
        //}
     /*   public async Task<IEnumerable<UserRole>> GetUserRoleListAsync()
        {
            return await context.UserRoles.ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<Role>> GetRoleListAsync()
        {
            return await context.Roles.ToListAsync().ConfigureAwait(false);
        }*/
    }


}
