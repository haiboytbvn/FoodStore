using Eating2.Business.Presenter;
using Eating2.DataAcess.Repositories;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eating2.DataAcess.Models;
using Microsoft.Owin.Security;
using System.Security.Principal;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Eating2.Business.ViewModels;
using System.Threading.Tasks;
using Eating2.Exception;

namespace Eating2.Business.Presenter
{
    public class UserPresenter : IUserPresenter
    {
        #region Properties

        protected HttpContextBase HttpContext;
        protected ApplicationUserManager UserManager;
        protected ApplicationSignInManager SignInManager;
        protected IAuthenticationManager AuthenticationManager;
        protected IPrincipal User;
        protected RoleManager<IdentityRole> RoleManager;


        #endregion

        

        public UserPresenter(HttpContextBase context) : base()
        {
            HttpContext = context;
            UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            SignInManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            AuthenticationManager = HttpContext.GetOwinContext().Authentication;
            User = HttpContext.User;
            RoleManager = HttpContext.GetOwinContext().Get<ApplicationRoleManager>();

        }

       
        public Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            var user = UserManager.FindById(userId);
            if (user == null)
            {
               
                throw new NotFoundException();
            }
            return UserManager.ChangePasswordAsync(userId, currentPassword, newPassword);

        }
        public Task<ApplicationUser> FindByIdAsync(string userId)
        {
            return UserManager.FindByIdAsync(userId);
        }

        public Task<SignInStatus> PasswordSignInAsync(string email, string passWord, bool rememberMe, bool shouldLockOut)
        {

            return SignInManager.PasswordSignInAsync(email.Trim(), passWord, rememberMe, shouldLockOut);
        }

        public Task SignInAsync(ApplicationUser user, bool isPersistent, bool rememberBrowser)
        {
            return SignInManager.SignInAsync(user, isPersistent, rememberBrowser);
        }

        public void SignOut()
        {
            AuthenticationManager.SignOut();
        }

        public Task<IdentityResult> CreateAsync(RegisterViewModel model)
        {
            var user = new ApplicationUser { UserName = model.Email.Trim(), Email = model.Email.Trim() };

            return UserManager.CreateAsync(user, model.Password);
        }


        public bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }


        //ManagePresenter
        public Task<string> GetPhoneNumberAsync(string userId)
        {
            return UserManager.GetPhoneNumberAsync(userId);
        }


        public Task<bool> GetTwoFactorEnabledAsync(string userId)
        {
            return UserManager.GetTwoFactorEnabledAsync(userId);
        }


        public Task<IList<UserLoginInfo>> GetLoginsAsync(string userId)
        {
            return UserManager.GetLoginsAsync(userId);
        }


        public Task<bool> TwoFactorBrowserRememberedAsync(string userId)
        {
            return AuthenticationManager.TwoFactorBrowserRememberedAsync(userId);
        }

        //My Repo
        public IndexViewModel FindUserByID(string UserId)
        {
            var currentUser = UserManager.FindById(UserId);
            if (currentUser == null)
            {
                throw new NotFoundException();
            }
            
            IndexViewModel model = new IndexViewModel {  Email = currentUser.Email, DisplayName = currentUser.DisplayName };
            return model;
        }

        public IndexViewModel FindUserByUserName(string UserName)
        {
            var currentUser = UserManager.FindByName(UserName);
            if (currentUser == null)
            {
                throw new NotFoundException();
            }
            IndexViewModel model = new IndexViewModel { ID = currentUser.Id, Email = currentUser.Email, DisplayName = currentUser.DisplayName };
            return model;
        }

        //public UpdateDisplayNameViewModel GetCurrentUserById(string id)
        //{
        //    var currentUser = UserManager.FindById(id);
        //    if (currentUser == null)
        //    {
        //        throw new UserNotFoundException();
        //    }
        //    var viewModel = new UpdateDisplayNameViewModel { DisplayName = currentUser.DisplayName.Trim() };
        //    return viewModel;
        //}



        //public void UpdateDisplayNameInDB(string UserId, string NewDisplayName)
        //{

        //    var currentUser = UserManager.FindById(UserId);
        //    if (currentUser == null)
        //    {
        //        logger.Error("User was not found");
        //        throw new UserNotFoundException();
        //    }
        //    currentUser.DisplayName = NewDisplayName;
        //    UserManager.Update(currentUser);
        //    HttpContext.GetOwinContext().Get<ApplicationDbContext>().SaveChanges();
        //    FeedObservers(currentUser);
        //}



        //public string GetUserProfilePictureUrl(string id)
        //{
        //    var user = GetUserById(id);
        //    var folderPath = Path.Combine(UserConfigurations.PhotosFolderPath, user.Email, UserConfigurations.ProfileImageFileName);
        //    return folderPath;
        //}
        //public string GetUserProfilePictureUrlWithLastModifiedDate(string id)
        //{
        //    var user = GetUserById(id);
        //    var u = UserManager.GetRoles(id);
        //    var UrlPath = Path.Combine(UserConfigurations.PhotosFolderPath, user.Email, UserConfigurations.ProfileImageFileName + "?" + user.LastModifiedDate);
        //    return UrlPath;
        //}

        public string[] GetRolesForUser(string userID)
        {
            var rolesList = UserManager.GetRoles(userID);
            var roles = rolesList.ToArray();
            return roles;
        }
    } 
}