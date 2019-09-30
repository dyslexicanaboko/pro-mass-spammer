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

PRINT 'Seed_dbo.DeliveryMethod.sql'

:r .\Seed_dbo.DeliveryMethod.sql

GO

PRINT 'Seed_dbo.MassCommunicationStatus.sql'

:r .\Seed_dbo.MassCommunicationStatus.sql

GO

PRINT 'Seed_dbo.TransmissionStatus.sql'

:r .\Seed_dbo.TransmissionStatus.sql
