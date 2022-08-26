CREATE TABLE [dbo].[MeterReadingUploads] (
    [MeterReadingUploadsId] INT                IDENTITY (1, 1) NOT NULL,
    [AccountId]             INT                NOT NULL,
    [MeterReadingDateTime]  DATETIMEOFFSET (7) NOT NULL,
    [MeterReadingValue]     INT                NOT NULL,
    CONSTRAINT [PK_MeterReadingUploads] PRIMARY KEY CLUSTERED ([MeterReadingUploadsId] ASC),
    CONSTRAINT [constrain_reading_value] CHECK ([MeterReadingValue]>=(1) AND [MeterReadingValue]<=(99999)),
    CONSTRAINT [FK_MeterReadingUploads_Accounts] FOREIGN KEY ([AccountId]) REFERENCES [dbo].[Accounts] ([AccountId])
);




