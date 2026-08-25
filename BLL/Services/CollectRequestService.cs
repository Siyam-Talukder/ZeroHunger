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

        public List<CollectRequestModel> GetAssignedTasks(int employeeId)
        {
            var data = repo.GetAssignedTasksForEmployee(employeeId);
            var mapped = mapper.Map<List<CollectRequestModel>>(data);
            return mapped;
        }

        // Restaurant Action
        public bool Create(CollectRequestModel r)
        {
            var mapped = mapper.Map<CollectRequest>(r);

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

        // Employee accepts 
        public bool AcceptTask(int requestId, int employeeId)
        {
            var request = repo.GetById(requestId);

            if (request == null || request.Status != "Assigned" || request.EmployeeId != employeeId)
            {
                return false;
            }

            request.Status = "Accepted";
            return repo.Update(requestId, request);
        }

        // Employee Cancel
        public bool CancelTask(int requestId, int employeeId)
        {
            var request = repo.GetById(requestId);

            if (request == null || (request.Status != "Assigned" && request.Status != "Accepted") || request.EmployeeId != employeeId)
            {
                return false;
            }

            request.EmployeeId = null;
            request.Status = "Pending";
            return repo.Update(requestId, request);
        }

        //Employee action 2
        public string CollectFood(int requestId, int employeeId)
        {
            var request = repo.GetById(requestId);
            if (request == null || request.Status != "Accepted" || request.EmployeeId != employeeId)
            {
                return "Invalid";
            }

            if (DateTime.Now > request.MaxPreservationTime)
            {
                request.Status = "Expired";
                repo.Update(requestId, request);
                return "Expired";
            }

            request.Status = "Collected";
            repo.Update(requestId, request);
            return "Success";
        }

        //Employee action 3
        public string CompleteRequest(int requestId, int employeeId)
        {
            var request = repo.GetById(requestId);

            if (request == null || request.Status != "Collected" || request.EmployeeId != employeeId)
            {
                return "Invalid";
            }
            request.CompletedAt = DateTime.Now;

            if (DateTime.Now > request.MaxPreservationTime)
            {
                request.Status = "Expired";
                repo.Update(requestId, request);
                return "Expired";
            }

            request.Status = "Completed";
            repo.Update(requestId, request);
            return "Success";
        }
    }
}
