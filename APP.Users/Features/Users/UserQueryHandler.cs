using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using APP.Users.Domain;
using APP.Users.Features.Roles;
using APP.Users.Features.Skills;
using CORE.APP.Features;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace APP.Users.Features.Users
{
    public class UserQueryRequest : Request, IRequest<IQueryable<UserQueryResponse>> 
    {
        [JsonIgnore]
        public override int Id { get => base.Id; set => base.Id = value; }

        [Required, StringLength(30, MinimumLength = 3)]
        public string UserName { get; set; }

        [Required, StringLength(10, MinimumLength = 3)]
        public string Password { get; set; }
    }

    public class UserQueryResponse : QueryResponse
    {

        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public string Active {  get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName  { get; set; }
        public int RoleId { get; set; }
        public RoleQueryResponse Role {  get; set; }
        public List<SkillQueryResponse> Skills { get; set; }

        public List<int> SkillIds { get; set; }

    }

    public class UserQueryHandler : UsersDbHandler, IRequestHandler<UserQueryRequest, IQueryable<UserQueryResponse>>
    {
        public UserQueryHandler(UsersDb db) : base(db)
        {
        }

        public Task<IQueryable<UserQueryResponse>> Handle(UserQueryRequest request, CancellationToken cancellationToken)
        {
            var entityQuery = _db.Users.Include(u => u.Role).Include(u => u.UserSkills).ThenInclude(us => us.Skill).OrderByDescending(u => u.IsActive).ThenBy(u => u.UserName).AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.UserName) && !string.IsNullOrWhiteSpace(request.Password))
                entityQuery = entityQuery.Where(u => u.UserName == request.UserName && u.Password == request.Password);


            var query = entityQuery.Select(u => new UserQueryResponse()
            {
                Active = u.IsActive ? "Active" : "Not Active",
                FirstName = u.FirstName,
                FullName = u.FirstName + " " + u.LastName,
                Id = u.Id,
                IsActive = u.IsActive,
                LastName = u.LastName,
                UserName = u.UserName,
                Password = u.Password,
                Role = new RoleQueryResponse()
                {
                    Name = u.Role.Name,
                },
                RoleId = u.RoleId,
                SkillIds = u.SkillIds,
                Skills = u.UserSkills.Select(us => new SkillQueryResponse()
                {
                    Name = us.Skill.Name
                }).ToList()
            });
            return Task.FromResult(query);
           

        }
    }
}
