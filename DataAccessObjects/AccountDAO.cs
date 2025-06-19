using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObjects
{
	public class AccountDAO
	{
		private readonly MyStoreContext _context;

		public AccountDAO(MyStoreContext context)
		{
			_context = context;
		}

		public List<AccountMember> GetAllAccounts()
		{
			return _context.AccountMembers.ToList();
		}

		public AccountMember? GetAccountById(string id)
		{
			return _context.AccountMembers.FirstOrDefault(a => a.MemberId == id);
		}

		public AccountMember? GetAccountByEmail(string email)
		{
			return _context.AccountMembers.FirstOrDefault(a => a.EmailAddress == email);
		}

		public AccountMember? Authenticate(string email, string password)
		{
			return _context.AccountMembers.FirstOrDefault(a => 
				a.EmailAddress == email && a.MemberPassword == password);
		}

		public void SaveAccount(AccountMember account)
		{
			_context.AccountMembers.Add(account);
			_context.SaveChanges();
		}

		public void UpdateAccount(AccountMember account)
		{
			_context.Entry(account).State = EntityState.Modified;
			_context.SaveChanges();
		}

		public void DeleteAccount(string id)
		{
			var account = _context.AccountMembers.Find(id);
			if (account != null)
			{
				_context.AccountMembers.Remove(account);
				_context.SaveChanges();
			}
		}
	}
}
