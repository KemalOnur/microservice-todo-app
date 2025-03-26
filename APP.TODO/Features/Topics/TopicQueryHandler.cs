using APP.TODO.Domain;
using CORE.APP.Features;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace APP.TODO.Features.Topics
{
    public class TopicQueryRequest : Request, IRequest<IQueryable<TopicQueryResponse>>
    {
    }

    public class TopicQueryResponse : QueryResponse 
    {
        public string Name { get; set; }
    }


    public class TopicQueryHandler : TodoDbHandler, IRequestHandler<TopicQueryRequest, IQueryable<TopicQueryResponse>>
    {
        public TopicQueryHandler(TodoDb db) : base(db)
        {
        }

        public Task<IQueryable<TopicQueryResponse>> Handle(TopicQueryRequest request, CancellationToken cancellationToken)
        {
            IQueryable<TopicQueryResponse> query = _db.Topics.OrderBy(t => t.Name).Select(t => new TopicQueryResponse()
            {
                Id = t.Id,
                Name = t.Name,
            });

            return Task.FromResult(query);
        }
    }
}
