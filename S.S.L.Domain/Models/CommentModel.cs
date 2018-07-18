using System;

namespace S.S.L.Domain.Models
{
    public class CommentModel
    {
        public int Id { get; set; }

        public string Message { get; set; }
        //Relationships
        public UserModel User { get; set; }

        public DateTime Date { get; set; }
    }
}
