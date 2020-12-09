CREATE TABLE [Application].[Result] (
    [Id]                INT           IDENTITY (1, 1) NOT NULL,
    [Active]            BIT           CONSTRAINT [DF_Result_Active] DEFAULT ((1)) NOT NULL,
    [DateCreated]       DATETIME2 (7) CONSTRAINT [DF_Result_DateCreated] DEFAULT (getdate()) NOT NULL,
    [FirstName]         VARCHAR (255) NOT NULL,
    [LastName]          VARCHAR (255) NOT NULL,
    [RaceTime]          TIME(3)       NOT NULL,
    [StatusId]          INT           CONSTRAINT [DF_Result_StatusId] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Resutlt] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [Application].[Result.DCStatus] ([Id]),
);

GO
CREATE NONCLUSTERED INDEX [IX_U_RaceTime]
    ON [Application].[Result]([RaceTime] ASC);
