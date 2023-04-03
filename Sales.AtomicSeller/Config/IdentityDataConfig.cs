using Sales.AtomicSeller.Identity;
using System.Collections.Generic;

namespace Sales.AtomicSeller.Config
{
    /// <summary>
    /// Identity Default Data.
    /// </summary>
    public class IdentityDataConfig 
    {
        /// <summary>
        /// Default Roles.
        /// </summary>
        public List<Role> Roles { get; set; }
        /// <summary>
        /// Default Users.
        /// </summary>
        public List<User> Users { get; set; }
    }


}
