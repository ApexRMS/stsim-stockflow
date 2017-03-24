﻿'************************************************************************************
' STSimStockFlow: A .NET library for simulating stocks and flows
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports SyncroSim.Core
Imports SyncroSim.Core.Forms

Class FlowOrderDataFeedView

    Protected Overrides Sub InitializeView()

        MyBase.InitializeView()

        Dim v As DataFeedView = Me.Session.CreateMultiRowDataFeedView(Me.Scenario, Me.ControllingScenario)
        Me.PanelFlowOrder.Controls.Add(v)

    End Sub

    Public Overrides Sub LoadDataFeed(ByVal dataFeed As DataFeed)

        MyBase.LoadDataFeed(dataFeed)

        Me.SetCheckBoxBinding(Me.CheckBoxApplyBeforeTransitions, DATASHEET_FLOW_ORDER_OPTIONS, DATASHEET_FLOW_ORDER_OPTIONS_ABT_COLUMN_NAME)
        Me.SetCheckBoxBinding(Me.CheckBoxApplyEquallyRankedFlowsSimultaneously, DATASHEET_FLOW_ORDER_OPTIONS, DATASHEET_FLOW_ORDER_OPTIONS_AERS_COLUMN_NAME)

        Dim v As DataFeedView = CType(Me.PanelFlowOrder.Controls(0), DataFeedView)
        v.LoadDataFeed(dataFeed, DATASHEET_FLOW_ORDER)

    End Sub

    Public Overrides Sub EnableView(enable As Boolean)

        If (Me.PanelFlowOrder.Controls.Count > 0) Then
            Dim v As DataFeedView = CType(Me.PanelFlowOrder.Controls(0), DataFeedView)
            v.EnableView(enable)
        End If

        Me.LabelFlowOrder.Enabled = enable
        Me.CheckBoxApplyBeforeTransitions.Enabled = enable
        Me.CheckBoxApplyEquallyRankedFlowsSimultaneously.Enabled = enable

    End Sub

End Class

