CREATE TABLE [dbo].[Contact] (
    [Id]              INT           NOT NULL IDENTITY,
    [ClientID]        INT           NOT NULL,
    [Name]            VARCHAR (200) NOT NULL,
    [LastName]        VARCHAR (200) NOT NULL,
    [Area]            VARCHAR (200) NULL,
    [TelephoneNumber] VARCHAR (50)  NULL,
    [InternNumber]    VARCHAR (50)  NULL,
    [email]           VARCHAR (200) NULL,
    [Active]          BIT           DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Contact_ToClient] FOREIGN KEY ([ClientID]) REFERENCES [dbo].[Client] ([Id])
);

