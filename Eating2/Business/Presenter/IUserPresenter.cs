using Eating2.Business.ViewModels;
using Eating2.DataAcess.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eating2.Business.Presenter
{
    public interface IUserPresenter
    {
        Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
        Task<ApplicationUser> FindByIdAsync(string userId);
        Task<SignInStatus> PasswordSignInAsync(string email, string passWord, bool rememberMe, bool shouldLockOut);
        Task SignInAsync(ApplicationUser user, bool isPersistent, bool rememberBrowser);
        void SignOut();
        Task<IdentityResult> CreateAsync(RegisterViewModel model);
        bool HasPassword();
        Task<string> GetPhoneNumberAsync(string userId);
        Task<bool> GetTwoFactorEnabledAsync(string userId);
        Task<IList<UserLoginInfo>> GetLoginsAsync(string userId);
        Task<bool> TwoFactorBrowserRememberedAsync(string userId);
        IndexViewModel FindUserByID(string UserId);
        IndexViewModel FindUserByUserName(string UserName);
        string[] GetRolesForUser(string userID);

    }
}