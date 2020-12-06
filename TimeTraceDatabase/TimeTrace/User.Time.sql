CREATE TABLE [Application].[User.Time] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Active]      BIT           CONSTRAINT [DF_User.Time_Active] DEFAULT ((1)) NOT NULL,
    [DateCreated] DATETIME2 (7) CONSTRAINT [DF_User.Time_DateCreated] DEFAULT (getdate()) NOT NULL,
    [FirstName]   VARCHAR (255) NOT NULL,
    [LastName]    VARCHAR (255) NOT NULL,
    [RaceTime]    TIME(3)       NOT NULL,
    [IsApproved]  BIT           CONSTRAINT [DF_User.Time_IsApproved] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_User.Time] PRIMARY KEY CLUSTERED ([Id] ASC),
);
