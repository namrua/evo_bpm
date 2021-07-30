CREATE TABLE [dbo].[ServiceType] (
    [ID]              UNIQUEIDENTIFIER NOT NULL,
    [ServiceTypeName] NVARCHAR (100)   NULL,
    [RecordStatus]    BIT         NULL,
    CONSTRAINT [PK_ServiceType] PRIMARY KEY CLUSTERED ([ID] ASC)
);

