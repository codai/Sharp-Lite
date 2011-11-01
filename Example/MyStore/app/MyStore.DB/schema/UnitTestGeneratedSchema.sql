
    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKDDD7BED2D9812CFF]') AND parent_object_id = OBJECT_ID('Products_ProductCategories'))
alter table Products_ProductCategories  drop constraint FKDDD7BED2D9812CFF


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKDDD7BED28F30214F]') AND parent_object_id = OBJECT_ID('Products_ProductCategories'))
alter table Products_ProductCategories  drop constraint FKDDD7BED28F30214F


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK570785C1427FAD8B]') AND parent_object_id = OBJECT_ID('Orders'))
alter table Orders  drop constraint FK570785C1427FAD8B


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKD1A96374B3D390D7]') AND parent_object_id = OBJECT_ID('OrderLineItems'))
alter table OrderLineItems  drop constraint FKD1A96374B3D390D7


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKD1A963748F30214F]') AND parent_object_id = OBJECT_ID('OrderLineItems'))
alter table OrderLineItems  drop constraint FKD1A963748F30214F


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK248A035DD9812CFF]') AND parent_object_id = OBJECT_ID('ProductCategories'))
alter table ProductCategories  drop constraint FK248A035DD9812CFF


    if exists (select * from dbo.sysobjects where id = object_id(N'Products') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Products

    if exists (select * from dbo.sysobjects where id = object_id(N'Products_ProductCategories') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Products_ProductCategories

    if exists (select * from dbo.sysobjects where id = object_id(N'Customers') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Customers

    if exists (select * from dbo.sysobjects where id = object_id(N'Orders') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Orders

    if exists (select * from dbo.sysobjects where id = object_id(N'OrderLineItems') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table OrderLineItems

    if exists (select * from dbo.sysobjects where id = object_id(N'ProductCategories') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table ProductCategories

    create table Products (
        Id INT IDENTITY NOT NULL,
       Name NVARCHAR(255) null,
       Amount DECIMAL(19,5) null,
       primary key (Id)
    )

    create table Products_ProductCategories (
        ProductFk INT not null,
       ProductCategoryFk INT not null
    )

    create table Customers (
        Id INT IDENTITY NOT NULL,
       FirstName NVARCHAR(255) null,
       LastName NVARCHAR(255) null,
       StreetAddress NVARCHAR(255) null,
       ZipCode NVARCHAR(255) null,
       primary key (Id)
    )

    create table Orders (
        Id INT IDENTITY NOT NULL,
       PlacedOn DATETIME null,
       OrderStatusTypeFk INT null,
       CustomerFk INT null,
       primary key (Id)
    )

    create table OrderLineItems (
        Id INT IDENTITY NOT NULL,
       OrderFk INT null,
       Amount DECIMAL(19,5) null,
       ProductFk INT null,
       Quantity INT null,
       primary key (Id)
    )

    create table ProductCategories (
        Id INT IDENTITY NOT NULL,
       Name NVARCHAR(255) null,
       ProductCategoryFk INT null,
       primary key (Id)
    )

    alter table Products_ProductCategories 
        add constraint FKDDD7BED2D9812CFF 
        foreign key (ProductCategoryFk) 
        references ProductCategories

    alter table Products_ProductCategories 
        add constraint FKDDD7BED28F30214F 
        foreign key (ProductFk) 
        references Products

    alter table Orders 
        add constraint FK570785C1427FAD8B 
        foreign key (CustomerFk) 
        references Customers

    alter table OrderLineItems 
        add constraint FKD1A96374B3D390D7 
        foreign key (OrderFk) 
        references Orders

    alter table OrderLineItems 
        add constraint FKD1A963748F30214F 
        foreign key (ProductFk) 
        references Products

    alter table ProductCategories 
        add constraint FK248A035DD9812CFF 
        foreign key (ProductCategoryFk) 
        references ProductCategories
