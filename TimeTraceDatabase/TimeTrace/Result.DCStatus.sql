CREATE TABLE [Application].[Result.DCStatus] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Active]      BIT           CONSTRAINT [DF_Result.DCStatus_Active] DEFAULT ((1)) NOT NULL,
    [DateCreated] DATETIME2 (7) CONSTRAINT [DF_Result.DCStatus_DateCreated] DEFAULT (getdate()) NOT NULL,
    [Name]        VARCHAR (255) NOT NULL,
    [Description] VARCHAR (255) NOT NULL,
    CONSTRAINT [PK_Result.DCStatus] PRIMARY KEY CLUSTERED ([Id] ASC),
);
