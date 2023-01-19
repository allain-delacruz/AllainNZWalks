namespace AllainNZWalks.Models.Domain
{
    public class Role
    {
        public Guid id { get; set; }
        public string RoleName { get; set; }

        //Navigation Property
        public List<UserRole> UserRoles { get; set; }
    }
}
