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
    public class ContentUpdateRequest : Request, IRequest<CommandResponse>
    {

        [Required, StringLength(100)]
        public string Name { get; set; }
    }


    public class ContentUpdateHandler : TodoDbHandler, IRequestHandler<ContentUpdateRequest, CommandResponse>
    {
        public ContentUpdateHandler(TodoDb db) : base(db)
        {
        }

        public async Task<CommandResponse> Handle(ContentUpdateRequest request, CancellationToken cancellationToken)
        {
            if (await _db.Contents.AnyAsync(x=> x.Id != request.Id && x.Name.ToUpper() == request.Name.ToUpper().Trim(), cancellationToken))
                return Error("Content with the same name exists");

            var entity = await _db.Contents.FindAsync(request.Id, cancellationToken);
            if (entity is null)
                return Error("Content not found");

            entity.Name = request.Name.Trim();

            _db.Contents.Update(entity);
            await _db.SaveChangesAsync(cancellationToken);
            return Success("Content Updated Successfully");
        }
    }
}
