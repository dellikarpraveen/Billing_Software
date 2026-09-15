IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [InventoryItems] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Unit] nvarchar(max) NOT NULL,
    [OnHand] decimal(18,2) NOT NULL,
    [ReorderLevel] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_InventoryItems] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [MenuItems] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [CategoryId] int NULL,
    [PriceCents] int NOT NULL,
    [PrepTimeSec] int NULL,
    [Active] bit NOT NULL,
    [PhotoUrl] nvarchar(max) NULL,
    CONSTRAINT [PK_MenuItems] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Orders] (
    [Id] int NOT NULL IDENTITY,
    [KitchenId] int NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    [TotalCents] int NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [OrderItems] (
    [Id] int NOT NULL IDENTITY,
    [OrderId] int NOT NULL,
    [MenuItemId] int NOT NULL,
    [Quantity] int NOT NULL,
    [ModifiersJson] nvarchar(max) NULL,
    [PriceCents] int NOT NULL,
    CONSTRAINT [PK_OrderItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OrderItems_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_OrderItems_OrderId] ON [OrderItems] ([OrderId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260915152959_InitialCreate', N'8.0.0');
GO

COMMIT;
GO

