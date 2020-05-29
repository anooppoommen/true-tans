Public Class Form4

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckBox1.Checked = My.Settings.pretrans_map
        CheckBox2.Checked = My.Settings.check_for_updates
        CheckBox3.Checked = My.Settings.Share_changes_to_pretrans_map
        CheckBox4.Checked = My.Settings.Custom_regex
        CheckBox5.Checked = My.Settings.report_bugs
    End Sub

    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer

    Private Sub Form4_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        drag = True 'Sets the variable drag to true.
        mousex = Windows.Forms.Cursor.Position.X - Me.Left 'Sets variable mousex
        mousey = Windows.Forms.Cursor.Position.Y - Me.Top 'Sets variable mousey
    End Sub

    Private Sub Form4_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If drag Then
            Me.Top = Windows.Forms.Cursor.Position.Y - mousey
            Me.Left = Windows.Forms.Cursor.Position.X - mousex
        End If
    End Sub

    Private Sub Form4_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        drag = False
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Me.Close()
    End Sub

    Private Sub PictureBox2_MouseHover(sender As Object, e As EventArgs) Handles PictureBox2.MouseHover
        PictureBox2.Image = My.Resources.close_hover__light
    End Sub

    Private Sub PictureBox2_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox2.MouseLeave

        PictureBox2.Image = My.Resources.close_hover
    End Sub

    Private Sub Form4_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        Dim pen As New System.Drawing.Pen(Brushes.Black)
        Dim rect = New Rectangle(0, 0, Me.Width - 1, Me.Height - 1)
        e.Graphics.DrawRectangle(pen, rect)
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        My.Settings.pretrans_map = CheckBox1.Checked
        update_form()
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        My.Settings.check_for_updates = CheckBox2.Checked
        update_form()
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        My.Settings.Share_changes_to_pretrans_map = CheckBox3.Checked
        update_form()
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        My.Settings.Custom_regex = CheckBox4.Checked
        update_form()
    End Sub

    Private Sub CheckBox5_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox5.CheckedChanged
        My.Settings.report_bugs = CheckBox5.Checked
        update_form()
    End Sub

    Public Sub update_form()
        My.Settings.Save()
        If (My.Settings.Custom_regex = True) Then
            Panel1.Visible = True
            TextBox1.Text = My.Settings.Package_name
            TextBox2.Text = My.Settings.Package_defnition
        Else
            Panel1.Visible = False
        End If
        If My.Settings.Custom_regex = False Then
            My.Settings.Package_name = "Malayalam(Default)"
            My.Settings.Package_defnition = "malayalam.json"
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        My.Settings.Package_name = TextBox1.Text
        My.Settings.Package_defnition = TextBox2.Text
        My.Settings.Save()
    End Sub
End Class