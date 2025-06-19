using BusinessObjects;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implements
{
	public class AccountService : IAccountService
	{
		private readonly IAccountRepository _accountRepository;

		public AccountService(IAccountRepository accountRepository)
		{
			_accountRepository = accountRepository;
		}

		public List<AccountMember> GetAllAccounts()
		{
			return _accountRepository.GetAllAccounts();
		}

		public AccountMember GetAccountById(string id)
		{
			return _accountRepository.GetAccountById(id);
		}

		public AccountMember GetAccountByEmail(string email)
		{
			return _accountRepository.GetAccountByEmail(email);
		}

		public AccountMember Authenticate(string email, string password)
		{
			return _accountRepository.Authenticate(email, password);
		}

		public void SaveAccount(AccountMember account)
		{
			_accountRepository.SaveAccount(account);
		}

		public void UpdateAccount(AccountMember account)
		{
			_accountRepository.UpdateAccount(account);
		}

		public void DeleteAccount(string id)
		{
			_accountRepository.DeleteAccount(id);
		}
	}
}
