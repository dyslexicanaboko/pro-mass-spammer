CREATE TABLE [dbo].[Recipient]
(
[RecipientId] [int] NOT NULL IDENTITY(1, 1),
[MassCommunicationId] [int] NOT NULL,
[TransmissionStatusId] [int] NOT NULL,
[TransmissionStatusMessage] [varchar] (1000) NULL,
[ContactString] [varchar] (1000) NOT NULL,
[CreatedOnUtc] [datetime2] (0) NOT NULL CONSTRAINT [DF_dbo.Recipient_CreatedOnUtc] DEFAULT (getutcdate()),
[ModifiedOnUtc] [datetime2] (0) NULL
)
GO
ALTER TABLE [dbo].[Recipient] ADD CONSTRAINT [PK_dbo.Recipient_RecipientId] PRIMARY KEY CLUSTERED  ([RecipientId])
GO
CREATE NONCLUSTERED INDEX [IX_dbo.Recipient_MassCommunicationId] ON [dbo].[Recipient] ([MassCommunicationId])
GO
CREATE NONCLUSTERED INDEX [IX_dbo.Recipient_TransmissionStatusId] ON [dbo].[Recipient] ([TransmissionStatusId])
GO
ALTER TABLE [dbo].[Recipient] ADD CONSTRAINT [FK_dbo.Recipient_dbo.MassCommunication_MassCommunicationId] FOREIGN KEY ([MassCommunicationId]) REFERENCES [dbo].[MassCommunication] ([MassCommunicationId])
GO
ALTER TABLE [dbo].[Recipient] ADD CONSTRAINT [FK_dbo.Recipient_dbo.TransmissionStatus_TransmissionStatusId] FOREIGN KEY ([TransmissionStatusId]) REFERENCES [dbo].[TransmissionStatus] ([TransmissionStatusId])
GO
;


GO
;

