using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HarborManager.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HarborManager.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Boats")]
    public class BoatsController : Controller
    {
        private readonly IBoats _boats;
        public BoatsController(IBoats boats)
        {
            _boats = boats;
        }

        [HttpGet]
        public IEnumerable<BoatDTO> GetAll()
        {
            return _boats.GetAll().ToList();
        }
    }
}