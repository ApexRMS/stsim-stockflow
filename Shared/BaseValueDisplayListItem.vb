﻿'************************************************************************************
' STSimStockFlow: A .NET library for simulating stocks and flows
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Class BaseValueDisplayListItem

    Private m_Value As Integer
    Private m_Display As String

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="value"></param>
    ''' <param name="display"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal value As Integer, ByVal display As String)

        Me.m_Value = value
        Me.m_Display = display

    End Sub

    ''' <summary>
    ''' Gets the value for this combo item
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Value() As Integer
        Get
            Return Me.m_Value
        End Get
    End Property

    ''' <summary>
    ''' Overrides ToString
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function ToString() As String
        Return Me.m_Display
    End Function

End Class
