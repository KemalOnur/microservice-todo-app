
using APP.TODO.Domain;
using CORE.APP.Features;
using MediatR;

namespace APP.TODO.Features.Topics
{

    public class TopicDeleteRequest : Request, IRequest<CommandResponse>
    {

    }


    class TopicDeleteHandler : TodoDbHandler, IRequestHandler<TopicDeleteRequest, CommandResponse>
    {
        public TopicDeleteHandler(TodoDb db) : base(db)
        {
        }

        public async Task<CommandResponse> Handle(TopicDeleteRequest request, CancellationToken cancellationToken)
        {
            var entity = await _db.Topics.FindAsync(request.Id, cancellationToken);

            if (entity is null)
                return Error("Topic not Found!!!");

            _db.Topics.Remove(entity);

            await _db.SaveChangesAsync(cancellationToken);

            return Success("Topic Deleted Successfully", entity.Id);
        }
    }
}
