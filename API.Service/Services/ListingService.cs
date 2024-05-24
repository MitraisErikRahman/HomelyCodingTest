using API.Core.Interfaces;
using API.Core.Models;
using API.Service.DTOs;
using API.Service.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace API.Service.Services
{
    public class ListingService : IListingService
    {
        private readonly ILogger<ListingService> _logger;
        private readonly IListingRepository _listingRepository;
        private readonly IMapper _mapper;

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

        /// <summary>
        /// A service method to get listings
        /// </summary>
        /// <param name="suburb">Suburb</param>
        /// <param name="categoryType">Category type</param>
        /// <param name="statusType">Status type</param>
        /// <param name="skip">Skip</param>
        /// <param name="take">How many records to take</param>
        /// <returns></returns>
        public async Task<PagedResult<ListingDTO>> GetListings(string suburb, Enums.CategoryType categoryType, Enums.StatusType statusType, int skip, int take)
        {
            _logger.LogInformation($"GetListings method in Listing service called");
            var listing = await _listingRepository.GetListings(suburb, categoryType, statusType, skip, take);

            if (listing == null) return null;

            var total = listing.Count();
            if (total == 0) return null;

            var listingDTO = _mapper.Map<IEnumerable<ListingDTO>>(listing);
            return new PagedResult<ListingDTO>(skip, total, listingDTO);
        }
    }
}
