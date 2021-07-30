using System;
using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.Models.Status;
using MediatR;

namespace CleanEvoBPM.Application.CQRS.Status.Query
{
    public class GetStatusDetailsQuery : IRequest<GenericResponse<StatusResponseModel>>
    {
        public Guid Id { get; set; }
    }
}