

using APP.TODO.Domain;
using CORE.APP.Features;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace APP.TODO.Features.Topics
{
    public class TopicUpdateRequest : Request, IRequest<CommandResponse>
    {
        [Required]
        [StringLength(50)]

        public string Name { get; set; }
    }

    public class TopicUpdateHandler : TodoDbHandler, IRequestHandler<TopicUpdateRequest, CommandResponse>
    {
        public TopicUpdateHandler(TodoDb db) : base(db)
        {
        }

        public async Task<CommandResponse> Handle(TopicUpdateRequest request, CancellationToken cancellationToken)
        {
            if (await _db.Topics.AnyAsync(x => x.Id != request.Id && x.Name.ToUpper() == request.Name.ToUpper().Trim(), cancellationToken))
                return Error("Topic with the same name EXISTS!!");


            var entity = await _db.Topics.FindAsync(request.Id, cancellationToken);

            if(entity is null)
            {
                return Error("Topic not found");
            }

            entity.Name = request.Name.Trim();
            _db.Topics.Update(entity);
            await _db.SaveChangesAsync(cancellationToken);
            return Success("Topic Updated Successfully", entity.Id);
            
        }
    }
}
