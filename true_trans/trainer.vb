Imports Newtonsoft.Json
Structure traineee
    Dim word As String
    Dim eword As String
    Dim systranslation As ArrayList
    Dim sysword As String
End Structure

Public Class trainer
    Public man As New maglish
    'contains pre mapped words and suggetions 
    Public Function possible(a As String) As ArrayList
        Dim t As New ArrayList

        Return t
    End Function
    Public Function load()
        Return 1
    End Function
    Public Function transitrate(word As String) As ArrayList
        Dim s As ArrayList
        Return s
    End Function
    Public Function train()
        man.init()
        Dim t As New traineee
        Dim check As New maglish
        Dim mi As Integer = 1
        Dim writer As New System.IO.StreamWriter(Application.StartupPath + "\list.map", True)
        Dim traing_list As New System.IO.StreamReader(Application.StartupPath + "\wordlist.txt")
        While traing_list.Peek <> -1
            Dim m = traing_list.ReadLine.ToString
            Dim k = JsonConvert.DeserializeObject(Of ArrayList)(m)
            Dim s = man.one_word(k(0))
            ' Form2.ListBox1.Items.Add(k(0))
            '  Form2.ListBox2.Items.Add(k(1))
            '  Form2.ListBox3.Items.Add(s)
            Dim q = (man.split_word(k(0)))
            Dim si As String = ""
            For Each ty In q
                si = si + ty
            Next
            ' Form2.ListBox4.Items.Add(si)
            Form2.Label5.Text = mi.ToString
            Form2.ProgressBar1.Value = (mi / 109582) * 100
            mi += 1
            Form2.Refresh()
            'System.Threading.Thread.Sleep(500)
        End While
        Return 1 'completed succcessfully
    End Function
End Class
