using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebSiteManager.Services;

namespace WebSiteManager.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebSitesController : ControllerBase
    {
        private readonly IWebSiteService _webSiteService;
        private readonly IMapper _mapper;

        public WebSitesController(IWebSiteService webSiteService, IMapper mapper)
        {
            _webSiteService = webSiteService;
            _mapper = mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sorting">Sorting parameters in the format sorting=id.asc,name.desc and etch</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get([FromQuery] Models.Page page, string sorting)
        {
            var sortingDictionary = GetSortingDictionary(sorting);

            var result = _webSiteService.GetAll(_mapper.Map<Page>(page), new Sorting { Columns = sortingDictionary });

            if (result.Errors.ContainsKey(ErrorType.UnexpectedError))
            {
                return BadRequest();
            }

            return Ok(_mapper.Map<Models.Paginated<Models.WebSiteDetailed>>(result.Data));
        }

        private static Dictionary<string, SortingDirection> GetSortingDictionary(string sorting)
        {
            var sortingDictionary = new Dictionary<string, SortingDirection>();

            if (string.IsNullOrEmpty(sorting))
            {
                return sortingDictionary;
            }

            var sortingPairs = sorting.Split(",", StringSplitOptions.RemoveEmptyEntries);
            
            foreach (var sortingPair in sortingPairs)
            {
                var sortingPairSplit = sortingPair.Split(".", StringSplitOptions.RemoveEmptyEntries);
                sortingDictionary.Add(sortingPairSplit[0],
                    sortingPairSplit[1].ToLower() == "desc" ? SortingDirection.Descending : SortingDirection.Ascending);
            }

            return sortingDictionary;
        }
    }
}