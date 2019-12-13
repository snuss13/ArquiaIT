CREATE TABLE [dbo].[Retention] (
    [Id]         INT   IDENTITY (1, 1) NOT NULL,
    [InvoiceID]  INT   NOT NULL,
    [Ganancias]  MONEY NOT NULL,
    [IVA]        MONEY NOT NULL,
    [SUSS]       MONEY NOT NULL,
    [IIBB]       MONEY NOT NULL,
    [IIBBSpends] MONEY NOT NULL,
    [Others]     MONEY NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Retention_ToInvoice] FOREIGN KEY ([InvoiceID]) REFERENCES [dbo].[Invoice] ([Id])
);

