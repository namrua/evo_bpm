using System;
using CleanEvoBPM.Domain.Entities;

namespace CleanEvoBPM.Application.Models.Status
{
    public class StatusResponseModel : AuditableEntity
    {
        public bool Active { get; set;}
    }
}