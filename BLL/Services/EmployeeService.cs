using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BLL.Models;
using DAL.EF.Tables;
using DAL.Repo;

namespace BLL.Services
{
    public class EmployeeService
    {
        EmployeeRepo repo;
        IMapper mapper;

        public EmployeeService(EmployeeRepo repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public EmployeeModel GetById(int id)
        {
            var data = repo.GetById(id);
            var mapped = mapper.Map<EmployeeModel>(data);
            return mapped;
        }

        public List<EmployeeModel> All()
        {
            var data = repo.All();
            var mapped = mapper.Map<List<EmployeeModel>>(data);
            return mapped;
        }

        public bool Create(EmployeeModel r)
        {
            var mapped = mapper.Map<Employee>(r);
            var res = repo.Create(mapped);
            return res;
        }

        public bool Delete(int id)
        {
            var res = repo.Delete(id);
            return res;
        }

        public bool Update(int id, EmployeeModel model)
        {
            var mapped = mapper.Map<Employee>(model);
            return repo.Update(id, mapped);
        }
    }
}
