CREATE TABLE [dbo].[ProblemCategory] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Name]        NVARCHAR (50)    NOT NULL,
    [Description] NVARCHAR (50)    NOT NULL,
    [CreatedDate] DATETIME         NULL,
    [UpdatedDate] DATETIME         NULL,
    [IsActived]   BIT              NULL,
    [CreatedBy]   UNIQUEIDENTIFIER NULL,
    [UpdatedBy]   UNIQUEIDENTIFIER NULL,
    [DeleteFlag]  BIT              NULL,
    CONSTRAINT [PK__ProblemC__3214EC07E6028D65] PRIMARY KEY CLUSTERED ([Id] ASC)
);

