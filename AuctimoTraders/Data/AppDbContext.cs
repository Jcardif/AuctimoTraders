using System;
using AuctimoTraders.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuctimoTraders.Data
{
    /// <summary>
    ///     Represents the Database structure with the DbSet properties representing the tables
    /// </summary>

    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        /// <summary>
        ///     Represents the Country Table defined by <see cref="Country"/>
        /// </summary>
        public DbSet<Country> Countries { get; set; }

        /// <summary>
        ///     Represents the ItemTypes Table defined by <see cref="ItemType"/>
        /// </summary>
        public DbSet<ItemType> ItemTypes { get; set; }

        /// <summary>
        ///     Represents the Regions Table defined by <see cref="Region"/>
        /// </summary>
        public DbSet<Region> Regions { get; set; }

        /// <summary>
        ///     Represents the Sales Table defined by <see cref="Sale"/>
        /// </summary>
        public DbSet<Sale> Sales { get; set; }
        

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="options"></param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<AppUser>(entity => { entity.ToTable("AppUsers", "Security"); });

            builder.Entity<AppRole>(entity => { entity.ToTable("AppRoles", "Security"); });

            builder.Entity<IdentityUserClaim<Guid>>(entity => { entity.ToTable("UserClaims", "Security"); });

            builder.Entity<IdentityUserLogin<Guid>>(entity => { entity.ToTable("UserLogins", "Security"); });

            builder.Entity<IdentityRoleClaim<Guid>>(entity => { entity.ToTable("RoleClaims", "Security"); });

            builder.Entity<IdentityUserRole<Guid>>(entity => { entity.ToTable("UserRoles", "Security"); });


            builder.Entity<IdentityUserToken<Guid>>(entity => { entity.ToTable("UserTokens", "Security"); });
        }
    }
}
