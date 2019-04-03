-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_IVM_MaterialReceive_3997]
	-- Add the parameters for the stored procedure here
		@StartDate as date,
		@EndDate as date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select DI.ID 
,DI.ReceiveDate 
,DI.DocID as Weighticket
,MT.Name as 'Material'
,DQTY.Amount as 'bale'
,DPLADT.Amount as 'ADT'
,DPLWgt.Amount as 'ASIS'
,DIQCMP.Value as 'MP AVG'
,DIQCOT.Value as 'OT AVG'
,DIQCPM.Value as 'PM AVG'
,DITR.ID as 'TRUCK No.'
,MTST.ReferenceValue as 'ลาน'
 from DT_Inbound DI 
 --Quatity
	left Join View_DTInbound_LatestVersionID V on V.ID = DI.ID 
	left join DT_InboundQuantity DPLWgt on DI.id= DPLWgt.DT_InboundID
	and DPLWgt.QuantityTypeID=1
	Left Join DT_InboundQuantity DPLADT ON DPLADT.DT_InboundID=DI.ID 
	and DPLADT.QuantityTypeID=3
	Left Join DT_InboundQuantity DQTY ON DQTY.DT_InboundID=DI.ID and DQTY.QuantityTypeID=4
	--QC
	left join DT_InboundQC DIQCMP on DI.ID=DIQCMP.DT_InboundID 
	and DIQCMP.QCID=1
	left join DT_InboundQC DIQCOT on DI.ID=DIQCOT.DT_InboundID 
	and DIQCOT.QCID=2
	left join DT_InboundQC DIQCPM on DI.ID=DIQCPM.DT_InboundID 
	and DIQCPM.QCID=3
	--Truck
	left join  DT_InboundTruck DITR on DI.ID=DITR.DT_InboundID
	--Station
	left join DT_InboundDocument MTST on DI.ID=MTST.DT_InboundID
	and mtst.DocID=145
	left join  MT_Material MT on DI.MaterialID=MT.ID

--where DI.DocID='1809-260054-11'
where DI.ReceiveDate between @StartDate and @EndDate
and di.DocStatusID in (1,4,7)
and mt.TypeID in (1,3)



END
GO
