using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimGame.Domain
{
    [Serializable]
    public abstract class DomainObject
    {
        public abstract int Id { get; set; }
        [NotMapped]
        public virtual bool Deleted { get; set; }
    }
}