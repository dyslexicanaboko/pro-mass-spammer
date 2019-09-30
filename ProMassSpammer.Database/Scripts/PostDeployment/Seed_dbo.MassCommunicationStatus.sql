MERGE INTO [dbo].[MassCommunicationStatus] AS Target 
USING (VALUES 
	(0, N'Unsent', N'Communication has never been attempted to be sent.'),
	(1, N'Waiting', N'Communication is waiting to be picked up for sending.'),
	(2, N'Processing', N'Communication is being processed.'),
	(3, N'Sent', N'Communication has been processed.'),
	(4, N'Error', N'There was a soft error encountered while processing.'),
	(5, N'Failure', N'There was a hard error encountered while processing, this communication cannot be sent due to a runtime exception.')
)
AS Source ([MassCommunicationStatusId], [Name], [Description]) 
ON Target.[MassCommunicationStatusId] = Source.[MassCommunicationStatusId] 
-- update matched rows 
WHEN MATCHED THEN 
	UPDATE SET 
		 [Name] = Source.[Name] 
		,[Description] = Source.[Description]
-- insert new rows 
WHEN NOT MATCHED BY TARGET THEN 
	INSERT ([MassCommunicationStatusId], [Name], [Description]) 
	VALUES ([MassCommunicationStatusId], [Name], [Description]) 
-- delete rows that are in the target but not the source 
WHEN NOT MATCHED BY SOURCE THEN 
	DELETE;