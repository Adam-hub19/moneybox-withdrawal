using System;

namespace Moneybox.App
{
    public class Account
    {
        public const decimal PayInLimit = 4000m;

        public Guid Id { get; set; }

        public User User { get; set; }

        public decimal Balance { get; set; }

        public decimal Withdrawn { get; set; }

        public decimal PaidIn { get; set; }

        // let us change this Account model from plain old object to one with some rich behaviours

        /*
         * Let's take care of validations first
         */

            // check if sufficient funds are available
        public bool FundsAreAvailable(Guid id, decimal amount)
        {
            bool areAvailable = false;
            if (this.Id == id && this.Balance - amount > 0m )
	        {
                areAvailable = true;
	        }
            else
            {
                throw new InvalidOperationException("Insufficient funds");
            }      

            return areAvailable;

        }

        // check for paid in limit

        public bool CheckPayInLimit(decimal amount)
       {
             bool isOk = true;
            if (this.PaidIn + amount > PayInLimit)
              {
                isOk = false;
                throw new InvalidOperationException("Account pay in limit reached");
                
              }

            return isOk;
        }

        // withdraw money

        public decimal Withdraw(decimal amount)
        {
            this.Balance -= amount;
            this.Withdrawn -= amount;

            return Withdrawn;
        }

        // make a deposit

         public void Deposit(decimal amount)
        {
            this.Balance += amount;
            this.PaidIn += amount;

            return PaidIn;
        }




    }
}
