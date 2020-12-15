USE [dbo.PEX]

DECLARE @path VARCHAR(4000) = 'C:\r\PEX\'
DECLARE @filelocation VARCHAR(100)
DECLARE @sql varchar(max)

DELETE FROM [dbo].[Vendor]

SET @filelocation = @path + 'Vendors.csv';

SET @sql = '
 BULK INSERT [dbo].[Vendor]
 FROM ''' + @filelocation + '''
 WITH(
  FIRSTROW = 2,
  FIELDTERMINATOR = '','',
  ROWTERMINATOR = ''\n'',
  TABLOCK
 )'

EXEC(@sql)