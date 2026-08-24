using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BLL.Models;
using DAL.EF.Tables;

namespace BLL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Resturant, ResturantModel>().ReverseMap();
            CreateMap<Employee, EmployeeModel>().ReverseMap();
            CreateMap<CollectRequest, CollectRequestModel>().ReverseMap();
        }
    }
}
