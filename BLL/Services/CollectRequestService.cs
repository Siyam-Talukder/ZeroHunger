using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Azure.Core;
using BLL.Models;
using DAL.EF.Tables;
using DAL.Repo;

namespace BLL.Services
{
    public class CollectRequestService
    {
        CollectRequestRepo repo;
        IMapper mapper;

        public CollectRequestService(CollectRequestRepo repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public CollectRequestModel GetById(int id)
        {
            var data = repo.GetById(id);
            var mapped = mapper.Map<CollectRequestModel>(data);
            return mapped;
        }

        public List<CollectRequestModel> All()
        {
            var data = repo.All();
            var mapped = mapper.Map<List<CollectRequestModel>>(data);
            return mapped;
        }


        public bool Delete(int id)
        {
            var res = repo.Delete(id);
            return res;
        }

        public bool Update(int id, CollectRequestModel model)
        {
            var mapped = mapper.Map<CollectRequest>(model);
            return repo.Update(id, mapped);
        }


        // Restaurant Action
        public bool Create(CollectRequestModel r)
        {
            var mapped = mapper.Map<CollectRequest>(r);
            if (mapped.MaxPreservationTime <= DateTime.Now)
            {
                return false;
            }

            mapped.Status = "Pending";
            mapped.CreatedAt = DateTime.Now;

            var res = repo.Create(mapped);
            return res;
        }

        // Admin Action
        public bool AssignEmployee(int requestId, int employeeId)
        {
            var request = repo.GetById(requestId);

            if (request == null || request.Status != "Pending")
            {
                return false;
            }

            request.EmployeeId = employeeId;
            request.Status = "Assigned";

            return repo.Update(requestId, request);
        }


        //Employee action
        public bool CompleteRequest(int requestId, int employeeId)
        {
            var request = repo.GetById(requestId);

            if (request == null || request.Status != "Assigned" || request.EmployeeId != employeeId)
            {
                return false;
            }

            request.Status = "Completed";
            request.CompletedAt = DateTime.Now;
            return repo.Update(requestId, request);
        }

        
    }
}
