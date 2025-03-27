using APP.TODO.Domain;
using CORE.APP.Features;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace APP.TODO.Features.Todos
{

    public class TodoUpdateRequest : Request, IRequest<CommandResponse>
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

        public int? ContentId { get; set; }
    }


    public class TodoUpdateHandler : TodoDbHandler, IRequestHandler<TodoUpdateRequest, CommandResponse>
    {
        public TodoUpdateHandler(TodoDb db) : base(db)
        {
        }

        public async Task<CommandResponse> Handle(TodoUpdateRequest request, CancellationToken cancellationToken)
        {
            if (await _db.Todos.AnyAsync(x => x.Id != request.Id && x.Title.ToUpper() == request.Title.ToUpper().Trim() && x.IsCompleted != true))
                return Error("Not Completed Notes with the same title exist");

            
            var entity = await _db.Todos.Include(x => x.TodoTopics).SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
                return Error("Todo doesnt exists");

            _db.TodoTopics.RemoveRange(entity.TodoTopics);

            entity.Id = request.Id;
            entity.IsCompleted = request.IsCompleted;
            entity.Notes = request.Notes?.Trim();
            entity.Title = request.Title.Trim();
            entity.UpdatedAt = request.UpdatedAt;
            entity.TopicIds = request.TopicIds;
            entity.ContentId = request.ContentId;

            _db.Todos.Update(entity);
            await _db.SaveChangesAsync(cancellationToken);

            return Success("Notes Updated Successfully", entity.Id);
        }
    }
}
