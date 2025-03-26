using APP.TODO.Domain;
using CORE.APP.Features;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.TODO.Features
{
    public abstract class TodoDbHandler : Handler
    {
        protected readonly TodoDb _db;

        protected TodoDbHandler(TodoDb db) : base(new CultureInfo("en-US"))
        {
            _db = db;
        }
    }
}
