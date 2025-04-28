Imports System.Net.Mime.MediaTypeNames
Imports System.Threading
Imports System.Xml
Imports Microsoft.VisualBasic
Public Class NSTSTLogger

    Private Property sw As System.IO.StreamWriter
    Private Property _RT_CD As String = ""
    ' ログ基準レベル
    Private Property _logBaseLevel As Integer = 1

    ' パス設定
    Private ReadOnly BASE_PATH1 As String = "logs\"
    Private ReadOnly BASE_PATH2 As String = "ASX\"
    ' ログ固有文字
    Private ReadOnly ORIGINVALUE As String = "NSTST"


    Public Sub New(logName As String, driveString As String, rtcd As String, logBaseLevel As Integer)

        InitLogFile(logName, driveString, rtcd, logBaseLevel)

    End Sub

    Public Sub New(logName As String, driveString As String, rtcd As String)

        InitLogFile(logName, driveString, rtcd, _logBaseLevel)

    End Sub

    Private Sub InitLogFile(logName As String, driveString As String, rtcd As String, logBaseLevel As Integer)
        If logBaseLevel = 0 Then Return

        Dim dt As DateTime = DateTime.Now
        Dim sb As New System.Text.StringBuilder()

        _RT_CD = rtcd
        _logBaseLevel = logBaseLevel

        If logName.Length = 0 Then Return
        If Not "ASX".Contains(_RT_CD) Then Return

        ' GUID取得
        Dim guidValue As Guid = Guid.NewGuid()

        Try
            ' ディレクトリ存在チェック 存在しない場合はディレクトリ生成
            sb.Append(driveString)
            sb.Append(BASE_PATH1)
            If Not System.IO.Directory.Exists(sb.ToString) Then
                System.IO.Directory.CreateDirectory(sb.ToString)
            End If
            sb.Append(BASE_PATH2)
            If Not System.IO.Directory.Exists(sb.ToString) Then
                System.IO.Directory.CreateDirectory(sb.ToString)
            End If
            sb.Append(logName)
            sb.Append("\")
            If Not System.IO.Directory.Exists(sb.ToString) Then
                System.IO.Directory.CreateDirectory(sb.ToString)
            End If

            ' ログファイル名生成
            sb.Append(logName)
            sb.Append("_")
            sb.Append(dt.ToString("yyyyMMdd"))
            sb.Append("_")
            sb.Append(guidValue)
            sb.Append(".log")

            ' streamwriter初期化
            sw = New System.IO.StreamWriter(sb.ToString, True)

        Catch ex As Exception
            Console.WriteLine("InitLogFile error " & ex.Message)
        End Try
    End Sub

    Public Sub CreateLog(className As String, methodName As String, rowNum As Integer, msg As String, id As String)

        CreateLog(className, methodName, rowNum, msg, id, _logBaseLevel)

    End Sub

    Public Sub CreateLog(className As String, methodName As String, rowNum As Integer, msg As String, id As String, logLevel As Integer)
        If Not "ASX".Contains(_RT_CD) Then Return
        If _logBaseLevel = 0 Then Return
        If _logBaseLevel < logLevel Then Return

        ' ロック
        'Await _Semaphore.WaitAsync()

        Dim dt As DateTime = DateTime.Now
        Dim sb As New System.Text.StringBuilder()

        ' ログ作成
        sb.Clear()
        sb.Append(dt.ToString("yyyy/MM/dd HH:mm:ss.fff "))
        sb.Append(id)
        sb.Append(" ")
        sb.Append(ORIGINVALUE)
        sb.Append(" ")
        sb.Append(className)
        sb.Append(" ")
        sb.Append(methodName)
        sb.Append(" ")
        sb.Append(rowNum.ToString)
        sb.Append(" ")
        sb.Append(msg)

        Try
            ' ログ出力
            'Await sw.WriteLineAsync(sb.ToString)
            sw.WriteLineAsync(sb.ToString)

        Catch ex As Exception
            Console.WriteLine("CreateLog error " & ex.Message)
        End Try

        ' ロック解除
        '_Semaphore.Release()

    End Sub

    Public Sub WriteLogToFile()
        If Not "ASX".Contains(_RT_CD) Then Return
        If _logBaseLevel = 0 Then Return
        Try
            sw.Close()
        Catch ex As Exception
            Console.WriteLine("WriteLogToFile error " & ex.Message)
        End Try
    End Sub

End Class
