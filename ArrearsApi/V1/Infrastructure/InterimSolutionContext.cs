using Microsoft.EntityFrameworkCore;

namespace ArrearsApi.V1.Infrastructure
{

    public class InterimSolutionContext : DbContext
    {
        //Guidance on the context class can be found here https://github.com/LBHackney-IT/lbh-arrears-api/wiki/InterimSolutionContext
        public InterimSolutionContext(DbContextOptions<InterimSolutionContext> options) : base(options)
        {
        }

        public DbSet<BatchLogEntity> BatchLogEntities { get; set; }
    }
}
