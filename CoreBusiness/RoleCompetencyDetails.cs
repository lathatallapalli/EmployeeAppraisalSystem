namespace CoreBusiness
{
    public class RoleCompetencyDetails
    {
        public int DetailId { get; set; }
        public int RoleId { get; set; }
        public int CompetencyId { get; set; }

        //Navigation properties
        public Role Role { get; set; } // Navigation property for Role
        public Competency Competency { get; set; } // Navigation property for Competency

    }
}
