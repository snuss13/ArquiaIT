CREATE TABLE [dbo].[DebitNote] (
    [Id]             INT           IDENTITY (1, 1) NOT NULL,
    [CategoryID]     INT           NOT NULL,
	[InvoiceID]     INT           NOT NULL,
    [StatusID]       INT           NOT NULL,
    [NoteDate]    DATE          NOT NULL,
    [NoteNumber]  NVARCHAR (50) NOT NULL,
    [ValueInPesos]   MONEY         NOT NULL,
    [ValueInDollars] MONEY         NULL,
    [IVA]            MONEY         NOT NULL,
    [ChangeRate]     MONEY         NULL,
    [InvoiceTotal]   MONEY         NULL,
    [PayDate]        DATE          NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DebitNote_ToInvoiceLine] FOREIGN KEY ([InvoiceID]) REFERENCES [dbo].[Invoice] ([Id]),
    CONSTRAINT [FK_DebitNote_ToCategory] FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[InvoiceCategory] ([Id]),
    CONSTRAINT [FK_DebitNote_ToStatus] FOREIGN KEY ([StatusID]) REFERENCES [dbo].[InvoiceStatus] ([Id])
);

