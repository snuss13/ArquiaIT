CREATE TABLE [dbo].[SystemConfiguration] (
    [Id]             INT   IDENTITY (1, 1) NOT NULL,
    [LastChangeRate] MONEY NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

