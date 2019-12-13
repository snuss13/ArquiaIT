CREATE TABLE [dbo].[PurchaseOrder] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [ClientID]   INT           NOT NULL,
    [PONumber]   NVARCHAR (50) NULL,
    [Date]       DATE          NOT NULL,
    [IsDolar]    BIT           NOT NULL,
    [ChangeRate] MONEY         NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PO_ToClient] FOREIGN KEY ([ClientID]) REFERENCES [dbo].[Client] ([Id])
);

