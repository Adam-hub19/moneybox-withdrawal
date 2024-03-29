﻿using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using System;

namespace Moneybox.App.Features
{
    public class TransferMoney
    {
        private IAccountRepository accountRepository;
        private INotificationService notificationService;

        public TransferMoney(IAccountRepository accountRepository, INotificationService notificationService)
        {
            this.accountRepository = accountRepository;
            this.notificationService = notificationService;
        }

        public void Execute(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            var from = this.accountRepository.GetAccountById(fromAccountId);
            var to = this.accountRepository.GetAccountById(toAccountId);

            if (from.FundsAreAvailable(fromAccountId,amount) && to.checkPayInLimit(amount))
             {
                from.Withdraw(amount);
                to.Deposit(amount);
             }
          

            if (from.Balance < 500m)
            {
                this.notificationService.NotifyFundsLow(from.User.Email);
            }

            if (Account.PayInLimit - paidIn < 500m)
            {
                this.notificationService.NotifyApproachingPayInLimit(to.User.Email);
            }


            this.accountRepository.Update(from);
            this.accountRepository.Update(to);
        }
    }
}
