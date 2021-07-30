CREATE TABLE [dbo].[BusinessDomain] (
    [Id]                 UNIQUEIDENTIFIER NOT NULL,
    [Description]        NVARCHAR (1000)  NULL,
    [CreatedDate]        DATETIME         NOT NULL,
    [UpdatedDate]        DATETIME         NULL,
    [BusinessDomainName] NVARCHAR (250)   NOT NULL,
    [Status]             BIT              NOT NULL,
    CONSTRAINT [PK_BusinessDomain] PRIMARY KEY CLUSTERED ([Id] ASC)
);

