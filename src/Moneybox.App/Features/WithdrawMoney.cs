using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using System;

namespace Moneybox.App.Features
{
    public class WithdrawMoney
    {
        private IAccountRepository accountRepository;
        private INotificationService notificationService;

        public WithdrawMoney(IAccountRepository accountRepository, INotificationService notificationService)
        {
            this.accountRepository = accountRepository;
            this.notificationService = notificationService;
        }

        public void Execute(Guid fromAccountId, decimal amount)
        {
            // TODO:

            // get the account to withdraw from
            var from = this.accountRepository.GetAccountById(fromAccountId);

            // do validate and withdraw the money

            if (from.FundsAreAvailable(fromAccountId, amount))
            {
                from.Withdraw(amount);
            }

            
            if (from.Balance < 500m)
            {
                this.notificationService.NotifyFundsLow(from.User.Email);
            }

             this.accountRepository.Update(from);
        }
    }
}
