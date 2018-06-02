using Microsoft.AspNetCore.Identity;
using System;

namespace Surging.Identity.Core.Models
{
    public class SurgingRole : IdentityRole
    {

        public DateTime CreateDate { get; set; } = DateTime.Now;

        public DateTime UpdateDate { get; set; } = DateTime.Now;
    }
}
