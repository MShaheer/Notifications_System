CREATE TABLE [dbo].[Virtual_Player_Notifications] (
    [Virtual_Player_Notification_ID] INT IDENTITY (1, 1) NOT NULL,
    [User_Account_ID]                NVARCHAR(50) NOT NULL,
    [Notification_Type_ID]           INT NOT NULL,
    [isActive]                       BIT NOT NULL,
    PRIMARY KEY CLUSTERED ([Virtual_Player_Notification_ID] ASC),
    CONSTRAINT [FK_Virtual_Player_Notifications_Notification_Types] FOREIGN KEY ([Notification_Type_ID]) REFERENCES [dbo].[Notification_Types] ([Notification_Type_ID])
);

