CREATE TABLE [dbo].[Technology] (
    [Id]                    UNIQUEIDENTIFIER NOT NULL,
    [TechnologyName]        NVARCHAR (50)    NOT NULL,
    [TechnologyDescription] NVARCHAR (200)   NOT NULL,
    [TechnologyActive]      BIT              NOT NULL,
    [CreatedDate]           DATETIME         NOT NULL,
    [UpdatedDate]           DATETIME         NULL,
    [UpdatedUser]           UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_Technology] PRIMARY KEY CLUSTERED ([Id] ASC)
);

