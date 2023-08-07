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

