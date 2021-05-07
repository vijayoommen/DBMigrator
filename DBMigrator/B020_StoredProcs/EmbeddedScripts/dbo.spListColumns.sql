if exists(select * from sys.objects where name = 'spListColumns')
	drop proc dbo.spListColumns
go

create proc dbo.spListColumns as
begin
	select 1, * from sys.columns order by name 
end
go