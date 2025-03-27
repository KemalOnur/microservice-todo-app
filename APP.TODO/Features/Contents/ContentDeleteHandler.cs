using APP.TODO.Domain;
using CORE.APP.Features;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APP.TODO.Features.Contents
{
    public class ContentDeleteRequest : Request, IRequest<CommandResponse>
    {
    }


    public class ContentDeleteHandler : TodoDbHandler, IRequestHandler<ContentDeleteRequest, CommandResponse>
    {
        public ContentDeleteHandler(TodoDb db) : base(db)
        {
        }

        public async Task<CommandResponse> Handle(ContentDeleteRequest request, CancellationToken cancellationToken)
        {
            var entity = await _db.Contents.Include(c => c.Todos).SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity is null)
                return Error("Content not found");
            if (entity.Todos.Any())
                return Error("Content Can not be deleted since it has a attached Todos");
            _db.Contents.Remove(entity);
            await _db.SaveChangesAsync(cancellationToken);
            return Success("Content Deleted Successfully");
        }
    }
}
