using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using WebApiMimic.Models;

namespace WebApiMimic.Database
{
    public class MimicContext:DbContext
    {
        public MimicContext(DbContextOptions<MimicContext> options) : base(options)
        {

        }
        public DbSet<Palavra> Palavras { get; set; }
    }
}
