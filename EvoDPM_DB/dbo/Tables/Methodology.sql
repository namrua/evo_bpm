CREATE TABLE [dbo].[Methodology] (
    [ID]              UNIQUEIDENTIFIER NOT NULL,
    [MethodologyName] NVARCHAR (250)   NULL,
    [RecordStatus]    BIT         NULL,
    CONSTRAINT [PK_Methodology] PRIMARY KEY CLUSTERED ([ID] ASC)
);

