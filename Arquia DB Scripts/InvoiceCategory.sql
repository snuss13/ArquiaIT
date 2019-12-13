CREATE TABLE [dbo].[InvoiceCategory] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (100) NOT NULL,
    [Active]      BIT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

