MERGE INTO [dbo].[TransmissionStatus] AS Target 
USING (VALUES 
	(0, N'Waiting', N'Communication is waiting to be picked up for sending.'),
	(1, N'Processing', N'Communication is being processed.'),
	(2, N'Sent', N'Communication has been processed.'),
	(3, N'Error', N'There was a soft error encountered while processing.'),
	(4, N'First Attempt', N'If a soft error is encountered for the first time then the communication will be tried again after a cool down.'),
	(5, N'Second Attempt', N'If a soft error is encountered for the second time then the communication will be tried again after a cool down.'),
	(6, N'Third Attempt', N'If a soft error is encountered for the third time then the communication will be marked as an error and it won''t be tried again.')
)
AS Source ([TransmissionStatusId], [Name], [Description]) 
ON Target.[TransmissionStatusId] = Source.[TransmissionStatusId] 
-- update matched rows 
WHEN MATCHED THEN 
	UPDATE SET 
		 [Name] = Source.[Name] 
		,[Description] = Source.[Description]
-- insert new rows 
WHEN NOT MATCHED BY TARGET THEN 
	INSERT ([TransmissionStatusId], [Name], [Description]) 
	VALUES ([TransmissionStatusId], [Name], [Description]) 
-- delete rows that are in the target but not the source 
WHEN NOT MATCHED BY SOURCE THEN 
	DELETE;