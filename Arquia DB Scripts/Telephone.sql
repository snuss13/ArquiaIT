CREATE TABLE [dbo].[Telephone] (
    [Id]       INT          IDENTITY (1, 1) NOT NULL,
    [ClientID] INT          NOT NULL,
    [Number]   VARCHAR (50) NULL,
    [TypeID]   INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Telephone_ToClient] FOREIGN KEY ([ClientID]) REFERENCES [dbo].[Client] ([Id]),
    CONSTRAINT [FK_Telephone_ToTelType] FOREIGN KEY ([TypeID]) REFERENCES [dbo].[TelephoneType] ([Id])
);

