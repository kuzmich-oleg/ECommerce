using ECommerce.API.Search.Interfaces;
using ECommerce.API.Search.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Search.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpPost]
        public async Task<ActionResult> Search([FromBody] SearchTerm term, CancellationToken cancellationToken)
        {
            var result = await _searchService.SearchAsync(term.CustomerId, cancellationToken);

            if (result == null) return NotFound();

            return this.Ok(result);
        }
    }
}
