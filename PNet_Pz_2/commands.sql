EXEC sp_configure 'clr enabled', 1
RECONFIGURE
GO

EXEC sp_configure 'show advanced options', 1
RECONFIGURE
GO

EXEC sp_configure 'clr strict security', 0;
RECONFIGURE
GO

DROP PROCEDURE IF EXISTS GetDebitorClr;
GO

DROP ASSEMBLY IF EXISTS CLRFunctions;
GO

CREATE ASSEMBLY CLRFunctions FROM '..\PNet_Pz_2\bin\Debug\PNet_Pz_2.dll'
GO

CREATE PROCEDURE GetDebitorClr
@Name nvarchar(50)
AS
    EXTERNAL NAME CLRFunctions.CustomProcedure.GetDebitorByName;
GO

USE [Bank]
EXEC GetDebitorClr @Name=Dmytro