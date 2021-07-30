﻿using CleanEvoBPM.Application.CQRS.ServiceType.Command;
using CleanEvoBPM.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.ServiceType.Event
{
    public class UpdateServiceTypeLog : IDomainEvent
    {
        public Guid Id { get; set; }
        public string ServiceTypeName { get; set; }
        public bool RecordStatus { get; set; }

        public UpdateServiceTypeLog(UpdateServiceTypeCommand model)
        {
            this.Id              = model.Id;
            this.ServiceTypeName = model.ServiceTypeName;
            this.RecordStatus    = model.RecordStatus;
        }
    }
}
