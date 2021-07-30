CREATE TABLE [dbo].[Client] (
    [Id]                 UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [ClientName]         NVARCHAR (50)    NOT NULL,
    [ClientDivisionName] NVARCHAR (50)    NOT NULL,
    [CreatedDate]        DATETIME         NULL,
    [LastUpdated]        DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([ClientDivisionName] ASC),
    UNIQUE NONCLUSTERED ([ClientName] ASC)
);

