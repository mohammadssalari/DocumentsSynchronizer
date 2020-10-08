using System;
using System.Collections.Generic;
using System.Linq;
using DocumentsSynchronizer.Models;
using DocumentsSynchronizer.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DocumentsSynchronizer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExampleController : ControllerBase
    {
        

        private readonly ILogger<ExampleController> _logger;

        public ExampleController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        [HttpGet]
        public IEnumerable<Documents> Get()
        {
            var gwr = new GWRequester(Configuration);
            return gwr.GetAllChangedDocuments();
        }
    }
}