using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.InMemory
{
    public class RoleInMemoryRepository : IRoleRepository
    {
        private static List<Role> _roles = new List<Role>()
        {
            new Role { RoleId = 1, RoleName = "Sr.dev"},
            new Role { RoleId = 2, RoleName = "Dev"}

        };
        public int GetIdByRoleName(string role)
        {
            foreach (var _role in _roles)
            {
                if (_role.RoleName == role)
                {
                    return _role.RoleId;
                }
            }
            return 0;
        }
    }
}
