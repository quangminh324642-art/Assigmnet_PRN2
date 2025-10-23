using BusinessObjects;
using ProductManagementDemo.Models;

namespace DataAccessLayer
{
    public class AccountDAO
    {
        public static AccountMember GetAccountById(string accountID)
        {
            using var context = new MyStoreContext();
            var accountMember = context.AccountMembers.FirstOrDefault(a => a.MemberID == accountID);
            return accountMember;
        }
    }

}

