USE [ProMassSpammer]
GO

SELECT * FROM dbo.MassCommunicationStatus
SELECT * FROM dbo.DeliveryMethod

INSERT INTO [dbo].MassCommunication
(
    Title,
    MassCommunicationStatusId,
    StatusMessage,
    Catalyst,
    DeliveryMethodId,
    Subject,
    Body,
    [From],
    CreatedOnUtc
)
VALUES
(   'Test mass comm',           -- Title - varchar(100)
    0,            -- MassCommunicationStatusId - int
    NULL,           -- StatusMessage - varchar(1000)
    'SSMS',           -- Catalyst - varchar(1000)
    0,            -- DeliveryMethodId - int
    'Testing a mass comm out',           -- Subject - varchar(78)
    N'This is a test mass comm',          -- Body - nvarchar(max)
    'eli@massComm.com',           -- From - varchar(1000)
    GETUTCDATE() -- CreatedOnUtc - datetime2(0)
 )

 SELECT * FROM dbo.MassCommunication

 SELECT * FROM dbo.TransmissionStatus

 DECLARE @i INT = 0;
 DECLARE @count INT = 5;
 
 WHILE @i < @count 
 BEGIN
	 INSERT INTO dbo.Recipient
	 (
		 MassCommunicationId,
		 TransmissionStatusId,
		 TransmissionStatusMessage,
		 ContactString,
		 CreatedOnUtc
	 )
	 VALUES
	 (   1,            -- MassCommunicationId - int
		 0,            -- TransmissionStatusId - int
		 NULL,           -- TransmissionStatusMessage - varchar(1000)
		 ('Recipient' + CAST(@i AS VARCHAR(10)) + '@email.com'),           -- ContactString - varchar(1000)
		 GETUTCDATE() -- CreatedOnUtc - datetime2(0)
	 )
 
 	SET @i = @i + 1;
 END
 
 SELECT * FROM dbo.Recipient

SELECT mc.MassCommunicationId
      ,mc.Body
      ,mc.Catalyst
      ,mc.[From]
      ,mc.MassCommunicationStatusId
      ,mcs.[Name] AS McStatus
      ,mc.StatusMessage
      ,mc.Subject
      ,mc.Title
      ,rec.RecipientId
      ,rec.ContactString
      ,rec.TransmissionStatusMessage
      ,rec.TransmissionStatusId
	  ,ts.[Name] AS TransStatus
FROM dbo.MassCommunication mc
    INNER JOIN dbo.Recipient rec
        ON rec.MassCommunicationId = mc.MassCommunicationId
    LEFT JOIN dbo.MassCommunicationStatus mcs
        ON mcs.MassCommunicationStatusId = mc.MassCommunicationStatusId
    LEFT JOIN dbo.TransmissionStatus ts
        ON ts.TransmissionStatusId = rec.TransmissionStatusId;
