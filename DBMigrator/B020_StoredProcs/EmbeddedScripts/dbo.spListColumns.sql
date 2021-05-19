
create proc dbo.spListColumns as
begin
	select 1, * from sys.columns order by name 
end
go