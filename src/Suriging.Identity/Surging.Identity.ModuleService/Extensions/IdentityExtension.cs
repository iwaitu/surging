using Surging.Identity.Core.Models;
using Surging.Identity.IModuleService.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surging.Identity.ModuleService.Extensions
{
    public static class IdentityExtension
    {
        public static UserModel ToModel(this SurgingUser user)
        {
            var um = new UserModel();
            um.Id = user.Id;
            um.BACKPIC = user.BACKPIC;
            um.BDZP = user.BDZP;
            um.CreateDate = user.CreateDate;
            um.ExtenAppUserIdentity = user.ExtenAppUserIdentity;
            um.FRONTPIC = user.FRONTPIC;
            um.HeaderImage = user.HeaderImage;
            um.IsHumanReadName = user.IsHumanReadName;
            um.IsRealName = user.IsRealName;
            um.LastLoginDate = user.LastLoginDate;
            um.OpenId = user.OpenId;
            um.PeopleWorkCertification = user.PeopleWorkCertification;
            um.PersonNo = user.PersonNo;
            um.UpdateDate = user.UpdateDate;
            um.UserName = user.UserName;
            um.VIDEO = user.VIDEO;
            return um;

        }
    }
}
