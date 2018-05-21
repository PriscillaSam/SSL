using System;
using System.ComponentModel.DataAnnotations;

namespace S.S.L.Infrastructure.S.S.L.Entities
{
    public class Model
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
