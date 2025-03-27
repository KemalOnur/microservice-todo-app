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
    public class ContentCreateRequest : Request, IRequest<CommandResponse>
    {
        [JsonIgnore]
        public override int Id { get => base.Id; set => base.Id = value; }

        [Required, StringLength(100)]
        public string Name { get; set; }
    }


    public class ContentCreateHandler : TodoDbHandler, IRequestHandler<ContentCreateRequest, CommandResponse>
    {
        public ContentCreateHandler(TodoDb db) : base(db)
        {
        }

        public async Task<CommandResponse> Handle(ContentCreateRequest request, CancellationToken cancellationToken)
        {
            if (await _db.Contents.AnyAsync(x => x.Name.ToUpper() == request.Name.ToUpper().Trim(), cancellationToken))
                return Error("Coontent with the same name exists");

            var entity = new Content()
            {
                Name = request.Name?.Trim()
            };

            _db.Contents.Add(entity);
            await _db.SaveChangesAsync(cancellationToken);
            return Success("Content created Successfully");
        }
    }
}
