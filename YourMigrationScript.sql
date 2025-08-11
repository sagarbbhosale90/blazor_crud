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

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250806040854_init_client_2'
)
BEGIN
    CREATE TABLE [Products] (
        [ProductId] int NOT NULL IDENTITY,
        [ProductName] nvarchar(max) NOT NULL,
        [Price] decimal(18,4) NULL,
        [Quantity] float NULL,
        CONSTRAINT [PK_Products] PRIMARY KEY ([ProductId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250806040854_init_client_2'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250806040854_init_client_2', N'8.0.19');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250806043751_init_client_3'
)
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'ProductName');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Products] ALTER COLUMN [ProductName] nvarchar(450) NOT NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250806043751_init_client_3'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Products_ProductName] ON [Products] ([ProductName]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250806043751_init_client_3'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250806043751_init_client_3', N'8.0.19');
END;
GO

COMMIT;
GO

