using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HarborManager.Contracts;



namespace HarborManager.Logic
{
    public class Bookings : IBookings
    {

        private IHarborDbContext _harborDbContext;
        public Bookings(IHarborDbContext harborDbContext)
        { 
            _harborDbContext=harborDbContext;
        }

        public IEnumerable<BookingDTO> GetAll()
        {
            return _harborDbContext.Bookings.ToList();
        }

        public BookingDTO GetById(int id)
        {
            var item = _harborDbContext.Bookings.FirstOrDefault(t => t.Id == id);

            return item;
        }

        public int Add(BookingDTO booking)
        {
            if (booking.Id > 0)
            {
                throw new System.ArgumentException("Id darf nicht gesetzt sein");
            }

            ControllBooking(booking);

            _harborDbContext.Bookings.Add(booking);
            _harborDbContext.Complete();

            return booking.Id;
        }

        public int Update(BookingDTO booking)
        {

            if (booking.Id == 0)
            {
                throw new System.ArgumentException("Id muss gesetzt sein");
            }

            ControllBooking(booking);
            
            if(!_harborDbContext.Bookings.Any(b=> b.Id == booking.Id))
            {
                throw new System.ArgumentException("Id nicht vorhanden");
            }

            _harborDbContext.Bookings.Update(booking);
            _harborDbContext.Complete();

            return booking.Id;
        }

        public void DeleteAll()
        {
            _harborDbContext.Bookings.RemoveRange(_harborDbContext.Bookings);
            _harborDbContext.Complete();
        }

        public void DeleteById(int id)
        {
            BookingDTO booking = _harborDbContext.Bookings.FirstOrDefault(t => t.Id == id);
            _harborDbContext.Bookings.Remove(booking);
            _harborDbContext.Complete();
        }

        private void ControllBooking(BookingDTO booking)
        {
            if (booking == null)
            {
                throw new System.ArgumentNullException();
            }

            if (StringToDate(booking.Arrival) < DateTime.Now.Date | StringToDate(booking.Departure) < DateTime.Now.Date)
            {
                throw new System.ArgumentException("Buchung in der Vergangenheit nicht möglich");
            }

            if (StringToDate(booking.Departure)  < StringToDate(booking.Arrival) )
            {
                throw new System.ArgumentException("Ankunft muss vor der Abfahrt erfolgen");
            }

            if (StringToDate(booking.Departure)  == StringToDate(booking.Arrival) )
            {
                throw new System.ArgumentException("Mindestens 1 Tag Aufenthaltsdauer");
            }

            if (_harborDbContext.Boats.All(boat => boat.Id != booking.BoatId))

            {
                throw new System.ArgumentException("Das Boot ist unbekannt");
            }

            if (_harborDbContext.Docks.All(dock => dock.Id != booking.DockId))
            {
                throw new System.ArgumentException("Das Dock ist unbekannt");
            }

            //Es gibt schon eine andere Buchung von diesem Boot, welche in die Zeit der gewünschten Buchung fällt 
            if (_harborDbContext.Bookings.Any(b =>
                b.BoatId == booking.BoatId && b.Id != booking.Id &&
                (StringToDate(b.Departure) > StringToDate(booking.Arrival) && StringToDate(b.Departure) <= StringToDate(booking.Departure) ||
                 StringToDate(b.Arrival) < StringToDate(booking.Departure) && StringToDate(b.Arrival) >= StringToDate(booking.Arrival) ||
                 StringToDate(b.Arrival) <= StringToDate(booking.Arrival) && StringToDate(b.Departure) >= StringToDate(booking.Departure))))
            {
                throw new System.ArgumentException("Das Boot hat in diesen Zeitraum schon gebucht");
            }


            //Es gibt eine Buchung mit der geforderten DockId, welche in die Zeit der gewünschten Buchung fällt 
            if (_harborDbContext.Bookings.Any(b =>
                b.DockId == booking.DockId &&
                (StringToDate(b.Departure) > StringToDate(booking.Arrival) && StringToDate(b.Departure) <= StringToDate(booking.Departure) ||
                 StringToDate(b.Arrival) < StringToDate(booking.Departure) && StringToDate(b.Arrival) >= StringToDate(booking.Arrival) ||
                 StringToDate(b.Arrival) <= StringToDate(booking.Arrival) && StringToDate(b.Departure) >= StringToDate(booking.Departure))))
             {
                List<int> busyDockIds = _harborDbContext.Bookings.Where(b => StringToDate(b.Departure)  > StringToDate(booking.Arrival) || StringToDate(b.Arrival)  < StringToDate(booking.Departure)).Select(b => b.DockId).ToList();

                DockDTO dock = _harborDbContext.Docks.FirstOrDefault(d => !busyDockIds.Contains(d.Id));
                if (dock != null)
                {
                    throw new System.ArgumentException("DockId " + dock.Id.ToString() + " ist noch frei");
                }
                throw new System.ArgumentException("Alle Docks sind besetzt");

            }
        }

        private string DateToString(DateTime date)
        {
            return date.ToString("dd.MM.yyyy");
        }

        private DateTime StringToDate(string s)
        {
            try
            {
                return Convert.ToDateTime(s);
            }
            catch (Exception)
            {
                throw new System.ArgumentException("Die übergebenen Zeiten sind im falschen Format");
            }
        }
    }
}
