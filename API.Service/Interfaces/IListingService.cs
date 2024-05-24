using API.Core.Models;
using API.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static API.Core.Models.Enums;

namespace API.Service.Interfaces
{
    public interface IListingService
    {
        /// <summary>
        /// A service method to get listings
        /// </summary>
        /// <param name="suburb">Suburb</param>
        /// <param name="categoryType">Category type</param>
        /// <param name="statusType">Status type</param>
        /// <param name="skip">Skip</param>
        /// <param name="take">How many records to take</param>
        /// <returns></returns>
        Task<PagedResult<ListingDTO>> GetListings(string suburb, CategoryType categoryType, StatusType statusType, int skip, int take);
    }
}
