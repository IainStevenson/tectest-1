/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
USE [tectest1.database]
GO
IF ((SELECT COUNT(*) FROM dbo.Accounts) = 0)
BEGIN
    INSERT INTO [dbo].[Accounts] ([AccountId] ,[FirstName] ,[LastName]) SELECT 2344,'Tommy','Test'
    INSERT INTO [dbo].[Accounts] ([AccountId] ,[FirstName] ,[LastName]) SELECT 2233,'Barry','Test'
    INSERT INTO [dbo].[Accounts] ([AccountId] ,[FirstName] ,[LastName]) SELECT 8766,'Sally','Test'
    INSERT INTO [dbo].[Accounts] ([AccountId] ,[FirstName] ,[LastName]) SELECT 2345,'Jerry','Test'
    INSERT INTO [dbo].[Accounts] ([AccountId] ,[FirstName] ,[LastName]) SELECT 2346,'Ollie','Test'
    INSERT INTO [dbo].[Accounts] ([AccountId] ,[FirstName] ,[LastName]) SELECT 2347,'Tara','Test'
    INSERT INTO [dbo].[Accounts] ([AccountId] ,[FirstName] ,[LastName]) SELECT 2348,'Tammy','Test'
    INSERT INTO [dbo].[Accounts] ([AccountId] ,[FirstName] ,[LastName]) SELECT 2349,'Simon','Test'
    INSERT INTO [dbo].[Accounts] ([AccountId] ,[FirstName] ,[LastName]) SELECT 2350,'Colin','Test'
    INSERT INTO [dbo].[Accounts] ([AccountId] ,[FirstName] ,[LastName]) SELECT 2351,'Gladys','Test'
    INSERT INTO [dbo].[Accounts] ([AccountId] ,[FirstName] ,[LastName]) SELECT 2352,'Greg','Test'
    INSERT INTO [dbo].[Accounts] ([AccountId] ,[FirstName] ,[LastName]) SELECT 2353,'Tony','Test'
    INSERT INTO [dbo].[Accounts] ([AccountId] ,[FirstName] ,[LastName]) SELECT 2355,'Arthur','Test'
    INSERT INTO [dbo].[Accounts] ([AccountId] ,[FirstName] ,[LastName]) SELECT 2356,'Craig','Test'
    INSERT INTO [dbo].[Accounts] ([AccountId] ,[FirstName] ,[LastName]) SELECT 6776,'Laura','Test'
    INSERT INTO [dbo].[Accounts] ([AccountId] ,[FirstName] ,[LastName]) SELECT 4534,'JOSH','TEST'
    INSERT INTO [dbo].[Accounts] ([AccountId] ,[FirstName] ,[LastName]) SELECT 1234,'Freya','Test'
    INSERT INTO [dbo].[Accounts] ([AccountId] ,[FirstName] ,[LastName]) SELECT 1239,'Noddy','Test'
    INSERT INTO [dbo].[Accounts] ([AccountId] ,[FirstName] ,[LastName]) SELECT 1240,'Archie','Test'
    INSERT INTO [dbo].[Accounts] ([AccountId] ,[FirstName] ,[LastName]) SELECT 1241,'Lara','Test'
    INSERT INTO [dbo].[Accounts] ([AccountId] ,[FirstName] ,[LastName]) SELECT 1242,'Tim','Test'
    INSERT INTO [dbo].[Accounts] ([AccountId] ,[FirstName] ,[LastName]) SELECT 1243,'Graham','Test'
    INSERT INTO [dbo].[Accounts] ([AccountId] ,[FirstName] ,[LastName]) SELECT 1244,'Tony','Test'
    INSERT INTO [dbo].[Accounts] ([AccountId] ,[FirstName] ,[LastName]) SELECT 1245,'Neville','Test'
    INSERT INTO [dbo].[Accounts] ([AccountId] ,[FirstName] ,[LastName]) SELECT 1246,'Jo','Test'
    INSERT INTO [dbo].[Accounts] ([AccountId] ,[FirstName] ,[LastName]) SELECT 1247,'Jim','Test'
    INSERT INTO [dbo].[Accounts] ([AccountId] ,[FirstName] ,[LastName]) SELECT 1248,'Pam','Test'
END