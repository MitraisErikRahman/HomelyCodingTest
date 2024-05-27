using API.Core.Interfaces;
using API.Core.Models;
using API.Service.DTOs;
using API.Service.Interfaces;
using API.Service.Responses;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static API.Core.Models.Enums;

namespace API.Service.Services
{
    public class ListingService : IListingService
    {
        #region Variables Declaration
        private readonly ILogger<ListingService> _logger;
        private readonly IListingRepository _listingRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        /// <summary>
        /// Listing service class constructor
        /// </summary>
        /// <param name="listingRepository">Listing repository interface dependency injection</param>
        /// <param name="mapper">Object class mapper  interface dependency injection</param>
        /// <param name="logger">Logger  interface dependency injection</param>
        public ListingService(IListingRepository listingRepository, IMapper mapper, ILogger<ListingService> logger)
        {
            _listingRepository = listingRepository;
            _mapper = mapper;
            _logger = logger;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// A service method to get listings
        /// </summary>
        /// <param name="suburb">Suburb</param>
        /// <param name="categoryType">Category type</param>
        /// <param name="statusType">Status type</param>
        /// <param name="skip">Skip</param>
        /// <param name="take">How many records to take</param>
        /// <returns></returns>
        public async Task<ReturnResponse> GetListings(string suburb, Enums.CategoryType categoryType, Enums.StatusType statusType, int skip, int take)
        {
            _logger.LogInformation($"GetListings method in Listing service called");
            ReturnResponse returnResponse = new ReturnResponse();

            try
            {               
                if (string.IsNullOrEmpty(suburb))
                {
                    returnResponse.StatusCode = (int)StatusCode.Status200OK;
                    returnResponse.Message = "No Suburb provided";
                    return returnResponse;
                }

                var listing = await _listingRepository.GetListings(suburb, categoryType, statusType, skip, take);

                var total = 0;
                if (listing != null) total = listing.Count();

                var listingDTO = _mapper.Map<IEnumerable<ListingDTO>>(listing);

                if (total == 0)
                {
                    returnResponse.StatusCode = (int)StatusCode.Status404NotFound;
                    returnResponse.Message = "No results";
                }
                else
                {
                    var pagedResult = new PagedResult<ListingDTO>(skip, total, listingDTO);
                    returnResponse.StatusCode = (int)StatusCode.Status200OK;
                    returnResponse.Message = "Data found";
                    returnResponse.Result = JsonConvert.SerializeObject(pagedResult);
                }
            }
            catch (Exception ex)
            {
                returnResponse.StatusCode = (int)StatusCode.Status500InternalServerError;
                returnResponse.Message = ex.Message;
            }
            
            return returnResponse;
        } 
        #endregion
    }
}
