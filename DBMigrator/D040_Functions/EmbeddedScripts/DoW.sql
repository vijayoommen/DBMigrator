if exists( select * from sys.objects o inner join sys.schemas s on s.schema_id = o.schema_id where o.name = 'DoW' and o.type = 'FN' and s.name = 'dbo')
	drop function dbo.DoW
go

create function dbo.DoW(@date datetime) 
returns int
WITH execute as CALLER
AS
Begin
	
	return DATEPART(dw, getdate())
END
GO