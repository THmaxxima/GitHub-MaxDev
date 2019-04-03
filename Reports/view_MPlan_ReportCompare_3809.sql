USE [MPlanx]
GO

/****** Object:  View [dbo].[view_MPlan_ReportCompare_3809]    Script Date: 25/02/2019 10:08:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[view_MPlan_ReportCompare_3809]
AS
SELECT        di.ID, di.ReceiveDate, di.DocID AS WeightTicket, dt.TruckPlateLicense, ds.ID AS SupplierID, ms.Name AS SupplierName, di.MaterialID, mt.Name AS MaterialName, MC.MScaleWeightIn, d4.Amount AS MPlanWeightIn, 
                         MC.MSCaleWeightOut, d5.Amount AS MPlanWeightOut, MC.MSCaleNetWeight, d6.Amount AS MPlanNetWeight, MC.MSCaleMP, d1.Value AS MPlanMP, MC.MSCaleOT, d2.Value AS MPlanOT, MC.MSCalePM, 
                         d3.Value AS MPlanPM, di.DocStatusID
FROM            dbo.DT_Inbound AS di INNER JOIN
                         dbo.DT_InboundTruck AS dt ON di.ID = dt.DT_InboundID INNER JOIN
                         dbo.DT_InboundSupplier AS ds ON dt.ID = ds.DT_InboundID AND ds.SupplierTypeID = 2 INNER JOIN
                         dbo.MT_Supplier AS ms ON ds.SupplierID = ms.ID INNER JOIN
                         dbo.MT_Material AS mt ON di.MaterialID = mt.ID INNER JOIN
                         dbo.View_DTInbound_LatestVersionID AS vs ON di.ID = vs.ID LEFT OUTER JOIN
                         dbo.DT_InboundQC AS d1 ON di.ID = d1.DT_InboundID AND d1.QCID = 1 LEFT OUTER JOIN
                         dbo.DT_InboundQC AS d2 ON di.ID = d2.DT_InboundID AND d2.QCID = 2 LEFT OUTER JOIN
                         dbo.DT_InboundQC AS d3 ON di.ID = d3.DT_InboundID AND d3.QCID = 3 LEFT OUTER JOIN
                         dbo.DT_InboundQuantity AS d4 ON di.ID = d4.DT_InboundID AND d4.QuantityTypeID = 14 LEFT OUTER JOIN
                         dbo.DT_InboundQuantity AS d5 ON di.ID = d5.DT_InboundID AND d5.QuantityTypeID = 15 LEFT OUTER JOIN
                         dbo.DT_InboundQuantity AS d6 ON di.ID = d6.DT_InboundID AND d6.QuantityTypeID = 2 LEFT OUTER JOIN
                             (SELECT        dis.ID, d2s.WeightTicket AS MscaleWeightTicket, d1s.WeightOutDateTime, dis.PlateLicense AS MscalePlateLicense, d1s.WeightIn / 1000 AS MScaleWeightIn, d1s.WeightOut / 1000 AS MSCaleWeightOut, 
                                                         d1s.NetWeight / 1000 AS MSCaleNetWeight, d4s.Value AS MSCaleMP, d5s.Value AS MSCaleOT, d6s.Value AS MSCalePM
                               FROM            MSCALE.MSCale.dbo.DT_WeightingTrans AS dis INNER JOIN
                                                         MSCALE.MSCale.dbo.DT_WeightingTransTask AS d1s ON dis.ID = d1s.ID INNER JOIN
                                                         MSCALE.MSCale.dbo.DT_WeightingTransItem AS d2s ON d1s.ID = d2s.TaskID LEFT OUTER JOIN
                                                         MSCALE.MSCale.dbo.DT_WeightingTransItemQC AS d4s ON d1s.ID = d4s.TaskID AND d4s.QCID = 1 LEFT OUTER JOIN
                                                         MSCALE.MSCale.dbo.DT_WeightingTransItemQC AS d5s ON d1s.ID = d5s.TaskID AND d5s.QCID = 2 LEFT OUTER JOIN
                                                         MSCALE.MSCale.dbo.DT_WeightingTransItemQC AS d6s ON d1s.ID = d6s.TaskID AND d6s.QCID = 3) AS MC ON di.DocID = MC.MscaleWeightTicket
WHERE        (mt.TypeID = 1) AND (di.DocStatusID IN (4, 7)) AND (di.TransactionTypeID = 1) AND (di.ReceiveDate >= DATEADD(month, - 6, GETDATE())) AND (MC.MScaleWeightIn <> d4.Amount) OR
                         (mt.TypeID = 1) AND (di.DocStatusID IN (4, 7)) AND (di.TransactionTypeID = 1) AND (di.ReceiveDate >= DATEADD(month, - 6, GETDATE())) AND (MC.MSCaleWeightOut <> d5.Amount) OR
                         (mt.TypeID = 1) AND (di.DocStatusID IN (4, 7)) AND (di.TransactionTypeID = 1) AND (di.ReceiveDate >= DATEADD(month, - 6, GETDATE())) AND (MC.MSCaleNetWeight <> d6.Amount) OR
                         (mt.TypeID = 1) AND (di.DocStatusID IN (4, 7)) AND (di.TransactionTypeID = 1) AND (di.ReceiveDate >= DATEADD(month, - 6, GETDATE())) AND (MC.MSCaleMP <> d1.Value) OR
                         (mt.TypeID = 1) AND (di.DocStatusID IN (4, 7)) AND (di.TransactionTypeID = 1) AND (di.ReceiveDate >= DATEADD(month, - 6, GETDATE())) AND (MC.MSCaleOT <> d2.Value) OR
                         (mt.TypeID = 1) AND (di.DocStatusID IN (4, 7)) AND (di.TransactionTypeID = 1) AND (di.ReceiveDate >= DATEADD(month, - 6, GETDATE())) AND (MC.MSCalePM <> d3.Value)
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[17] 4[21] 2[45] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "di"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 243
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "dt"
            Begin Extent = 
               Top = 6
               Left = 281
               Bottom = 136
               Right = 465
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ds"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 227
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ms"
            Begin Extent = 
               Top = 138
               Left = 265
               Bottom = 268
               Right = 454
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "mt"
            Begin Extent = 
               Top = 270
               Left = 38
               Bottom = 400
               Right = 253
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "vs"
            Begin Extent = 
               Top = 270
               Left = 291
               Bottom = 366
               Right = 461
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "d1"
            Begin Extent = 
               Top = 366
               Left = 291
               Bottom = 496
               Right = 461
            End
            DisplayFlags = 280
            TopColumn = 0
       ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'view_MPlan_ReportCompare_3809'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'  End
         Begin Table = "d2"
            Begin Extent = 
               Top = 402
               Left = 38
               Bottom = 532
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "d3"
            Begin Extent = 
               Top = 498
               Left = 246
               Bottom = 628
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "d4"
            Begin Extent = 
               Top = 534
               Left = 38
               Bottom = 664
               Right = 220
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "d5"
            Begin Extent = 
               Top = 630
               Left = 258
               Bottom = 760
               Right = 440
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "d6"
            Begin Extent = 
               Top = 666
               Left = 38
               Bottom = 796
               Right = 220
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "MC"
            Begin Extent = 
               Top = 6
               Left = 503
               Bottom = 136
               Right = 701
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 14
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'view_MPlan_ReportCompare_3809'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'view_MPlan_ReportCompare_3809'
GO

