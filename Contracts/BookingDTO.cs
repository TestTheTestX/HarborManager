using System;
using System.Collections.Generic;
using System.Text;

namespace HarborManager.Contracts
{
    public class BookingDTO
    {
        public int Id { get; set; }
        public int BoatId { get; set; }
        public int DockId { get; set; }
        public string Arrival { get; set; }
        public string Departure { get; set; }
    }
}
