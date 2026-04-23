USE ProductPackagingDB;
GO

-- Product
INSERT INTO Products (ProductName)
VALUES ('Self Adjusting Table');

-- Packaging Types
INSERT INTO PackagingTypes (PackageTypeName)
VALUES ('Box'), ('Packet');

-- Root Packaging
INSERT INTO Packaging (ProductId, PackageTypeId, ParentPackageId)
VALUES (1, 1, NULL); -- PackageID = 1

-- Child Packaging
INSERT INTO Packaging (ProductId, PackageTypeId, ParentPackageId)
VALUES 
(1, 1, 1), -- PackageID = 2
(1, 1, 1), -- PackageID = 3
(1, 2, 1); -- PackageID = 4

-- Nested Packaging
INSERT INTO Packaging (ProductId, PackageTypeId, ParentPackageId)
VALUES (1, 2, 4); -- PackageID = 5

-- Items
INSERT INTO Items (ItemName)
VALUES 
('Table Top'),
('Table Legs'),
('Screwdriver'),
('Screws');

-- Assign Items to Packaging
INSERT INTO PackagingItems (PackageId, ItemId)
VALUES 
(2, 1), -- Table Top
(3, 2), -- Table Legs
(4, 3), -- Screwdriver
(5, 4); -- Screws