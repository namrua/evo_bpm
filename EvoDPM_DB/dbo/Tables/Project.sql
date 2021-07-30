CREATE TABLE [dbo].[Project] (
    [Id]                    UNIQUEIDENTIFIER NOT NULL,
    [ClientId]              UNIQUEIDENTIFIER NULL,
    [ProjectName]           NVARCHAR (50)    NULL,
    [StartDate]             DATETIME         NULL,
    [LastUpdated]           DATETIME         NULL,
    [BusinessDomainDisplay] NVARCHAR (50)    NULL,
    [ProjectCode]           NVARCHAR (20)    NULL,
    [ProjectManagerId]      UNIQUEIDENTIFIER NULL,
    [ProjectTypeId]         UNIQUEIDENTIFIER NULL,
    [ServiceTypeId]         UNIQUEIDENTIFIER NULL,
    [BusinessDomainId]      UNIQUEIDENTIFIER NULL,
    [MethodologyId]         UNIQUEIDENTIFIER NULL,
    [TechnologyId]          UNIQUEIDENTIFIER NULL,
    [DeliveryODC]           NVARCHAR (50)    NULL,
    [DeliveryLocation]      NVARCHAR (50)    NULL,
    [StatusId]              UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED ([Id] ASC)
);

