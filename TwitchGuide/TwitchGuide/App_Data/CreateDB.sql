USE [TCGdb]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




/* IDENTITY TABLES */


-- ############# AspNetRoles #############
CREATE TABLE [dbo].[AspNetRoles]
(
    [Id]   NVARCHAR (128) NOT NULL,
    [Name] NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex]
    ON [dbo].[AspNetRoles]([Name] ASC);

-- ############# AspNetUsers #############
CREATE TABLE [dbo].[AspNetUsers]
(
    [Id]                   NVARCHAR (128) NOT NULL,
    [Email]                NVARCHAR (256) NULL,
    [EmailConfirmed]       BIT            NOT NULL,
    [PasswordHash]         NVARCHAR (MAX) NULL,
    [SecurityStamp]        NVARCHAR (MAX) NULL,
    [PhoneNumber]          NVARCHAR (MAX) NULL,
    [PhoneNumberConfirmed] BIT            NOT NULL,
    [TwoFactorEnabled]     BIT            NOT NULL,
    [LockoutEndDateUtc]    DATETIME       NULL,
    [LockoutEnabled]       BIT            NOT NULL,
    [AccessFailedCount]    INT            NOT NULL,
    [UserName]             NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]([UserName] ASC);




-- ############# AspNetUserClaims #############
CREATE TABLE [dbo].[AspNetUserClaims]
(
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [UserId]     NVARCHAR (128) NOT NULL,
    [ClaimType]  NVARCHAR (MAX) NULL,
    [ClaimValue] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]([UserId] ASC);

-- ############# AspNetUserLogins #############
CREATE TABLE [dbo].[AspNetUserLogins]
(
    [LoginProvider] NVARCHAR (128) NOT NULL,
    [ProviderKey]   NVARCHAR (128) NOT NULL,
    [UserId]        NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC, [UserId] ASC),
    CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]([UserId] ASC);

-- ############# AspNetUserRoles #############
CREATE TABLE [dbo].[AspNetUserRoles]
(
    [UserId] NVARCHAR (128) NOT NULL,
    [RoleId] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]([UserId] ASC);
GO
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]([RoleId] ASC);




/* *********** Users ************ */

CREATE TABLE [dbo].[Users] (
    [UserID]    INT            IDENTITY (1, 1) NOT NULL,
    [TwitchID]  INT            NULL,
    [Username]  NVARCHAR (256) NULL,
    [AuthToken] NVARCHAR (40)  NULL,
    [Avatar]    VARCHAR (1024) NULL,
    PRIMARY KEY CLUSTERED ([UserID] ASC)
);

/* *********** timeblocks ************ */


CREATE TABLE [dbo].[Timeblocks] (
    [Index]       INT     IDENTITY (1, 1) NOT NULL,
    [StartHour]   TINYINT NOT NULL,
    [StartMinute] TINYINT NOT NULL,
    [EndHour]     TINYINT NOT NULL,
    [EndMinute]   TINYINT NOT NULL,
    [Day]         TINYINT NOT NULL,
    CONSTRAINT [pk_TimeblockID] PRIMARY KEY CLUSTERED ([StartHour] ASC, [StartMinute] ASC, [EndHour] ASC, [EndMinute] ASC, [Day] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [ux_schedules_Index]
    ON [dbo].[Timeblocks]([Index] ASC);



/* ************Schedules************* */ 

CREATE TABLE [dbo].[Schedules](
	[UserID] [int] NOT NULL,
	[TimeblockID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[TimeblockID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

ALTER TABLE [dbo].[Schedules]  WITH CHECK ADD FOREIGN KEY([TimeblockID])
REFERENCES [dbo].[Timeblocks] ([Index])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Schedules]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
ON DELETE CASCADE
GO



-- ############# Followers #############

CREATE TABLE [dbo].[Followers] (
    [UserID]      INT NOT NULL,
    [FollowingID] INT NOT NULL,
    CONSTRAINT [PK_Followers] PRIMARY KEY CLUSTERED ([UserID] ASC, [FollowingID] ASC),
    CONSTRAINT [FK_Followers_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([UserID]) ON DELETE CASCADE
); 

-- ############# News #############

CREATE TABLE [dbo].[News] (
    [NewsID]    INT            IDENTITY (1, 1) NOT NULL,
    [Title]     NVARCHAR (60)  NOT NULL,
    [Content]   NVARCHAR (MAX) NOT NULL,
    [DateAdded] DATETIME       NOT NULL,
    PRIMARY KEY CLUSTERED ([NewsID] DESC)
);

GO
CREATE UNIQUE NONCLUSTERED INDEX [ux_sitenews_Index]
    ON [dbo].[News]([NewsID] DESC);

-- ############# SiteNews #############

CREATE TABLE [dbo].[SiteNews] (
    [UserID] INT NOT NULL,
    [NewsID] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([UserID] ASC, [NewsID] DESC),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([UserID]),
    FOREIGN KEY ([NewsID]) REFERENCES [dbo].[News] ([NewsID]) ON DELETE CASCADE
);

