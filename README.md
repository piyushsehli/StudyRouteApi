# StudyRouteApi

Purpose of this project is to enlighten the study path for every student who
wants to come to Canada for a better future in studies.

API providing information about colleges and universities from all over
Canada based on Study Streams, Programs, Courses, Part-time, Full-time,
Student satisfaction rate (ratings), Fees, Admission Requirements, Summer
breaks and Co-ops.

Allow different web and mobile clients to have successful access to the
information

## Technology Architecture

###ASP.NET CORE API

Repository pattern: To create an abstraction layer between data access and business logic layer.

Scaffold-Database: To generate entities

Automapper: For object-to-object mapping.

Get By Id/All, Post, Put and Delete: Http Request types

###RDS - SQL Server: Supporting database for various CRUD operations.

###Swagger Docs: API documentation

###AWS Elastic Beanstalk - Deployment: For API deployment.

###Apigee
API Management

Security Policy: API-Verify-Key

API Portal: Build and publish developer portal

###Test Client

###ASP.NET Core Web Application
