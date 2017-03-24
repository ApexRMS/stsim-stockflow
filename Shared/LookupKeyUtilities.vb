﻿'************************************************************************************
' STSimStockFlow: A .NET library for simulating stocks and flows
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports SyncroSim.Core

Module LookupKeyUtilities

    Public Function GetNullableKey(ByVal value As Nullable(Of Integer)) As Integer

        If (value.HasValue) Then
            Return value.Value
        Else
            Return 0
        End If

    End Function

    Public Function CreateRecordLookup(ByVal ds As DataSheet, ByVal colName As String) As Dictionary(Of Integer, Boolean)

        Dim d As New Dictionary(Of Integer, Boolean)
        Dim dt As DataTable = ds.GetData()

        For Each dr As DataRow In dt.Rows

            If (dr.RowState <> DataRowState.Deleted) Then
                d.Add(CInt(dr(colName)), True)
            End If

        Next

        Return d

    End Function

End Module
