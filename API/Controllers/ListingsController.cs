using API.Core;
using API.Core.Models;
using API.Service.DTOs;
using API.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using static API.Core.Models.Enums;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class ListingsController : ControllerBase
    {
        #region  Variables Declaration
        private readonly ILogger<ListingsController> _logger;
        private readonly IListingService _listingService;
        #endregion

        #region Constructors
        public ListingsController(ILogger<ListingsController> logger, IListingService listingService)
        {
            _logger = logger;
            _listingService = listingService;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        public async Task<IActionResult> GetListings(string suburb, CategoryType categoryType = CategoryType.None, StatusType statusType = StatusType.None, int skip = 0, int take = 10)
        {
            _logger.LogInformation($"GetListings method in Listing controller called");

            var response = await _listingService.GetListings(suburb, categoryType, statusType, skip, take);

            return Ok(response);
        } 
        #endregion
    }
}
