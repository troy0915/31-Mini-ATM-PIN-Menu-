using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Account
{
    private string Pin;
    private decimal Balance;
    private bool IsLocked = false;
    private int FailedAttempts = 0;

    public Account(string pin, decimal initialBalance)
    {
        Pin = pin;
        Balance = initialBalance;
    }

    public bool Authenticate(string inputPin)
    {
        if (IsLocked) return false;

        if (inputPin == Pin)
        {
            FailedAttempts = 0;
            return true;
        }
        else
        {
            FailedAttempts++;
            if (FailedAttempts >= 3)
            {
                IsLocked = true;
                Console.WriteLine("Account locked due to too many failed attempts.");
            }
            else
            {
                Console.WriteLine($"Incorrect PIN. {3 - FailedAttempts} attempt(s) left.");
            }
            return false;
        }
    }

    public bool Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Deposit amount must be positive.");
            return false;
        }
        Balance += amount;
        Console.WriteLine($"Deposited {amount:C}. New balance: {Balance:C}");
        return true;
    }

    public bool Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Withdrawal amount must be positive.");
            return false;
        }
        if (amount > Balance)
        {
            Console.WriteLine("Insufficient funds.");
            return false;
        }
        Balance -= amount;
        Console.WriteLine($"Withdrew {amount:C}. New balance: {Balance:C}");
        return true;
    }

    public void ShowBalance()
    {
        Console.WriteLine($"Current balance: {Balance:C}");
    }

    public bool IsAccountLocked()
    {
        return IsLocked;
    }
}
namespace _31__Mini_ATM__PIN___Menu_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string pin = "1234";
            decimal initialBalance = 1000m;
            var account = new Account(pin, initialBalance);

            while (!account.IsAccountLocked())
            {
                Console.Write("Enter PIN: ");
                string inputPin = Console.ReadLine();
                if (account.Authenticate(inputPin))
                    break;
            }

            if (account.IsAccountLocked()) return;

            while (true)
            {
                Console.WriteLine("\nATM Menu:");
                Console.WriteLine("1. Withdraw");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Check Balance");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter amount to withdraw: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal wAmt))
                            account.Withdraw(wAmt);
                        else
                            Console.WriteLine("Invalid amount.");
                        break;

                    case "2":
                        Console.Write("Enter amount to deposit: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal dAmt))
                            account.Deposit(dAmt);
                        else
                            Console.WriteLine("Invalid amount.");
                        break;

                    case "3":
                        account.ShowBalance();
                        break;

                    case "4":
                        Console.WriteLine("Thank you for using the ATM. Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }
}
