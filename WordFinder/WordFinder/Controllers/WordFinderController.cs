using Microsoft.AspNetCore.Mvc;

namespace WordFinder.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WordFinderController : ControllerBase
    {
        private readonly ILogger<WordFinderController> _logger;

        public WordFinderController(ILogger<WordFinderController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "FindWords")]
        public IEnumerable<string> Get()
        {
            //not implemented for being outside scope
            throw new NotImplementedException();
        }
    }
}
