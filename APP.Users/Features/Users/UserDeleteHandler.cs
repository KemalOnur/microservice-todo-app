using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APP.Users.Domain;
using CORE.APP.Features;
using MediatR;

namespace APP.Users.Features.Users
{
        public class UserDeleteRequest : Request, IRequest<CommandResponse>
        {

        }


        class UserDeleteHandler : UsersDbHandler, IRequestHandler<UserDeleteRequest, CommandResponse>
        {
            public UserDeleteHandler(UsersDb db) : base(db)
            {
            }

            public async Task<CommandResponse> Handle(UserDeleteRequest request, CancellationToken cancellationToken)
            {
                var entity = await _db.Users.FindAsync(request.Id, cancellationToken);

                if (entity is null)
                    return Error("Topic not Found!!!");

                _db.Users.Remove(entity);

                await _db.SaveChangesAsync(cancellationToken);

                return Success("Topic Deleted Successfully", entity.Id);
            }
        }
    
}
