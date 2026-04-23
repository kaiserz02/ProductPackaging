CREATE DATABASE ProductPackagingDB;
GO

USE ProductPackagingDB;
GO

CREATE TABLE Products (
    ProductId INT IDENTITY(1,1) PRIMARY KEY,
    ProductName NVARCHAR(255) NOT NULL
);

CREATE TABLE PackagingTypes (
    PackageTypeId INT IDENTITY(1,1) PRIMARY KEY,
    PackageTypeName NVARCHAR(100) NOT NULL
);

CREATE TABLE Packaging (
    PackageId INT IDENTITY(1,1) PRIMARY KEY,
    ProductId INT NOT NULL,
    PackageTypeId INT NOT NULL,
    ParentPackageId INT NULL,

    CONSTRAINT FK_Packaging_Product
        FOREIGN KEY (ProductId) REFERENCES Products(ProductId),

    CONSTRAINT FK_Packaging_Type
        FOREIGN KEY (PackageTypeId) REFERENCES PackagingTypes(PackageTypeId),

    CONSTRAINT FK_Packaging_Parent
        FOREIGN KEY (ParentPackageId) REFERENCES Packaging(PackageId)
);

CREATE TABLE Items (
    ItemId INT IDENTITY(1,1) PRIMARY KEY,
    ItemName NVARCHAR(255) NOT NULL
);

CREATE TABLE PackagingItems (
    PackageId INT NOT NULL,
    ItemId INT NOT NULL,

    CONSTRAINT PK_PackagingItems PRIMARY KEY (PackageId, ItemId),

    CONSTRAINT FK_PI_Package
        FOREIGN KEY (PackageId) REFERENCES Packaging(PackageId),

    CONSTRAINT FK_PI_Item
        FOREIGN KEY (ItemId) REFERENCES Items(ItemId)
);

CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(MAX) NOT NULL
);