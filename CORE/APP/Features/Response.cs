using CORE.APP.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.APP.Features
{
    public class CommandResponse : Entity
    {
        public bool IsSuccessful { get; }
        public string Message { get; }

        public CommandResponse(bool isSuccesful, string message = "", int id = 0) : base(id)// Assigning a default value in ctor make default value which helps us to call this constructor with only bool value
        {
            IsSuccessful = isSuccesful;
            Message = message;
        }
    }


    public class QueryResponse : Entity
    {

    }
}
