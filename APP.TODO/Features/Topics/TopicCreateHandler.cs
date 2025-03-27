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

namespace APP.TODO.Features.Topics
{

    public class TopicCreateRequest : Request, IRequest<CommandResponse>
    {
        [Required]
        [StringLength(1000)]

        public string Name { get; set; }

        [JsonIgnore]
        public override int Id { get => base.Id; set => base.Id = value; }
    }

    class TopicCreateHandler : TodoDbHandler, IRequestHandler<TopicCreateRequest, CommandResponse>
    {
        public TopicCreateHandler(TodoDb db) : base(db)
        {
        }

        public async Task<CommandResponse> Handle(TopicCreateRequest request, CancellationToken cancellationToken)
        {
            if (await _db.Topics.AnyAsync(x => x.Name.ToUpper() == request.Name.ToUpper().Trim(), cancellationToken))
                return Error("Topic with the same name exists!");
            var entity = new Topic()
            {
                Name = request.Name.Trim()
            };

            _db.Topics.Add(entity);
            await _db.SaveChangesAsync(cancellationToken);
            return Success("Topic created siccessfully.", entity.Id);
        }
    }
}
