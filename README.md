# GoF_Csharp_13.Chain-_of-_Responsibility_pattern

The Chain of Responsibility pattern is a behavioral design pattern that allows multiple objects to handle a request in a chain-like manner without 
the client explicitly specifying the receiver. Each handler in the chain has the option to handle the request or pass it to the next handler in the chain 
until it is processed or reaches the end of the chain.

Here's a simple explanation of the pattern:

Define a common interface for all handlers, which includes a method to handle the request.
Each handler implements the common interface and contains a reference to the next handler in the chain.
When a request is made, it starts at the first handler in the chain. If the current handler can handle the request, it does so. Otherwise, 
it passes the request to the next handler in the chain.
This process continues until a handler processes the request or until the chain ends.



