using Assignment3.Services.DataAccess;

namespace Assignment3.Services
{
    public class AccountService : IAccountService {
        private  IAccountDataMapper _mapper;
        public AccountService(IAccountDataMapper mapper) {
            _mapper = mapper;
        }

        public void AddVal() {
            _mapper.AddValue();
        }
    }
}