using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InviteService.Models
{

    public class InviteContext : DbContext
    {
        public InviteContext(DbContextOptions<InviteContext> options) : base(options) 
        {

        }
        public DbSet<Invite> Invites { get; set; }
    }
}
