using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api_Versioning_Swagger_Asp.Net_Core3._1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_Versioning_Swagger_Asp.Net_Core3._1.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Movie>>> GetAll()
        {
            var movie = new List<Movie>()
            {
                new Movie{Id=1, Name="Movie Name1 version 1" },
                new Movie{Id=2, Name="Movie Name2 version 1" },
                new Movie{Id=3, Name="Movie Name3 version 1" },
                new Movie{Id=4, Name="Movie Name4 version 1" }
            };

            return Ok(movie);
        }
    }
}
