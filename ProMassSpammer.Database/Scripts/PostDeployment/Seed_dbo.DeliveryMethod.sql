MERGE INTO [dbo].[DeliveryMethod] AS Target 
USING (VALUES 
	(0, N'Email', N'Send communications as a separate email to each recipient.', 1),
	(1, N'SMS', N'Send communications as a text to each recipient.', 1),
	(2, N'IM', N'Send each communication as an Instant Message to each recipient.', 1),
	(3, N'Push Notification', N'Send each communication as a push notification to configured subscribers.', 1)
)
AS Source ([DeliveryMethodId], [Name], [Description], [IsActive]) 
ON Target.[DeliveryMethodId] = Source.[DeliveryMethodId] 
-- update matched rows 
WHEN MATCHED THEN 
	UPDATE SET 
		 [Name] = Source.[Name] 
		,[Description] = Source.[Description]
		,[IsActive] = Source.[IsActive]
-- insert new rows 
WHEN NOT MATCHED BY TARGET THEN 
	INSERT ([DeliveryMethodId], [Name], [Description], [IsActive]) 
	VALUES ([DeliveryMethodId], [Name], [Description], [IsActive]) 
-- delete rows that are in the target but not the source 
WHEN NOT MATCHED BY SOURCE THEN 
	DELETE;