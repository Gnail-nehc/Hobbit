USE [QA_Automation_Test_POC]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[QA_Automation_Test].[GetAllControlsInProject]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [QA_Automation_Test].[GetAllControlsInProject]
GO

USE [QA_Automation_Test_POC]
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
CREATE FUNCTION [QA_Automation_Test].[GetAllControlsInProject]
(	
	-- Add the parameters for the function here
	@ProjectId int 
)
RETURNS TABLE 
AS
RETURN 
(
	select ControlProperty.Type as UIType,
	ControlProperty.Property as UIProperty,
	Control.PropertyValue as PropValue,
	Control.Name as UIName
	from Control,ControlProperty
	where Control.ProjectId = @ProjectId and Control.PropertyId=ControlProperty.Id
)




GO


