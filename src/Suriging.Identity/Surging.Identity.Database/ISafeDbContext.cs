using Microsoft.EntityFrameworkCore;
using Surging.Identity.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surging.Identity.Database
{
    public interface ISafeDbContext
    {
        DbSet<SurgingUser> Users { get; set; }
    }
}
