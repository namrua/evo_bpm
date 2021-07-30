using CleanEvoBPM.Application.CQRS.ProblemCategory.Command;
using CleanEvoBPM.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.ProblemCategory.Event
{
    public class DeleteProblemCategoryLog : IDomainEvent
    {
        public Guid Id { get; set; }        
        public DeleteProblemCategoryLog(DeleteProblemCategoryCommand model)
        {
            this.Id = model.Id;            
        }
    }
}
