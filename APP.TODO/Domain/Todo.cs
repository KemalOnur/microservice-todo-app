using CORE.APP.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APP.TODO.Domain
{
    public class Todo : Entity
    {
        [Required]
        [StringLength(500)]
        public string Title { get; set; }

        public string Notes { get; set; }   

        public DateTime? CreatedAt { get; set; } 

        public DateTime? UpdatedAt { get; set; }

        public bool IsCompleted { get; set; }

        public List<TodoTopic> TodoTopics { get; set; } = new List<TodoTopic>();
        
        [NotMapped]
        public List<int> TopicIds
        {
            get => TodoTopics?.Select(todoTopic => todoTopic.TopicId).ToList();
            set => TodoTopics = value?.Select(v => new TodoTopic() { TopicId = v}).ToList();
        }
    }
}
