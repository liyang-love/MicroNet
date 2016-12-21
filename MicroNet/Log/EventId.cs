using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroNet.Log
{
    public struct EventId
    {
        public int Id { get; }
        public string Name { get; }

        public override string ToString()
        {
            return string.Format("[Id：{0} Name:{1}]", this.Id, this.Name);
        }
    }
}
