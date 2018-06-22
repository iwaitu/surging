using MessagePack;
using Surging.Core.System.Intercept;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Surging.Identity.IModuleService.ViewModels
{
    [MessagePackObject]
    public class UserModel
    {
        [CacheKey(1)]
        [Key(0)]
        public string Id { get; set; }
        [CacheKey(2)]
        [Key(1)]
        public string UserName { get; set; }
        [Key(2)]
        public string PersonNo { get; set; }

        [Key(3)]
        public string HeaderImage { get; set; }

        /// <summary>
        /// 是否实名   false:没有实名；  true： 已实名
        /// </summary>
        [Key(4)]
        public bool IsRealName { get; set; }

        [Key(5)]
        public string OpenId { get; set; }

        [Key(6)]
        public DateTime LastLoginDate { get; set; } = DateTime.Now;
        /// <summary>
        /// 比对照片
        /// </summary>

        [Key(7)]
        public string BDZP { get; set; }

        /// <summary>
        /// 身份证正面照片路径
        /// </summary>

        [Key(8)]
        public string FRONTPIC { get; set; }

        /// <summary>
        /// 身份证反面照片
        /// </summary>

        [Key(9)]
        public string BACKPIC { get; set; }

        /// <summary>
        /// 验证视频
        /// </summary>

        [Key(10)]
        public string VIDEO { get; set; }

        /// <summary>
        /// 第三方应用用户标识
        /// </summary>
        [Key(11)]
        public string ExtenAppUserIdentity { get; set; }

        /// <summary>
        /// 人工实名
        /// </summary>

        [Key(12)]
        public bool IsHumanReadName { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        [Key(13)]
        public string PeopleWorkCertification { get; set; }

        /// <summary>
        /// 用户注册时间
        /// </summary>

        [Key(14)]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        [Key(15)]
        public DateTime UpdateDate { get; set; } = DateTime.Now;

        [Key(16)]
        public ICollection<string> Roles { get; set; } = new List<string>();
    }
}
