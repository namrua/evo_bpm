using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.ServiceType.Command
{
    public class UpdateServiceTypeCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string ServiceTypeName { get; set; }
        public bool RecordStatus { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Description { get; set; }
        public string UpdatedBy { get; set; }
    }
}
