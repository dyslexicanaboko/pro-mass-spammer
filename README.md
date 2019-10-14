# pro-mass-spammer
A very basic boilerplate data driven mass communication tool with the capability of expanding to any type of transmission/delivery method. The purpose of this project is to have a starting point that can be adopted to any project's need. It is usually a bad idea to have separate applications sending email, especially in the case of a SQL Server - this responsibility should be abstracted away into to a service to handle. This project will track all communications sent.

## Technology
* dot.net core 2.1 
* dot.net EF core
* MailKit for SMTP transmission
* Twilio Client for SMS transmission
* SimpleInjector for Dependency Injection

## Transmission methods
A developer has the opportunity to keep adding in different transmission methods. It doesn't matter what is being used so long as the paradigm is the same. Meaning a message is prepared for send, then the message is sent.
