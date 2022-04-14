### This is an ASP.NET MVC web site for news viewing. The backend is ASP.NET MVC. The frontend part is a React app.
***
## Backend
* ASP.Nen 6,
* For the database I used MS Sql Server,
* For data requests I used EntityFrameworkCore,
* Cookie Authentication and Authorization by roles,
* JSON Serialization used to send data to the client.

The solution defines two types of controllers. Some for working with the Frontend part, others for views inside ASP.NET.

You can perform CRUD operations using views inside ASP.NET. Only authorized users with the admin role can perform these operations.

***
## Frontend

React application that sends requests to the server and generates a response.

It has a main page that displays the latest three news. She does this by sending an appropriate request to the server, which returns data to her.

There is also a page that takes all the news and displays them as a list. Clicking on any news opens its own page.

For localization, I used the "i18next" library.
***
