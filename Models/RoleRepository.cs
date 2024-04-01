namespace EmployeeAppraisalSystem.Models
{
    public class RoleRepository
    {
        private static List<Role> _roles = new List<Role>()
        {
            new Role { RoleId = 1, RoleName = "sr.dev"},
            new Role { RoleId = 2, RoleName = "dev"}

        };

        public static int GetIdByRoleName(string rolename)
        {
            foreach (var role in _roles)
            {
                if (role.RoleName == rolename)
                {
                    return role.RoleId;
                }
            }
            return 0;
        }
    }
}
