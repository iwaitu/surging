using Surging.Core.Caching;
using Surging.Core.CPlatform.Runtime.Server.Implementation.ServiceDiscovery.Attributes;
using Surging.Core.System.Intercept;
using Surging.Identity.IModuleService.ViewModels;
using System.Threading.Tasks;

namespace Surging.Identity.IModuleService
{
    public interface IIdentityService
    {
        Task<UserModel> Authentication(LoginModel requestData);

        [Service(Date = "2018-5-31", Director = "iwaitu", Name = "获取用户")]
        [InterceptMethod(CachingMethod.Get, Key = "GetUser_id_{0}", CacheSectionType = SectionType.ddlCache, Mode = CacheTargetType.Redis, Time = 480)]
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
