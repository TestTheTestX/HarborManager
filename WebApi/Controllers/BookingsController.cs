using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using HarborManager.Contracts;
using Microsoft.AspNetCore.Http;


namespace HarborManager.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Bookings")]
    public class BookingsController : Controller
    {
        private readonly IBookings _bookings;
        public BookingsController(IBookings bookings)
        {
            _bookings = bookings;
        }

        [HttpGet]
        public IEnumerable<BookingDTO> GetAll()
        {
            return _bookings.GetAll().ToList();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var booking = _bookings.GetById(id);
            if (booking == null)
            {
                return StatusCode(404);         
            }

            return new ObjectResult(booking);
        }

        [HttpPost]
        public IActionResult Create([FromBody] BookingDTO booking)
        {

            if (booking == null)
            {
                return StatusCode(400);
            }

            try
            {
                _bookings.Add(booking);
                return StatusCode(201);
            }
            catch (ArgumentException e)
            {
                return StatusCode(422, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(400);
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] BookingDTO booking)
        {
            if (booking == null)
            {
                return StatusCode(400);
            }

            try
            {
                _bookings.Update(booking);
                return new NoContentResult();
            }
            catch (ArgumentException ex)
            {
                return StatusCode(422, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(400);
            }
        }

        [HttpDelete]
        public IActionResult DeleteAll()
        {
            _bookings.DeleteAll();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            var booking = _bookings.GetById(id);
            if (booking == null)
            {
                return StatusCode(404);
            }

            _bookings.DeleteById(id);
            return new NoContentResult();
        }
    }
}
