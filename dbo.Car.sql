CREATE TABLE [dbo].[Car] (
    [CarID]        INT           IDENTITY (1, 1) NOT NULL,
    [carName]   NVARCHAR (50) NOT NULL,
    [startDate] SMALLDATETIME NULL,
    [endDate]   SMALLDATETIME NULL,
    [day]       INT           NULL,
    CONSTRAINT [PK_Car] PRIMARY KEY CLUSTERED ([carName] ASC, [CarID] ASC)
);

