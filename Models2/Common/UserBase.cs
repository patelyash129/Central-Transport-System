using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Frameworks;
using Tech.Frameworks.Helpers;

namespace Models2.Common
{
    public class UserBase:QueryHelper
    {
        [DBColumnName("U_Id")]
        [PrimaryKey]
        public long Id { get; set; }

        [DBColumnName("is_deleted")]
        [Delete]
        public bool IsDeleted { get; set; }

        [Ignore(IgnoreFor.All)]
        public bool IsAudit { get; set; }
    }
}
