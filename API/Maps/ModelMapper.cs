using API.Core.Models;
using API.Service.DTOs;
using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API.Maps
{
    public class ModelMapper : Profile
    {
        public ModelMapper()
        {
            CreateMap<Listing, ListingDTO>();
        }
    }
}
