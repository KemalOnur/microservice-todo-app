using APP.TODO.Domain;
using CORE.APP.Features;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.TODO.Features.Todos
{

    public class TodoDeleteRequest : Request, IRequest<CommandResponse>
    {

    }

    class TodoDeleteHandler : TodoDbHandler, IRequestHandler<TodoDeleteRequest, CommandResponse>
    {
        public TodoDeleteHandler(TodoDb db) : base(db)
        {
        }

        public async Task<CommandResponse> Handle(TodoDeleteRequest request, CancellationToken cancellationToken)
        {
            var entity = await _db.Todos.Include(x => x.TodoTopics).SingleOrDefaultAsync(x => x.Id == request.Id,cancellationToken);

            if (entity is null)
                return Error("Notes not found");

            _db.TodoTopics.RemoveRange(entity.TodoTopics);

            _db.Todos.Remove(entity);
            await _db.SaveChangesAsync(cancellationToken);

            return Success("Note Deleted Succesfully", entity.Id);
        }
    }
}
