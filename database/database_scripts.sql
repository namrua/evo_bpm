-- 1.Create Database
CREATE DATABASE EvoBPM_DB
GO
USE [EvoBPM_DB]
GO
-- 2.Product 
CREATE TABLE [dbo].[Product](
       [ProductID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
       [ProductKey] [nvarchar](50) NOT NULL,
       [ProductName] [nvarchar](250) NOT NULL,
       [ProductImageUri] [nvarchar](250) NULL,
       [ProductTypeID] [uniqueidentifier] NOT NULL,
       [RecordStatus] [smallint] NOT NULL,
       [CreatedDate] [datetime] NOT NULL,
       [UpdatedDate] [datetime] NULL,
       [UpdatedUser] [uniqueidentifier] NULL,
CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED
(
       [ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
CONSTRAINT [IX_Product] UNIQUE NONCLUSTERED
(
       [ProductKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
 
-- 3.Product Type  
CREATE TABLE [dbo].[ProductType](
       [ProductTypeID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
       [ProductTypeKey] [nvarchar](250) NOT NULL,
       [ProductTypeName] [nvarchar](250) NOT NULL,
       [RecordStatus] [smallint] NOT NULL,
       [CreatedDate] [datetime] NOT NULL,
       [UpdatedDate] [datetime] NULL,
       [UpdatedUser] [uniqueidentifier] NULL,
CONSTRAINT [PK_ProductType] PRIMARY KEY CLUSTERED
(
       [ProductTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
CONSTRAINT [IX_ProductType] UNIQUE NONCLUSTERED
(
       [ProductTypeKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_ProductID]  DEFAULT (newid()) FOR [ProductID]
GO
ALTER TABLE [dbo].[ProductType] ADD  CONSTRAINT [DF_ProductType_ProductTypeID]  DEFAULT (newid()) FOR [ProductTypeID]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_ProductType] FOREIGN KEY([ProductTypeID])
REFERENCES [dbo].[ProductType] ([ProductTypeID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_ProductType]
GO

--Create Project
CREATE TABLE [dbo].[Project](
	[ProjectID] [uniqueidentifier] NOT NULL,
	[ProjectKey] [nvarchar](50) NULL,
	[ImageUrl] [nvarchar](350) NULL,
	[ProjectName] [nvarchar](250) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[RecordStatus] [smallint] NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[ProjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO