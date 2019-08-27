using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wdc.DirectoryLib.Types;

namespace DT.STS.IdentityServer.Common.Models
{
    public class AdUserAccount : UserAccount
    {
        public bool Active { get; set; }
    }
}
