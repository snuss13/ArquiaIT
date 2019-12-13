CREATE TABLE [dbo].[Spend] (
    [Id]                    INT            IDENTITY (1, 1) NOT NULL,
    [InvoiceID]             INT            NULL,
    [Vendor]                NVARCHAR (200) NOT NULL,
    [Description]           NVARCHAR (MAX) NULL,
    [InvoiceNumber]         NVARCHAR (50)  NOT NULL,
    [InvoiceDate]           DATE           NOT NULL,
	[SpendTypeID]			INT			   NOT NULL,
    [POValueInDollars]      MONEY          NULL,
    [InvoiveValueInDollars] MONEY          NULL,
    [InvoiceValue]          MONEY          NOT NULL,
    [IVA]                   MONEY          NOT NULL,
    [IIBB_ARBA]             MONEY          NULL,
    [ARCIBA]                MONEY          NULL,
    [InvoiceTotal]          MONEY          NOT NULL,
    [PayDate]               DATE           NULL,
    [OtherTaxes]            MONEY          NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_ISpends_ToInvoice] FOREIGN KEY ([InvoiceID]) REFERENCES [dbo].[Invoice] ([Id]),
    CONSTRAINT [FK_ISpends_ToSpendType] FOREIGN KEY ([SpendTypeID]) REFERENCES [dbo].[SpendType] ([Id])
);

