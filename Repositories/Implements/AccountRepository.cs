using BusinessObjects;
using DataAccessObjects;
using Repositories.Interfaces;

namespace Repositories.Implements
{
	public class AccountRepository : IAccountRepository
	{
		private readonly AccountDAO _accountDAO;

		public AccountRepository(MyStoreContext context)
		{
			_accountDAO = new AccountDAO(context);
		}

		public List<AccountMember> GetAllAccounts()
		{
			return _accountDAO.GetAllAccounts();
		}

		public AccountMember GetAccountById(string id)
		{
			return _accountDAO.GetAccountById(id);
		}

		public AccountMember GetAccountByEmail(string email)
		{
			return _accountDAO.GetAccountByEmail(email);
		}

		public AccountMember Authenticate(string email, string password)
		{
			return _accountDAO.Authenticate(email, password);
		}

		public void SaveAccount(AccountMember account)
		{
			_accountDAO.SaveAccount(account);
		}

		public void UpdateAccount(AccountMember account)
		{
			_accountDAO.UpdateAccount(account);
		}

		public void DeleteAccount(string id)
		{
			_accountDAO.DeleteAccount(id);
		}
	}
}
