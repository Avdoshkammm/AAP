using AAP.Application.DTO;
using AAP.Application.Interfaces;
using AAP.Domain.Entities;
using AAP.Domain.Interfaces;
using AutoMapper;

namespace AAP.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper mapper;
        private readonly IAccountRepository repository;
        public AccountService(IMapper _mapper, IAccountRepository _repository)
        {
            mapper = _mapper;
            repository = _repository;
        }

        public async Task Login(UserDTO userdto)
        {
            var user = mapper.Map<User>(userdto);
            await repository.Login(user);
        }

        public async Task Register(UserDTO userdto, string password)
        {
            User user = mapper.Map<User>(userdto);
            await repository.Register(user, password);
        }


    }
}
