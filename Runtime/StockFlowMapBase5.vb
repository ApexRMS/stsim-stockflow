﻿'*********************************************************************************************
' STSimStockFlow: A SyncroSim Module for the ST-Sim Stocks and Flows Add-In.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Core
Imports SyncroSim.Common

MustInherit Class StockFlowMapBase5(Of T)
    Inherits StockFlowMapBase

    Private m_map As New MultiLevelKeyMap5(Of SortedKeyMap2(Of T))

    Protected Sub New(ByVal scenario As Scenario)
        MyBase.New(scenario)
    End Sub

    Protected Sub AddItem(
        ByVal k1 As Nullable(Of Integer),
        ByVal k2 As Nullable(Of Integer),
        ByVal k3 As Nullable(Of Integer),
        ByVal k4 As Nullable(Of Integer),
        ByVal k5 As Nullable(Of Integer),
        ByVal iteration As Nullable(Of Integer),
        ByVal timestep As Nullable(Of Integer),
        ByVal item As T)

        Dim m As SortedKeyMap2(Of T) = Me.m_map.GetItemExact(k1, k2, k3, k4, k5)

        If (m Is Nothing) Then

            m = New SortedKeyMap2(Of T)(SearchMode.ExactPrev)
            Me.m_map.AddItem(k1, k2, k3, k4, k5, m)

        End If

        Dim v As T = m.GetItemExact(iteration, timestep)

        If (v IsNot Nothing) Then
            ThrowDuplicateItemException()
        End If

        m.AddItem(iteration, timestep, item)
        Me.SetHasItems()

    End Sub

    Protected Function GetItemExact(
        ByVal k1 As Nullable(Of Integer),
        ByVal k2 As Nullable(Of Integer),
        ByVal k3 As Nullable(Of Integer),
        ByVal k4 As Nullable(Of Integer),
        ByVal k5 As Nullable(Of Integer),
        ByVal iteration As Nullable(Of Integer),
        ByVal timestep As Nullable(Of Integer)) As T

        Dim m As SortedKeyMap2(Of T) = Me.m_map.GetItemExact(k1, k2, k3, k4, k5)

        If (m Is Nothing) Then
            Return Nothing
        End If

        Return m.GetItemExact(iteration, timestep)

    End Function

    Protected Function GetItem(
        ByVal k1 As Nullable(Of Integer),
        ByVal k2 As Nullable(Of Integer),
        ByVal k3 As Nullable(Of Integer),
        ByVal k4 As Nullable(Of Integer),
        ByVal k5 As Nullable(Of Integer),
        ByVal iteration As Nullable(Of Integer),
        ByVal timestep As Nullable(Of Integer)) As T

        If (Not Me.HasItems) Then
            Return Nothing
        End If

        Dim p As SortedKeyMap2(Of T) = Me.m_map.GetItem(k1, k2, k3, k4, k5)

        If (p Is Nothing) Then
            Return Nothing
        End If

        Return p.GetItem(iteration, timestep)

    End Function

End Class