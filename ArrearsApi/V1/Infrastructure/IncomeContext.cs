using Microsoft.EntityFrameworkCore;

namespace ArrearsApi.V1.Infrastructure
{

    public class IncomeContext : DbContext
    {       
        //Guidance on the context class can be found here https://github.com/LBHackney-IT/lbh-arrears-api/wiki/IncomeContext
        public IncomeContext(DbContextOptions<IncomeContext> options) : base(options)
        {
        }

        public DbSet<EvictionsEntity> EvictionsEntities { get; set; }
    }
}
