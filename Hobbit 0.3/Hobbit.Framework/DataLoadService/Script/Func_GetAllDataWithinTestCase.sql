USE [Automation]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Gnail].[GetAllDataWithinTestCase]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [Gnail].[GetAllDataWithinTestCase]
GO

USE [Automation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [Gnail].[GetAllDataWithinTestCase] 
(	
	-- Add the parameters for the function here
	@ProjectId int, 
	@TestCaseId int
)
RETURNS @retTable TABLE 
(
	Name varchar(100) NOT NULL,
	Value varchar(100) NOT NULL,
	IsGlobal bit NOT NULL
)
AS
BEGIN
	INSERT INTO @retTable(Name,Value,IsGlobal)
	select Name,Value,'1' from  QA_Automation_Test.GlobalSetting where ProjectId=@ProjectId
	INSERT INTO @retTable(Name,Value,IsGlobal)
	select Name,Value,'0' from  QA_Automation_Test.TestData where ProjectId=@ProjectId and TestCaseId=CAST(@TestCaseId as varchar(10))
	RETURN
END


GO


