Imports System.Text.RegularExpressions
Public Class Form1
    Public s As New maglish
    Public maglish_text As String = ""
    Dim maglish_c As String = ""
    Dim malindex As Integer = 0
    Dim engindex As Integer = 0
    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer



    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox23.Select()
        If My.Settings.Custom_regex = True Then
            s.load_package(My.Settings.Package_defnition)
        End If
        s.init()
        s.init_pretrans()
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        maglish_text = TextBox1.Text
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        maglish_text = TextBox1.Text
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        TextBox23.Text = s.many_words(TextBox1.Text.ToString)
        TextBox23.SelectionStart = TextBox23.Text.Length
        Dim re = New Regex("[\S]+")
        Word_count.Text = re.Matches(TextBox23.Text).Count
        call_mal_text()
    End Sub

    Private Sub TextBox23_Click(sender As Object, e As EventArgs) Handles TextBox23.Click
        call_mal_text()
    End Sub

    Private Sub TextBox2_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox23.KeyUp
        'MsgBox(e.KeyCode)
        If e.KeyCode >= 65 And e.KeyCode <= 90 Or e.KeyCode = 32 Then
            If (TextBox23.Text.Length <> 0) Then
                maglish_c = TextBox23.Text.Substring(TextBox23.SelectionStart - 1, 1)
                If (TextBox23.SelectionStart = TextBox23.Text.Length) Then
                    maglish_text += maglish_c
                Else
                    call_mal_text()
                End If
                'maglish_text += TextBox23.Text.Substring(TextBox23.Text.Length - 1)
            Else
                maglish_text = ""
            End If
        ElseIf (e.KeyCode = 8) Then
            If (maglish_text.Length > 0 And maglish_text.Length - 1 > 0) Then
                maglish_text = maglish_text.Substring(0, maglish_text.Length - 1)
            Else
                maglish_text = ""
            End If
        ElseIf e.KeyCode = 13 Then
            maglish_text += vbNewLine
        ElseIf e.KeyCode >= 37 And e.KeyCode <= 40 Then
            call_mal_text()

            If (TextBox23.SelectionStart - 1 >= 0 And e.KeyCode = 37) Then
                If TextBox23.Text.Substring(TextBox23.SelectionStart - 1, 1) = " " Then
                    TextBox23.SelectionStart = TextBox23.SelectionStart - 1
                End If
            End If
        ElseIf e.KeyValue <> 16 And e.KeyValue <> Keys.CapsLock And e.KeyValue <> Keys.NumLock And e.KeyValue <> Keys.ShiftKey And e.KeyValue <> Keys.ControlKey Then
            If TextBox23.Text.Length > 0 Then
                maglish_text += TextBox23.Text.Substring(TextBox23.Text.Length - 1)
            End If
        End If

        TextBox1.Text = maglish_text
    End Sub
    Private Sub call_mal_text()

        Dim re = New Regex("[\S]+")
        Dim res = New Regex("[\s]+")

        Dim malayalam_words = re.Matches(TextBox23.Text)
        Dim spaces_mala = res.Matches(TextBox23.Text)
        Dim english_words = re.Matches(TextBox1.Text)
        Dim spaces_eng = res.Matches(TextBox1.Text)

        Dim has_start_mal = False
        Dim has_start_eng = False
        Dim matched_index = False

        If TextBox23.Text.Length > 0 Then
            If TextBox23.Text(0) = " " Then
                has_start_mal = True
            ElseIf TextBox1.Text(0) = " " Then
                has_start_eng = True
            End If
        End If

        Dim word_index = 0
        Dim word_count = 0
        Dim space_index = 0

        If has_start_eng = False And has_start_mal = False Then
            For Each m In malayalam_words
                word_index += m.ToString.Length
                If word_index >= TextBox23.SelectionStart Then
                    matched_index = True
                    If (TextBox23.SelectionStart + 2 < TextBox23.Text.Length) Then
                        If TextBox23.Text.Substring(TextBox23.SelectionStart + 1, 1) = " " Then
                            matched_index = False
                        End If
                    End If
                    If (TextBox23.SelectionStart - 2 > 0) Then
                        If TextBox23.Text.Substring(TextBox23.SelectionStart - 1, 1) <> " " Then
                            matched_index = True
                        End If
                    End If
                    Exit For
                End If
                If (spaces_mala.Count > 0 And space_index < spaces_mala.Count) Then
                    word_index += spaces_mala.Item(space_index).Length
                    space_index += 1
                End If
                word_count += 1
            Next
        Else
            If (spaces_mala.Count > 0 And space_index < spaces_mala.Count) Then
                word_index += spaces_mala.Item(space_index).Length
            End If
            For Each m In malayalam_words
                word_index += m.ToString.Length
                If word_index >= TextBox23.SelectionStart Then
                    'MsgBox(m.ToString)
                    Exit For
                End If
                If (spaces_mala.Count > 0 And space_index < spaces_mala.Count) Then
                    word_index += spaces_mala.Item(space_index).Length
                    space_index += 1
                End If
            Next
        End If
        If word_count < malayalam_words.Count And word_count < english_words.Count And matched_index = True Then
            Label2.Text = english_words.Item(word_count).ToString + ":" + malayalam_words.Item(word_count).ToString
        Else
            Label2.Text = "-:-"
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        End
    End Sub

    Private Sub Panel2_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel2.MouseDown, Panel1.MouseDown, MyBase.MouseDown
        drag = True 'Sets the variable drag to true.
        mousex = Windows.Forms.Cursor.Position.X - Me.Left 'Sets variable mousex
        mousey = Windows.Forms.Cursor.Position.Y - Me.Top 'Sets variable mousey
    End Sub

    Private Sub Panel2_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel2.MouseMove, Panel1.MouseMove, MyBase.MouseMove
        If drag Then
            Me.Top = Windows.Forms.Cursor.Position.Y - mousey
            Me.Left = Windows.Forms.Cursor.Position.X - mousex
        End If
    End Sub

    Private Sub Panel2_MouseUp(sender As Object, e As MouseEventArgs) Handles Panel2.MouseUp, Panel1.MouseUp, MyBase.MouseUp
        drag = False
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Opacity = 30
        Me.Refresh()
        Me.WindowState = FormWindowState.Minimized
        Me.Opacity = 100
    End Sub
    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        End
    End Sub

    Private Sub PictureBox2_MouseHover(sender As Object, e As EventArgs) Handles PictureBox2.MouseHover
        PictureBox2.Image = My.Resources.close_hover
    End Sub

    Private Sub PictureBox2_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox2.MouseLeave
        PictureBox2.Image = My.Resources.close_hover_grey
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Dim k = Label2.Text.Split(":")
        Form3.TextBox1.Text = k(0)
        Form3.TextBox2.Text = k(1)
        Form3.Show()
    End Sub

    Private Sub Form1_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        Dim pen As New System.Drawing.Pen(Brushes.DimGray)
        Dim border_rectangle As New System.Drawing.Rectangle(0, 0, Me.Width - 1, Me.Height - 1)
        Dim text_border As New Rectangle(12, 74, 868, 429)

        'e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        e.Graphics.DrawRectangle(pen, border_rectangle)
        pen.Color = Color.FromArgb(232, 235, 237)
        pen.DashStyle = Drawing2D.DashStyle.Dot
        e.Graphics.DrawRectangle(pen, Panel3.Location.X - 2, Panel3.Location.Y - 2, Panel3.Width + 3, Panel3.Height + 3)
    End Sub

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Panel3.Paint
        Dim pen As New System.Drawing.Pen(Brushes.DimGray)
        pen.Color = Color.FromArgb(232, 235, 237)
        Dim text_border As New Rectangle(0, 0, Panel3.Width - 1, Panel3.Height - 1)
        e.Graphics.DrawRectangle(pen, text_border)
        e.Graphics.DrawLine(pen, New Point(0, (TextBox23.Location.Y + TextBox23.Height + 5)), New Point(Panel3.Width, (TextBox23.Location.Y + TextBox23.Height + 5)))
        e.Graphics.DrawLine(pen, New Point(0, (TextBox1.Location.Y - 5)), New Point(Panel3.Width, (TextBox1.Location.Y - 5)))
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Form4.Show()
        Form4.Select()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs)
        s.load_package("malayalam.json")
    End Sub
End Class
