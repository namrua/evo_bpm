using CleanEvoBPM.Application.CQRS.ProblemCategory.Command;
using CleanEvoBPM.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.ProblemCategory.Event
{
    public class UpdateProblemCategoryLog : IDomainEvent
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

        public UpdateProblemCategoryLog(UpdateProblemCategoryCommand model)
        {
            this.Id = model.Id;
            this.Name = model.Name;
            this.Description = model.Description;
            this.CreatedDate = model.CreatedDate;
            this.UpdatedDate = model.UpdatedDate;
            this.IsActived = model.IsActived;
            this.CreatedBy = model.CreatedBy;
            this.DeleteFlag = model.DeleteFlag;
        }
    }
}
