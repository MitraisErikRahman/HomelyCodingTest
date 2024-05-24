using API.Core.Models;
using API.Core.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Xunit;

namespace API.Tests
{
    public class ListingRepositoryTest
    {
        #region Variables Declaration
        private IConfiguration _configuration;
        private ILogger<ListingRepository> _logger;
        #endregion

        #region Public Methods
        [Fact]
        public void GetResidentialListings_ListingsFound_ReturnListings()
        {
            _configuration = GetConfiguration();
            string subsurb = "Southbank";

            var listingRepository = new ListingRepository(_configuration, _logger);

            var results = listingRepository.GetListings(subsurb, Enums.CategoryType.Residential, Enums.StatusType.Current, 0, 50);
            Assert.NotNull(results);
        }

        [Fact]
        public void GetRentalListings_ListingsFound_ReturnListings()
        {
            _configuration = GetConfiguration();
            string subsurb = "Kew";

            var listingRepository = new ListingRepository(_configuration, _logger);

            var results = listingRepository.GetListings(subsurb, Enums.CategoryType.Rental, Enums.StatusType.Current, 0, 50);
            Assert.NotNull(results);
        }

        public IConfiguration GetConfiguration()
        {
            if (_configuration == null)
            {
                var builder = new ConfigurationBuilder().AddJsonFile($"testsettings.json", optional: false);
                _configuration = builder.Build();
            }

            return _configuration;
        } 
        #endregion
    }
}
