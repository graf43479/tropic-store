﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Security;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();

        User GetUserByID(int id);

        User GetUserByName(string login);



        void CreateUser(string userName, string login, string password, string email, string phone, bool isActivated, bool mailing, string passwordSalt);

        bool ValidateUser(string login, string password);

        void SaveUser(User user);
 
        string GetUserNameByEmail(string email); //----???????

        MembershipUser GetMembershipUserByName(string login);

        IQueryable<User> UsersInfo { get; }

        void DeleteUser(User user);

        bool ActivateUser(string username, string key);

        //
        bool AdminExists();

        void CreateRole(string roleName);

        //roles
        void AddUserToRole(string username, string roleName);

        bool IsUserInRole(string username, string roleName);

        void RemoveUserFromRole(string username, string roleName);

        string[] GetRolesForUser(string username);

        IQueryable<Role> Roles { get; }

        IQueryable<UserRole> UserRoles { get; }

        void DeleteRole(Role role);

        void SaveRole(Role role);

       // Task<IEnumerable<User>> GetUserListAsync();

   //     Task<User> GetUserByLoginAsync(string login);

     //   Task<User> GetUserByIDAsync(int userID);

        /*  Task<IEnumerable<UserRole>> GetUserRoleListAsync();

        Task<IEnumerable<Role>> GetRoleListAsync();*/

    }
}
