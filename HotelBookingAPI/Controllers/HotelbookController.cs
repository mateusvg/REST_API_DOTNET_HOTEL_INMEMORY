using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HotelBookingAPI.Models;
using HotelBookingAPI.Data;

namespace HotelBookingAPI.Controllers
{

    [Route("api/[controller]/[action]")] // escapa o nome do controller e da ação dentro da url
    [ApiController]
    public class HotelbookController : ControllerBase
    {
        private readonly ApiContext _context;
        public HotelbookController(ApiContext context)
        {
            _context = context;
        }


        //Create/Edit
        [HttpPost]
        public JsonResult CreateEdit(HotelBooking booking) 
        {
            if (booking.Id == 0)
            {
                _context.Bookings.Add(booking);
            }else
            {
                var bookingInDb = _context.Bookings.Find(booking.Id);
                if (bookingInDb == null)
                    return new JsonResult(NotFound());

                bookingInDb = booking;
                
            }
            _context.SaveChanges();
            return new JsonResult(Ok(booking));
        }

        //Get
        [HttpGet]
        public JsonResult Get(int id)
        {
            var result = _context.Bookings.Find(id);
            if (result == null)
                return new JsonResult(NotFound());
            return new JsonResult(Ok(result));
        }

        //Delete
        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var result = _context.Bookings.Find(id);
            if (result == null)
                return new JsonResult(NotFound());
            _context.Bookings.Remove(result);
            _context.SaveChanges();
            return new JsonResult(NoContent());

        }

        //Get All

        [HttpGet("/GetAll")]
        [HttpGet()]
        public JsonResult GetAll()
        {
            var result =_context.Bookings.ToList();
            return new JsonResult(Ok(result));


        }

    }
}
