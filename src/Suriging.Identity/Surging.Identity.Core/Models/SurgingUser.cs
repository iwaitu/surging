using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Surging.Identity.Core.Models
{

    public class SurgingUser : IdentityUser
    {
        /// <summary>
        /// 真是姓名
        /// </summary>

        public string PersonName { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>

        public string PersonNo { get; set; }

        public string HeaderImage { get; set; }

        public string SecretHex { get; set; }

        /// <summary>
        /// 是否实名   false:没有实名；  true： 已实名
        /// </summary>
        public bool IsRealName { get; set; }

        public string OpenId { get; set; }

        public DateTime LastLoginDate { get; set; } = DateTime.Now;
        /// <summary>
        /// 比对照片
        /// </summary>

        public string BDZP { get; set; }

        /// <summary>
        /// 身份证正面照片路径
        /// </summary>

        public string FRONTPIC { get; set; }

        /// <summary>
        /// 身份证反面照片
        /// </summary>

        public string BACKPIC { get; set; }

        /// <summary>
        /// 验证视频
        /// </summary>

        public string VIDEO { get; set; }

        /// <summary>
        /// 第三方应用用户标识
        /// </summary>
        [System.ComponentModel.DataAnnotations.MaxLength(128)]

        public string ExtenAppUserIdentity { get; set; }

        /// <summary>
        /// 人工实名
        /// </summary>

        public bool IsHumanReadName { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        [MaxLength(32)]
        public string PeopleWorkCertification { get; set; }

        /// <summary>
        /// 用户注册时间
        /// </summary>

        public DateTime CreateDate { get; set; } = DateTime.Now;

        public DateTime UpdateDate { get; set; } = DateTime.Now;

        

    }
}
