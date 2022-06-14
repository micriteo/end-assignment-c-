CREATE TABLE [dbo].[Journey] (
    [JourneyID]       INT           NOT NULL,
    [name]     VARCHAR (50)  NOT NULL,
    [carName]  NVARCHAR (50) NULL,
    [distance] INT           NULL,
    [day]      INT           NULL,
    CONSTRAINT [PK_Journey] PRIMARY KEY CLUSTERED ([JourneyID] ASC)
);

