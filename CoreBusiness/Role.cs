namespace CoreBusiness
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;

        public string RoleDescription { get; set; } = string.Empty;

        //Naviagtion properties
        public ICollection<RoleCompetencyDetails> RoleCompetencyDetails { get; set; } // Navigation property for RoleCompetencyDetails

    }
}
