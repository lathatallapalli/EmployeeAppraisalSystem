using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreBusiness
{
    public class Appraisal
    {
        [Key]
        public int AppraisalId { get; set; }
        public Status status { get; set; }

/*        [ForeignKey("EmployeeId")]*/
        public int EmployeeId { get; set; }

/*        [ForeignKey("ManagerId")]*/
        public int ManagerId { get; set; }


        // Navigation properties
        public Employee Employee { get; set; }
      /*  public Employee Manager { get; set; }*/
        public ICollection<AppraisalDetailsCompetency> AppraisalDetailsCompetencies { get; set; }
        public ICollection<AppraisalDetailsObjective> AppraisalDetailsObjectives { get; set; }
    }

    public enum Status
    {
        New,
        Created,
        SelfRated,
        PeerRated,
        Rated,
        Completed

    }
}
