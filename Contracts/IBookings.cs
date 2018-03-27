using System;
using System.Collections.Generic;
using System.Text;

namespace HarborManager.Contracts
{
    public interface IBookings
    {
        IEnumerable<BookingDTO> GetAll();
        BookingDTO GetById(int id);
        int Add(BookingDTO booking);
        int Update(BookingDTO booking);
        void DeleteAll();
        void DeleteById(int id);
    }
}
