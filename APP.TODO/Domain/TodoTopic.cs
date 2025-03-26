using CORE.APP.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.TODO.Domain
{
    public class TodoTopic : Entity
    {
        public int TodoId { get; set; }
        public Todo Todo { get; set; }
        public int TopicId { get; set; }
        public Topic Topic { get; set; }

    }
}
