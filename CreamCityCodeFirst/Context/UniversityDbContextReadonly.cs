using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CreamCityCodeFirst.Context
{
    internal class UniversityDbContextReadonly : UniversityDbContext
    {
        public UniversityDbContextReadonly(DbContextOptions<UniversityDbContextReadonly> options) : base(options) {
            this.ChangeTracker.AutoDetectChangesEnabled = false;
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public override int SaveChanges()
        {
            throw new NotImplementedException("Saving is not allowed in the readonly context");
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException("Saving is not allowed in the readonly context");
        }
    }
}
