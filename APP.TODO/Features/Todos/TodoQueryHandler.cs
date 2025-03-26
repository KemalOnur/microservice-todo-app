using APP.TODO.Domain;
using CORE.APP.Features;
using MediatR;
using System.Data.Entity.Core.Metadata.Edm;
using System.Globalization;

namespace APP.TODO.Features.Todos
{
    public class TodoQueryRequest : Request, IRequest<IQueryable<TodoQueryResponse>>
    {

    }

    public class TodoQueryResponse : QueryResponse
    {
        public string Title { get; set; }

        public string Notes { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string CreatedAtF {  get; set; }

        public string UpdatedAtF{ get; set; }

        public bool IsCompleted { get; set; }

        public List<int> TopicIds { get; set; }

    }

    public class TodoQueryHandler : TodoDbHandler, IRequestHandler<TodoQueryRequest, IQueryable<TodoQueryResponse>>
    {
        public TodoQueryHandler(TodoDb db) : base(db)
        {
        }

        public Task<IQueryable<TodoQueryResponse>> Handle(TodoQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _db.Todos.OrderBy(x => x.IsCompleted).ThenByDescending(x => x.CreatedAt)
                .Select(x => new TodoQueryResponse()
                {
                    Id = x.Id,
                    IsCompleted = x.IsCompleted,
                    Notes = x.Notes,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    Title = x.Title,
                    TopicIds = x.TopicIds,
                    CreatedAtF = x.CreatedAt == null ? string.Empty : x.CreatedAt.Value.ToString("MM/dd/yyyy HH:mm:ss.", new CultureInfo("tr-TR")),
                    UpdatedAtF= x.UpdatedAt == null ? string.Empty : x.UpdatedAt.Value.ToString("MM/dd/yyyy HH:mm:ss.", new CultureInfo("tr-TR")),

                });

            return Task.FromResult(query);
        }
    }

}

