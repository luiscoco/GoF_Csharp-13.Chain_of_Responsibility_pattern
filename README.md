# GoF_Csharp_13.Chain_of_Responsibility_pattern

The Chain of Responsibility pattern is a behavioral design pattern that allows multiple objects to handle a request in a chain-like manner without 
the client explicitly specifying the receiver. Each handler in the chain has the option to handle the request or pass it to the next handler in the chain 
until it is processed or reaches the end of the chain.

Here's a simple explanation of the pattern:

Define a common interface for all handlers, which includes a method to handle the request.
Each handler implements the common interface and contains a reference to the next handler in the chain.
When a request is made, it starts at the first handler in the chain. If the current handler can handle the request, it does so. Otherwise, 
it passes the request to the next handler in the chain.
This process continues until a handler processes the request or until the chain ends.

Now, let's see an example in C#:

Suppose we have a simple bank transaction system, and we want to implement the Chain of Responsibility pattern to handle different types of transactions.

Output:

```
Transaction logged: Amount: 500, Account Balance: 1000
Transaction passed fraud detection. Proceeding...
Sufficient balance. Transaction successful!
Transaction logged: Amount: 2000, Account Balance: 1500
Transaction passed fraud detection. Proceeding...
Insufficient balance. Checking with the next handler...
Transaction logged: Amount: 300, Account Balance: 200
Insufficient balance. Checking with the next handler...
Transaction failed. Insufficient balance.
Transaction logged: Amount: 5000, Account Balance: 4000
Transaction passed fraud detection. Proceeding...
Sufficient balance. Transaction successful!
```

In this example, we set up a chain of responsibility with three handlers. Each handler tries to handle the transaction based on its specific logic. If a handler cannot handle the transaction, it passes the request to the next handler in the chain. The chain ends when a handler successfully handles the transaction or when no more handlers are available.

```csharp
﻿using System.Transactions;
using System;

public class ChainOfResponsibilityExample
{
    public static void Main()
    {
        // Creating the chain
        var logger = new TransactionLoggerHandler();
        var fraudDetection = new FraudDetectionHandler();
        var balanceChecker = new SufficientBalanceHandler();

        logger.SetNext(fraudDetection);
        fraudDetection.SetNext(balanceChecker);

        // Simulate different transactions
        Transaction transaction1 = new Transaction(500, 1000);
        Transaction transaction2 = new Transaction(2000, 1500);
        Transaction transaction3 = new Transaction(300, 200);
        Transaction transaction4 = new Transaction(5000, 4000);

        // Processing transactions through the chain
        logger.HandleTransaction(transaction1);
        logger.HandleTransaction(transaction2);
        logger.HandleTransaction(transaction3);
        logger.HandleTransaction(transaction4);
    }
}

//Step 1: Define the common interface for all handlers:
// Handler interface
public interface ITransactionHandler
{
    void SetNext(ITransactionHandler handler);
    void HandleTransaction(Transaction transaction);
}

//Step 2: Implement concrete handlers for different types of transactions:
// Concrete Handler 1: Check for Sufficient Balance
public class SufficientBalanceHandler : ITransactionHandler
{
    private ITransactionHandler _nextHandler;

    public void SetNext(ITransactionHandler handler)
    {
        _nextHandler = handler;
    }

    public void HandleTransaction(Transaction transaction)
    {
        if (transaction.Amount <= transaction.AccountBalance)
        {
            Console.WriteLine("Sufficient balance. Transaction successful!");
        }
        else if (_nextHandler != null)
        {
            Console.WriteLine("Insufficient balance. Checking with the next handler...");
            _nextHandler.HandleTransaction(transaction);
        }
        else
        {
            Console.WriteLine("Transaction failed. Insufficient balance.");
        }
    }
}

// Concrete Handler 2: Fraud Detection
public class FraudDetectionHandler : ITransactionHandler
{
    private ITransactionHandler _nextHandler;

    public void SetNext(ITransactionHandler handler)
    {
        _nextHandler = handler;
    }

    public void HandleTransaction(Transaction transaction)
    {
        // Simplified fraud detection logic
        if (transaction.Amount < 1000)
        {
            Console.WriteLine("Transaction passed fraud detection. Proceeding...");
        }
        else if (_nextHandler != null)
        {
            Console.WriteLine("Potential fraud detected. Checking with the next handler...");
            _nextHandler.HandleTransaction(transaction);
        }
        else
        {
            Console.WriteLine("Transaction failed. Potential fraud detected.");
        }
    }
}

// Concrete Handler 3: Transaction Logger
public class TransactionLoggerHandler : ITransactionHandler
{
    private ITransactionHandler _nextHandler;

    public void SetNext(ITransactionHandler handler)
    {
        _nextHandler = handler;
    }

    public void HandleTransaction(Transaction transaction)
    {
        Console.WriteLine($"Transaction logged: {transaction}");
        _nextHandler?.HandleTransaction(transaction);
    }
}


//Step 3: Create the Transaction class and set up the chain:
public class Transaction
{
    public decimal Amount { get; set; }
    public decimal AccountBalance { get; set; }

    public Transaction(decimal amount, decimal accountBalance)
    {
        Amount = amount;
        AccountBalance = accountBalance;
    }

    public override string ToString()
    {
        return $"Amount: {Amount}, Account Balance: {AccountBalance}";
    }
}
```

## How to setup Github actions

![Csharp Github actions](https://github.com/luiscoco/GoF_Csharp-13.Chain_of_Responsibility_pattern/assets/32194879/4b906030-0de2-48d2-90e9-1eef8c957d0f)
