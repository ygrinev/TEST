using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProxyAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProxyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuoteController : ControllerBase
    {
        private readonly ILogger<QuoteController> _logger;
        private readonly IQuoteHelper _quoteHelper;

        public QuoteController(ILogger<QuoteController> logger, IQuoteHelper quoteHelper)
        {
            _logger = logger;
            _quoteHelper = quoteHelper;
        }

        [HttpGet]
        public Quote Get(int maxPrice, int x, int y, int z)
        {
            try
            {
                var quote = _quoteHelper.GetBestQuote(maxPrice, x, y, z);
                return quote ?? new Quote();
            }
            catch(Exception e)
            {
                return new Quote(500, -1, $"ERROR: {e.Message}");
            }
        }
    }
}
