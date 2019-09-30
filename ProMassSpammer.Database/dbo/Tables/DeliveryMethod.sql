CREATE TABLE [dbo].[DeliveryMethod]
(
[DeliveryMethodId] [int] NOT NULL,
[Name] [varchar] (100) NOT NULL,
[Description] [varchar] (500) NOT NULL,
[IsActive] [bit] NOT NULL CONSTRAINT [DF_dbo.DeliveryMethod_IsActive] DEFAULT ((1)),
[CreatedOnUtc] [datetime2] (0) NOT NULL CONSTRAINT [DF_dbo.DeliveryMethod_CreatedOnUtc] DEFAULT (getutcdate())
)
GO
ALTER TABLE [dbo].[DeliveryMethod] ADD CONSTRAINT [PK_dbo.DeliveryMethod_DeliveryMethodId] PRIMARY KEY CLUSTERED  ([DeliveryMethodId])
GO
;

