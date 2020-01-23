# biosys
A research biobanking application built in C# ASP.NET MVC

This is a database-driven n-tier C# ASP.NET MVC application for managing research biosampling. The database underpinning the application is sufficiently generic and normalised to allow for any type of sampling and biosampling activity.

This application assumes an ASP.Net Identity Database First approach, using the standard user and role management security database on a SQL Server platform in combination with the host application BioSys database on the same SQL Server platform. You will need to set up the security database yourself and add the connection information to the application’s webconfig file.

The developer simply needs to create a view to simulate the sample collection and or processing form(s) in use and then integrate the view with controller and/or client side javascript and the model will then handle the rest of processing. The data tier’s [Study].[FormsAndLinks] table enables dynamic form navigation used by the javascript layer.
The application currently features three sample form views you can modify: two for blood sample collection and one for urine sample collection. The system assumed all form have a form ID which is linked to a study, so you are free to adopt this aspect of the data model, or adapt it to your own, necessitating some additional development.

This development of this application is ongoing, so further versions will appear here on GitHub over time.  
