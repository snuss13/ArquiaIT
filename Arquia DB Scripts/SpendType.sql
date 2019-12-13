CREATE TABLE [dbo].[SpendType] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Description] VARCHAR (200) NULL,
    [Active]    BIT           DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

