using CORE.APP.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.TODO.Domain
{
    public class Content : Entity
    {
        [Required,StringLength(100)]
        public string Name { get; set; }

        public List<Todo> Todos { get; set; } = new List<Todo>();
    }
}
