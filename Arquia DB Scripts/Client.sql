CREATE TABLE [dbo].[Client] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (200) NULL,
    [CUIT]    NVARCHAR (15)  NULL,
    [Address] NVARCHAR (500) NULL,
    [Active]  BIT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

