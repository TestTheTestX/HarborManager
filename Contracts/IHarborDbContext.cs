using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace HarborManager.Contracts
{
    public interface IHarborDbContext 
    {
        DbSet<BookingDTO> Bookings { get; set; }
        DbSet<BoatDTO> Boats { get; set; }
        DbSet<DockDTO> Docks { get; set; }
        int Complete();
    }
}