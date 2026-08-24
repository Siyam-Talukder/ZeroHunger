using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BLL.Models;
using DAL.EF.Tables;
using DAL.Repo;

namespace BLL.Services
{
    public class ResturantService
    {
        ResturantRepo repo;
        IMapper mapper;

        public ResturantService(ResturantRepo repo,IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public ResturantModel GetById(int id)
        {
            var data = repo.GetById(id);
            var mapped = mapper.Map<ResturantModel>(data);
            return mapped;
        }

        public List<ResturantModel> All()
        {
            var data = repo.All();
            var mapped = mapper.Map<List<ResturantModel>>(data);
            return mapped;
        }

        public bool Create(ResturantModel r)
        {
            var mapped = mapper.Map<Resturant>(r);
            var res = repo.Create(mapped);
            return res;
        }

        public bool Delete(int id)
        {
            var res = repo.Delete(id);
            return res;
        }

        public bool Update(int id, ResturantModel model)
        {
            var mapped = mapper.Map<Resturant>(model);
            return repo.Update(id, mapped);
        }
    }
}
