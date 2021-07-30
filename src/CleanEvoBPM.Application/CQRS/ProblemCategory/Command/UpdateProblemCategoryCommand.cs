using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.ProblemCategory.Command
{
    public class UpdateProblemCategoryCommand:IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActived { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }        
        public bool DeleteFlag { get; set; }
    }
}
