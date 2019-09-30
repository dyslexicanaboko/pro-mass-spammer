CREATE TABLE [dbo].[MassCommunicationStatus]
(
[MassCommunicationStatusId] [int] NOT NULL,
[Name] [nvarchar] (50) NOT NULL,
[Description] [nvarchar] (255) NOT NULL,
[CreatedOnUtc] [datetime2] (0) NOT NULL CONSTRAINT [DF_dbo.MassCommunicationStatus_CreatedOnUtc] DEFAULT (getutcdate())
)
GO
ALTER TABLE [dbo].[MassCommunicationStatus] ADD CONSTRAINT [PK_dbo.MassCommunicationStatus_MassCommunicationStatusId] PRIMARY KEY CLUSTERED  ([MassCommunicationStatusId])
GO
;

