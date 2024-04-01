using System.ComponentModel.DataAnnotations;

namespace CoreBusiness
{
    public class Competency
    {
        public int CompetencyId { get; set; }

        [Required]
        public string CompetencyName { get; set; } = string.Empty;

        [Required]
        public CompetencyType competencyType { get; set; }


        //Navigation properties
        public ICollection<RoleCompetencyDetails> RoleCompetencyDetails { get; set; } // Navigation property for RoleCompetencyDetails
    }

    public enum CompetencyType
    {
        Technical,
        Behavioral
    }
}
