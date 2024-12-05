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

CREATE TABLE [Cuisine] (
    [Id] UniqueIdentifier NOT NULL DEFAULT (newsequentialId()),
    [Type] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Cuisine] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Food] (
    [Id] UniqueIdentifier NOT NULL DEFAULT (newsequentialId()),
    [CuisineId] UniqueIdentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Food] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241103061923_InitializeInitialDatabase', N'8.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Food] ADD [ImageData] VARBINARY(MAX) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241104063302_AddedImageParameterOnFood', N'8.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Admin] (
    [Id] UniqueIdentifier NOT NULL DEFAULT (newsequentialId()),
    [Username] nvarchar(max) NOT NULL,
    [PasswordHash] varbinary(MAX) NOT NULL,
    [PasswordSalt] varbinary(MAX) NOT NULL,
    CONSTRAINT [PK_Admin] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241106131628_AddAdmin', N'8.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Dessert] (
    [Id] UniqueIdentifier NOT NULL DEFAULT (newsequentialId()),
    [CuisineId] UniqueIdentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [ImageData] VARBINARY(MAX) NULL,
    CONSTRAINT [PK_Dessert] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241128124158_AddDessertModel', N'8.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DROP TABLE [Food];
GO

CREATE TABLE [MainCourse] (
    [Id] UniqueIdentifier NOT NULL DEFAULT (newsequentialId()),
    [CuisineId] UniqueIdentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [ImageData] VARBINARY(MAX) NULL,
    CONSTRAINT [PK_MainCourse] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241205114424_RenameFoodToMainCourse', N'8.0.10');
GO

COMMIT;
GO

