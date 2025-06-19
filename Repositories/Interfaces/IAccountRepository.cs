using BusinessObjects;

namespace Repositories.Interfaces
{
	public interface IAccountRepository
	{
		List<AccountMember> GetAllAccounts();
		AccountMember GetAccountById(string id);
		AccountMember GetAccountByEmail(string email);
		AccountMember Authenticate(string email, string password);
		void SaveAccount(AccountMember account);
		void UpdateAccount(AccountMember account);
		void DeleteAccount(string id);
	}
}
