namespace EmployeeAppraisalSystem.Models
{
    public class Appraisal
    {
        public int AppraisalId { get; set; }
        public Status status { get; set; }
        
        public int empId { get; set; }

        public int MgrId { get; set; }  

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
