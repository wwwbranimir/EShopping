﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Events
{
    public class BaseIntegrationEvent
    {
        //CO-Relation Id
        public string CorrelationId { get; set; }
        public DateTime CreationDate { get; private set; }

        public BaseIntegrationEvent()
        {
            CorrelationId = Guid.NewGuid().ToString();
            CreationDate = DateTime.UtcNow;
        }

        public BaseIntegrationEvent(Guid correlationId, DateTime creationDate)
        {
            CorrelationId = correlationId.ToString();
            CreationDate = creationDate;
        }
    }
}
