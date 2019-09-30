CREATE TABLE [dbo].[TransmissionStatus]
(
[TransmissionStatusId] [int] NOT NULL,
[Name] [nvarchar] (50) NOT NULL,
[Description] [nvarchar] (255) NOT NULL,
[CreatedOnUtc] [datetime2] (0) NOT NULL CONSTRAINT [DF_dbo.TransmissionStatus_CreatedOnUtc] DEFAULT (getutcdate())
)
GO
ALTER TABLE [dbo].[TransmissionStatus] ADD CONSTRAINT [PK_dbo.TransmissionStatus_TransmissionStatusId] PRIMARY KEY CLUSTERED  ([TransmissionStatusId])
GO
;

