SELECT
	 r.RecipientId
	,ts.TransmissionStatusId
    ,ts.Name
    ,ts.Description
	,r.CreatedOnUtc
FROM dbo.Recipient r 
	LEFT JOIN dbo.TransmissionStatus ts
		ON ts.TransmissionStatusId = r.TransmissionStatusId
WHERE r.RecipientId = 1

