using Surging.Core.CPlatform.Runtime.Server.Implementation.ServiceDiscovery.Attributes;
using Surging.Identity.IModuleService.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Surging.Identity.IModuleService
{
    public interface IIdentityService
    {
        Task<UserModel> Authentication(LoginModel requestData);

        [Service(Date = "2018-5-31", Director = "iwaitu", Name = "获取用户")]
        Task<UserModel> GetUserInfo(string id);

        [Service(Date = "2018-5-31", Director = "iwaitu", Name = "检查证件号是否唯一")]
        Task<bool> CheckPersonNoUnqine(string id);

        [Service(Date = "2018-5-31", Director = "iwaitu", Name = "检查手机号是否唯一")]
        Task<bool> CheckPhonenumberUnqine(string id);


        [Service(Date = "2018-5-31", Director = "iwaitu", Name = "重置密码")]
        Task<bool> ResetPassword(string password1, string password2);

        [Service(Date = "2018-5-31", Director = "iwaitu", Name = "创建用户")]
        Task<UserModel> CreateUser(string username, string password);
    }


}
