
create function dbo.DoW(@date datetime) 
returns int
WITH execute as CALLER
AS
Begin
	return DATEPART(dw, getdate())
END
GO