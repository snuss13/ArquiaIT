CREATE TABLE [dbo].[PurchaseOrderLine] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [POID]           INT            NOT NULL,
    [Description]    NVARCHAR (500) NOT NULL,
    [ValueInDollars] MONEY          NULL,
    [Value]          MONEY          NULL,
    [ValueInPesos]   MONEY          NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PO_ToPOLine] FOREIGN KEY ([POID]) REFERENCES [dbo].[PurchaseOrder] ([Id])
);

