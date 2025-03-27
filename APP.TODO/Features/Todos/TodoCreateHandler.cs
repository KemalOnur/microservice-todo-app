using APP.TODO.Domain;
using CORE.APP.Features;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APP.TODO.Features.Todos
{

    public class TodoCreateRequest : Request, IRequest<CommandResponse>
    {
        [Required]
        [StringLength(500)]
        public string Title { get; set; }
        [StringLength(4000)]
        public string Notes { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool IsCompleted { get; set; }

        public List<int> TopicIds { get; set; }

        [JsonIgnore]
        public override int Id { get => base.Id; set => base.Id = value; }

        public  int? ContentId { get; set; }
    }


    public class TodoCreateHandler : TodoDbHandler, IRequestHandler<TodoCreateRequest, CommandResponse>
    {
        public TodoCreateHandler(TodoDb db) : base(db)
        {
        }

        public async Task<CommandResponse> Handle(TodoCreateRequest request, CancellationToken cancellationToken)
        {
            if (await _db.Todos.AnyAsync(x => x.Title.ToUpper() == request.Title.ToUpper().Trim() && x.IsCompleted != true))
                return Error("Not Completed Notes with the same title exist");

            var entity = new Todo()
            {
                IsCompleted = request.IsCompleted,
                Notes = request.Notes?.Trim(),
                Title = request.Title.Trim(),
                CreatedAt = request.CreatedAt,
                UpdatedAt = request.UpdatedAt,
                TopicIds = request.TopicIds,
                ContentId = request.ContentId
            };

            _db.Todos.Add(entity);
            await _db.SaveChangesAsync(cancellationToken);

            return Success("Notes Created Successfully");
        }
    }
}
