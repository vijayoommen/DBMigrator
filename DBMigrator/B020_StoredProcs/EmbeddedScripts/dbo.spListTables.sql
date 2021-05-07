
if exists(select * from sys.objects where name = 'spListTables')
	drop proc dbo.spListTables
go

create proc dbo.spListTables as
begin
	select * from sys.tables order by name 
end
go