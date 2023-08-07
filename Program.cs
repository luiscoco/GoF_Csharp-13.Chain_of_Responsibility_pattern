using System.Transactions;
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