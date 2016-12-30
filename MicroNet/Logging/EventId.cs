using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroNet.Logging
{
    public class EventId
    {
        public EventId()
        {

        }

        public EventId(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }


        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return string.Format(" EventId:{0} EventName:{1}", this.Id, this.Name);
        }
    }
}
