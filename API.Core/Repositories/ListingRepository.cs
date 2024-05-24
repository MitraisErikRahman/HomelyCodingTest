using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Core.Custom;
using API.Core.Interfaces;
using API.Core.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using static API.Core.Models.Enums;

namespace API.Core.Repositories
{
    public class ListingRepository : IListingRepository
    {
        #region Variables Declaration
        private readonly ILogger<ListingRepository> _logger;
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructors
        /// <summary>
        /// Listing repository class constructor
        /// </summary>
        /// <param name="configuration">Configuration interface dependency injection</param>
        /// <param name="logger">Logger  interface dependency injection</param>
        public ListingRepository(IConfiguration configuration, ILogger<ListingRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// A repository method to get listings
        /// </summary>
        /// <param name="suburb">Suburb</param>
        /// <param name="categoryType">Category type</param>
        /// <param name="statusType">Status type</param>
        /// <param name="skip">Skip</param>
        /// <param name="take">How many records to take</param>
        /// <returns></returns>
        public async Task<IEnumerable<Listing>> GetListings(string suburb, CategoryType categoryType, StatusType statusType, int skip, int take)
        {
            _logger.LogInformation($"GetListings method in Listing repository called");
            var listings = new List<Listing>();
            var total = 0;

            var filter = categoryType != CategoryType.None ? $" AND CategoryType = {(int)categoryType} " : string.Empty;
            filter = statusType != StatusType.None ? filter + $" AND StatusType = {(int)statusType} " : string.Empty;

            var sqlCommandText = $@" SELECT count(ListingId) FROM [Backend-TakeHomeExercise].dbo.Listings WITH(NOLOCK)
                                WHERE Suburb = @suburb {filter} ;

                                SELECT ListingId, StreetNumber, Street, Suburb, State, Postcode, DisplayPrice, Title, CategoryType, StatusType
                                FROM [Backend-TakeHomeExercise].dbo.Listings WITH(NOLOCK)
                                WHERE Suburb = @suburb {filter} 
                                ORDER BY ListingId
                                OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY ;
                            ";

            var dbManager = new DbManager(EnumDB.TEST, DbAccessLevel.READ, _configuration);

            using (var dbConnection = dbManager.GetOpenConnection())
            {
                var sqlCommand = new CommandDefinition(sqlCommandText, new { suburb, categoryType = (int)categoryType, statusType = (int)statusType, skip, take });
                var queryMultiple = dbConnection.QueryMultiple(sqlCommand);

                total = queryMultiple.Read<int>().FirstOrDefault();
                listings = queryMultiple.Read<Listing>().ToList();
            }

            if (total == 0)
            {
                _logger.LogInformation($"Listing not found");
                return null;
            }

            _logger.LogInformation($"Listing found");
            return listings;
        } 
        #endregion
    }
}
