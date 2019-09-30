CREATE TABLE [dbo].[MassCommunication]
(
[MassCommunicationId] [int] NOT NULL IDENTITY(1, 1),
[Title] [varchar] (100) NOT NULL,
[MassCommunicationStatusId] [int] NOT NULL,
[StatusMessage] [varchar] (1000) NULL,
[Catalyst] [varchar] (1000) NOT NULL,
[DeliveryMethodId] [int] NOT NULL,
[Subject] [varchar] (78) NOT NULL,
[Body] [nvarchar] (max) NOT NULL,
[From] [varchar] (1000) NOT NULL,
[CreatedOnUtc] [datetime2] (0) NOT NULL CONSTRAINT [DF_dbo.MassCommunication_CreatedOnUtc] DEFAULT (getutcdate()),
[ModifiedOnUtc] [datetime2] (0) NULL
)
GO
ALTER TABLE [dbo].[MassCommunication] ADD CONSTRAINT [PK_dbo.MassCommunication_MassCommunicationId] PRIMARY KEY CLUSTERED  ([MassCommunicationId])
GO
CREATE NONCLUSTERED INDEX [IX_dbo.MassCommunication_DeliveryMethodId] ON [dbo].[MassCommunication] ([DeliveryMethodId])
GO
CREATE NONCLUSTERED INDEX [IX_dbo.MassCommunication_MassCommunicationStatusId] ON [dbo].[MassCommunication] ([MassCommunicationStatusId])
GO
ALTER TABLE [dbo].[MassCommunication] ADD CONSTRAINT [FK_dbo.MassCommunication_dbo.DeliveryMethod_DeliveryMethodId] FOREIGN KEY ([DeliveryMethodId]) REFERENCES [dbo].[DeliveryMethod] ([DeliveryMethodId])
GO
ALTER TABLE [dbo].[MassCommunication] ADD CONSTRAINT [FK_dbo.MassCommunication_dbo.MassCommunicationStatus_MassCommunicationStatusId] FOREIGN KEY ([MassCommunicationStatusId]) REFERENCES [dbo].[MassCommunicationStatus] ([MassCommunicationStatusId])
GO
;

