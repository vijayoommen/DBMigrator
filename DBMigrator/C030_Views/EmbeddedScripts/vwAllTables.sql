if exists(select * from sys.views where name = 'vwAllTables')
	drop view dbo.vwAllTables
go

create view vwAllTables as
	select * from sys.tables t
go