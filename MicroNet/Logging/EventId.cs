using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroNet.Logging
{
    public struct EventId
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return string.Format(" Id:{0} Name:{1} ", this.Id, this.Name);
        }
    }
}
