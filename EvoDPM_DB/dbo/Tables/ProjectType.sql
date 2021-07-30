CREATE TABLE [dbo].[ProjectType] (
    [ID]              UNIQUEIDENTIFIER NOT NULL,
    [ProjectTypeName] NVARCHAR (100)   NULL,
    [RecordStatus]    BIT         NULL,
    CONSTRAINT [PK_ProjectType] PRIMARY KEY CLUSTERED ([ID] ASC)
);

