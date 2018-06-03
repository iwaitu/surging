using MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surging.Identity.IModuleService.ViewModels
{
    [MessagePackObject]
    public class LoginModel
    {
        [Key(0)]
        public string username { get; set; }
        [Key(1)]
        public string password { get; set; }
    }
}
