CREATE TABLE [dbo].[Notification_Types] (
    [Notification_Type_ID]     INT           NOT NULL IDENTITY,
    [Notification_Description] NVARCHAR (50) NOT NULL,
    [isActive]                 BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Notification_Type_ID] ASC)
);

