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
    [Route("api/Docks")]
    public class DocksController : Controller
    {
        private readonly IDocks _docks;
        public DocksController(IDocks docks)
        {
            _docks = docks;
        }

        [HttpGet]
        public IEnumerable<DockDTO> GetAll()
        {
            return _docks.GetAll().ToList();
        }
    }
}