CREATE TABLE [dbo].[Status] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Name]        NVARCHAR (50)    NOT NULL,
    [UpdatedUser] UNIQUEIDENTIFIER NULL,
    [UpdatedDate] DATETIME         NULL,
    [CreatedDate] DATETIME         NOT NULL,
    CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED ([Id] ASC)
);

