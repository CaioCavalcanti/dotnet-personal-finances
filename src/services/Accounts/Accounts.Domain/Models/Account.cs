using System;

class Account
{
    public Account(string name, AccountType type, double initialBalance)
    {
        Name = name;
        Type = type;
        Balance = initialBalance;
    }

    public Guid Id { get; }
    public string Name { get; }
    public AccountType Type { get; }
    public double Balance { get; set; }
}