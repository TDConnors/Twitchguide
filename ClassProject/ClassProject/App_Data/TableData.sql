DROP TABLE IF EXISTS [dbo].[Names]

CREATE TABLE [dbo].[Names] (
    [NameID] INT           IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (50) DEFAULT ('Unknown') NOT NULL,
    PRIMARY KEY CLUSTERED ([NameID] ASC)
);