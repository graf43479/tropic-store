using System;
using System.Web.Security;
using Domain.Abstract;
using Ninject;


namespace Domain
{
    public class PrimaryMembershipProvider: MembershipProvider
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger(); 
        
        [Inject]
        public IUserRepository usersRepository { get; set; }

        public MembershipCreateStatus CreateUser(string userName, string login, string password, string email, 
                                                 string phone, bool isActivated, bool mailing, string passwordSalt)
        {
            if(usersRepository.GetUserNameByEmail(email)!="")
                return MembershipCreateStatus.DuplicateEmail;

            MembershipUser user = usersRepository.GetMembershipUserByName(login);

            if (user!=null)
                return MembershipCreateStatus.DuplicateUserName;
            usersRepository.CreateUser(userName, login, password, email, phone, isActivated, mailing, passwordSalt);
            return MembershipCreateStatus.Success;
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            return false;
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { return true; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return true; }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string login, bool userIsOnline)
        {
            return usersRepository.GetMembershipUserByName(login);
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException();; }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return PasswordFormat; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string login, string password)
        {
         //   logger.Info("UserValidated");
            return usersRepository.ValidateUser(login.TrimEnd(), password.TrimEnd());
        }

      
    }


}


/*
 // // Свойства из web.config
private string _ApplicationName;
private bool _EnablePasswordReset;
private bool _EnablePasswordRetrieval = false;
private bool _RequiresQuestionAndAnswer = false;
private bool _RequiresUniqueEmail = true;
private int _MaxInvalidPasswordAttempts;
private int _PasswordAttemptWindow;
private int _MinRequiredPasswordLength;
private int _MinRequiredNonalphanumericCharacters;
private string _PasswordStrengthRegularExpression;
private MembershipPasswordFormat _PasswordFormat = MembershipPasswordFormat.Hashed;
 */