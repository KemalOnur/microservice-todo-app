using APP.TODO.Domain;
using CORE.APP.Features;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.TODO.Features.Contents
{
    public class ContentQueryRequest : Request, IRequest<IQueryable<ContentQueryResponse>>
    {

    }

    public class ContentQueryResponse : QueryResponse
    {
        public string Name { get; set; }

        public string TodoTitles { get; set; }

        public int NumberOfTodos { get; set; }
    }

    public class ContentQueryHandler : TodoDbHandler, IRequestHandler<ContentQueryRequest, IQueryable<ContentQueryResponse>>
    {
        public ContentQueryHandler(TodoDb db) : base(db)
        {
        }

        public Task<IQueryable<ContentQueryResponse>> Handle(ContentQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _db.Contents.OrderBy(x => x.Name).Select(x => new ContentQueryResponse()
            {
                Id = x.Id,
                Name = x.Name,
                NumberOfTodos = x.Todos.Count(),
                TodoTitles = string.Join(" , ", x.Todos.Select(t => t.Title))

            });
            return Task.FromResult(query);
        }
    }
}
