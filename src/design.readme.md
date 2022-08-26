# Code design

## Decisions

* .net 6 web api VS 2002 default cross platform
* Security is not a factor in this test. But is usually #1 priority.
* Test driven development/design
* Json return data type, text/plain or application/octet-stream input type.
* Using Mediatr to simulate out-of-process message processing but in-process. Encorages breaking down clases to simpler versions. 
* Using NUnit as its out of the box with VS 2022 but could have used XUnit or anything else.
* I prefer NSubstitute for mocking but again can use Moq, or whatever.
* As a client I an choosing at this stage the following.
    * Swagger - not terribly useful on this one as it cant really help drive the API method of this design.
    * POSTMAN - there is a postman collection in the solution
    * The reason that I leverage POSTMAN for json API's integration testing for automation in build systems. 
    * Too long since I worked on Angular to start a client now.

For many reasons I always take the database first approach as a result you will see the dacpac project with accounts record setup.

# Issues

The concerns of security aside, requiring something like APIM to front and protect the API and assist with authentication and authorisation, the main problem here is the unstructured nature of text based CSV files and defending against accidental or malicious rubbish ariving.

I tested this by passing an EXE through the API and it rejects it reporting the number of bytes rejected not rows.

I over engineered the validation to internally previde Reasons why a row failed during validation and simply excised that from the client response using JsonIgnore. this helps somewhat with the testing.

I took the route of a two stage hybrid custom/fluent validation to fail early when there is no hope of extracting objects to validate which when obtained anre validated via the fluentvalidation package, and use its rules based system.

I opted not to use AutoMapper as this is a simple mapping requirement.

Its been a long time since I did a file upload scenario and now realise why I avoid them LOL.

I tested this with text/plain and application/octet-stream content types and eventually landed on application/octet-stream.

I decided to have a refresher course on using EntityFramework as the ORM instead of Dapper. Which came in useful for the linq statement to check for later or same time readings to reject stale reading values.