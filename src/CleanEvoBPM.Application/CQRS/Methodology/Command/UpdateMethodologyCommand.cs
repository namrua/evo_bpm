using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace CleanEvoBPM.Application.CQRS.Methodology.Command
{
    public class UpdateMethodologyCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string MethodologyName { get; set; }
        public string Description { get; set; }
        public bool RecordStatus { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
