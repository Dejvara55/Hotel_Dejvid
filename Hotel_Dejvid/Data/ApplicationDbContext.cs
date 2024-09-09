using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Hotel_Dejvid.Models;

namespace Hotel_Dejvid.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Hotel_Dejvid.Models.Gost> Gost { get; set; } = default!;
        public DbSet<Hotel_Dejvid.Models.Placanje> Placanje { get; set; } = default!;
        public DbSet<Hotel_Dejvid.Models.Rezervacija> Rezervacija { get; set; } = default!;
        public DbSet<Hotel_Dejvid.Models.Soba> Soba { get; set; } = default!;
        public DbSet<Hotel_Dejvid.Models.Zaposleni> Zaposleni { get; set; } = default!;
    }
}
