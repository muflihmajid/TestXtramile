using MediatR;
using SceletonAPI.Application.Models.Query;

namespace SceletonAPI.Application.UseCases.RegisterUser
{
    public class RegisterUserCommand : IRequest<RegisterUserDto>
    {
        public RegisterCommandData Data { set; get; }
    }

    public class RegisterCommandData
    {
        public string email { set; get; }
        public string phone { set; get; }
        public string name { set; get; }
        public string company { set; get; }

    }
}
