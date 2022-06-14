CREATE TABLE [dbo].[Company]
(
	[Id] INT NOT NULL , 
    [Name] VARCHAR(50) NULL, 
    [carName] NCHAR(10) NOT NULL, 
    [journeyID] INT NOT NULL, 
    PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Company_ToTable] FOREIGN KEY (carName) REFERENCES [Car]([carName])
)
