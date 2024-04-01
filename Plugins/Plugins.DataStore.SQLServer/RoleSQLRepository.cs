using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.SQLServer
{
    public class RoleSQLRepository : IRoleRepository
    {
        private readonly AppraisalSystemContext db;

        public RoleSQLRepository(AppraisalSystemContext db)
        {
            this.db = db;
        }
        public int GetIdByRoleName(string role)
        {
            // Retrieve the role ID from the database based on the role name
            var _role = db.Roles.FirstOrDefault(r => r.RoleName == role);

            // If the role is found, return its ID; otherwise, return 0
            return _role != null ? _role.RoleId : 0;
        }
    }
}
