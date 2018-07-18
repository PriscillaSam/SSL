using System.Collections.Generic;

namespace S.S.L.Domain.Models
{
    public class ForumModel
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Detail { get; set; }
        public string Scriptures { get; set; }


        public IEnumerable<CommentModel> Comments { get; set; }
    }
}
