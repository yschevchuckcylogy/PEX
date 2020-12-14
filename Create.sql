USE [master]

IF EXISTS(SELECT name FROM sys.databases WHERE name = 'dbo.PEX')
 DROP DATABASE [dbo.PEX]

CREATE DATABASE [dbo.PEX]

USE [dbo.PEX]

CREATE TABLE [dbo].[Transaction](
 [UserID] [nvarchar](40) NOT NULL,
 [VendorID] [nvarchar](40) NOT NULL,
 [TransactionAmount] [bigint] NOT NULL,
 [TransactionDate] [datetime] NOT NULL
)

CREATE TABLE [dbo].[Vendor](
 [VendorID] [nvarchar](40) NOT NULL,
 [VendorName] [nvarchar](40) NOT NULL,
 [MonthlyPerUserCap] [bigint] NOT NULL,
 [MonthlyCap] [bigint] NOT NULL,
 [Enabled] [bit] NOT NULL
)

GO
