

create proc dbo.spListTables as
begin
	-- add a new comment line here
	select * from sys.tables order by name 
end
go