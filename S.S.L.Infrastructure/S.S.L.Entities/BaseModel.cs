using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace S.S.L.Infrastructure.S.S.L.Entities
{
    public abstract class BaseModel
    {
        [Key]
        [Column(Order = 1)]

        public int Id { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime UpdatedAt { get; set; }
    }
}
