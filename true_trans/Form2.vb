Imports Newtonsoft.Json
Public Class Form2
    Dim writer As New System.IO.StreamWriter(Application.StartupPath + "\list.map", True)
    Dim traing_list As New System.IO.StreamReader(Application.StartupPath + "\wordlist.txt")
    Public man As New maglish
    Dim mi As Integer = 1

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Dim s As New trainer
        's.train()
        'If traing_list.Peek = -1 Then
        'MsgBox("end of list")
        'Exit Sub
        'End If
        'Dim m = traing_list.ReadLine.ToString
        'Dim k = JsonConvert.DeserializeObject(Of ArrayList)(m)
        'Dim s = man.one_word(k(0))
        'Dim q = (man.split_word(k(0)))
        '  Dim si As String = ""
        '  For Each ty In q
        'si = si + ty
        '  Next
        '  mi += 1
        ' Label5.Text = mi
        ' TextBox1.Text = k(0)
        ' TextBox2.Text = k(1)
        ' TextBox3.Text = s
        ' TextBox4.Text = si
        Timer1.Start()
    End Sub

    Private Sub ProgressBar1_Click(sender As Object, e As EventArgs) Handles ProgressBar1.Click

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If traing_list.Peek = -1 Then
            MsgBox("end of list")
            Timer1.Stop()
            Exit Sub
        End If
        Dim m = traing_list.ReadLine.ToString
        Dim k = JsonConvert.DeserializeObject(Of ArrayList)(m)
        Dim s = man.one_word(k(0))
        Dim q = (man.split_word(k(0)))
        Dim si As String = ""
        For Each ty In q
            si = si + ty
        Next
        mi += 1
        Label5.Text = mi
        TextBox1.Text = k(0)
        TextBox2.Text = k(1)
        TextBox3.Text = s
        TextBox4.Text = si
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        man.init()
        Form1.Show()
    End Sub
End Class