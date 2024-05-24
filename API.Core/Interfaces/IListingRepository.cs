using API.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static API.Core.Models.Enums;

namespace API.Core.Interfaces
{
    public interface IListingRepository
    {
        /// <summary>
        /// A repository method to get listings
        /// </summary>
        /// <param name="suburb">Suburb</param>
        /// <param name="categoryType">Category type</param>
        /// <param name="statusType">Status type</param>
        /// <param name="skip">Skip</param>
        /// <param name="take">How many records to take</param>
        /// <returns></returns>
        Task<IEnumerable<Listing>> GetListings(string suburb, CategoryType categoryType, StatusType statusType, int skip, int take);
    }
}
