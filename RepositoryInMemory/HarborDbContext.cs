using System;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using HarborManager.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HarborManager.RepositoryInMemory
{
    public class HarborDbContext : DbContext, IHarborDbContext
    {
        public HarborDbContext(DbContextOptions<HarborDbContext> options)
            : base(options)
        {
            //wird die Datenbank erstmalig initialisiert soll sie mit einigen Werten gefüllt sein
            if (Boats.Count()==0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    Boats.Add(new BoatDTO() {Id = i, Bezeichnung = "Boot" + i.ToString(), CheckedIn = false});
                }
                SaveChanges();            
            }
            if (Docks.Count() == 0)
            {
                for (int i = 1; i <= 5; i++)
                {
                    Docks.Add(new DockDTO() { Id = i, Bezeichnung = "Dock" + i.ToString() });
                }
                SaveChanges();
            }
        }



        public DbSet<BookingDTO> Bookings { get; set; }
        public DbSet<BoatDTO> Boats { get; set; }
        public DbSet<DockDTO> Docks { get; set; }

        public int Complete()
        {
            return SaveChanges();
        }
    }
}