using Microsoft.EntityFrameworkCore;
using WebApiMimic.V1.Models;

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
