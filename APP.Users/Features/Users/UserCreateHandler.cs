using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using APP.Users.Domain;
using CORE.APP.Features;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace APP.Users.Features.Users
{
    public class UserCreateRequest : Request, IRequest<CommandResponse>
    {
        [Required]
        [StringLength(1000)]

        public string Name { get; set; }

        [JsonIgnore]
        public override int Id { get => base.Id; set => base.Id = value; }
    }

    class TopicCreateHandler : UsersDbHandler, IRequestHandler<UserCreateRequest, CommandResponse>
    {
        public TopicCreateHandler(UsersDb db) : base(db)
        {
        }

        public async Task<CommandResponse> Handle(UserCreateRequest request, CancellationToken cancellationToken)
        {
            if (await _db.Users.AnyAsync(x => x.UserName.ToUpper() == request.Name.ToUpper().Trim(), cancellationToken))
                return Error("Topic with the same name exists!");
            var entity = new User()
            {
                UserName = request.Name.Trim()
            };

            _db.Users.Add(entity);
            await _db.SaveChangesAsync(cancellationToken);
            return Success("User created successfully.", entity.Id);
        }
    }
}
