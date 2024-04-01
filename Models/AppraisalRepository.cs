namespace EmployeeAppraisalSystem.Models
{
    public class AppraisalRepository
    {
        private List<Appraisal> _appraisals = new List<Appraisal>();

        // Method to update the status of an appraisal
        public void UpdateAppraisalStatus(int empId, int mgrId, Status status)
        {
            // Find the appraisal based on empId and mgrId
            var appraisal = _appraisals.FirstOrDefault(a => a.empId == empId && a.MgrId == mgrId);

            if (appraisal != null)
            {
                // Update the status
                appraisal.status = status;
            }
            else
            {
                // Appraisal not found, you might want to handle this scenario
            }
        }


    }
}
