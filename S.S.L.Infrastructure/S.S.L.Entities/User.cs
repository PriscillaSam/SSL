namespace S.S.L.Infrastructure.S.S.L.Entities
{
    public class User : Model
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string ImageUrl { get; set; }

        //Relationships
        public int RoleId { get; set; }
        public Role Role { get; set; }

    }
}
