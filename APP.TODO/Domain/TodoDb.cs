using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace APP.TODO.Domain
{
    public class TodoDb : DbContext
    {
        public DbSet<Topic> Topics { get; set; }

        public DbSet<Todo> Todos{ get; set; }

        public DbSet<TodoTopic> TodoTopics { get; set; }

        public TodoDb(DbContextOptions options) : base(options) 
        {
            
        }
    }
}
