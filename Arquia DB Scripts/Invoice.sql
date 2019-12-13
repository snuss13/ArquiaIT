CREATE TABLE [dbo].[Invoice] (
    [Id]             INT           IDENTITY (1, 1) NOT NULL,
    [POLineID]       INT           NOT NULL,
    [CategoryID]     INT           NOT NULL,
    [StatusID]       INT           NOT NULL,
    [InvoiceDate]    DATE          NOT NULL,
    [InvoiceNumber]  NVARCHAR (50) NOT NULL,
    [ValueInPesos]   MONEY         NOT NULL,
    [ValueInDollars] MONEY         NULL,
    [IVA]            MONEY         NOT NULL,
    [ChangeRate]     MONEY         NULL,
    [InvoiceTotal]   MONEY         NULL,
    [PayDate]        DATE          NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Invoice_ToPOLine] FOREIGN KEY ([POLineID]) REFERENCES [dbo].[PurchaseOrderLine] ([Id]),
    CONSTRAINT [FK_Invoice_ToCategory] FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[InvoiceCategory] ([Id]),
    CONSTRAINT [FK_Invoice_ToStatus] FOREIGN KEY ([StatusID]) REFERENCES [dbo].[InvoiceStatus] ([Id])
);

