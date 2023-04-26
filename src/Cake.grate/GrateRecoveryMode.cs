using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake.grate
{
    /// <summary>
    /// Defines the recovery model for SQL Server.
    /// </summary>
    public enum GrateRecoveryMode
    {
        /// <summary>
        /// Doesn't change the mode
        /// </summary>
        NoChange,

        /// <summary>
        /// Does not create backup before migration
        /// </summary>
        Simple,

        /// <summary>
        /// Creates log backup before migration
        /// </summary>
        Full
    }
}
