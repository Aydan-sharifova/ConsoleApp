using DomainLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
   public class Groups:BaseEntity
    {
        public string Teacher { get; set; }
        
        public string Room { get; set; }
    }
}
