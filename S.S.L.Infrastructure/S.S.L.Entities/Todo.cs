namespace S.S.L.Infrastructure.S.S.L.Entities
{
    public class Todo : BaseModel
    {
        public string Item { get; set; }
        public bool Done { get; set; }


        //Relationship
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
