using System;
using System.ComponentModel.DataAnnotations;

namespace S.S.L.Infrastructure.S.S.L.Entities
{
    public abstract class BaseModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; } 
    }
}
