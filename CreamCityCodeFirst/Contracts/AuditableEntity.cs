using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CreamCityCodeFirst.Contracts
{
    internal abstract class AuditableEntity
    {
        [Required]
        public DateTimeOffset? LastChangedTimestamp { get; set; }

        [Required]
        public string LastChangedByUser { get; set; }

        public bool IsDeleted { get; set; }

    }
    internal abstract class AuditableEntity<T> : AuditableEntity, IIdentified<T>
    {
        [Required]
        public T Id { get; set; }

    }
}
