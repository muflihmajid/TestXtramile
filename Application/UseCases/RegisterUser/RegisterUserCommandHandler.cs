using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using SceletonAPI.Application.Interfaces;
using MediatR;
using SceletonAPI.Domain.Entities;

namespace SceletonAPI.Application.UseCases.RegisterUser {
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserDto> {
        private readonly IDBContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public RegisterUserCommandHandler (IDBContext context, IMapper mapper, IMediator mediator) {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<RegisterUserDto> Handle (RegisterUserCommand request, CancellationToken cancellationToken) {
            var user = new User ();
            var response = new RegisterUserDto ();
            // object tiket
            user = new User () {
                LastUpdateDate = DateTime.Now,
                Email = request.Data.email,
                Phone = request.Data.phone,
                Name = request.Data.name,
                Company = request.Data.company,
                CreateDate = DateTime.Now,
                ExpiredDate = DateTime.Now.AddHours (1),
                Key = "Tettttttttttttt",
                Verivy = false,
                CreateBy = "Admin"
            };
            _context.Users.Add (user);
            await _context.SaveChangesAsync (cancellationToken);
            response.Success = true;
            response.Message = "Register Success";
            await _context.SaveChangesAsync (cancellationToken);
            return response;
        }
    }

}