Public Class Form3

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Form3_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        Dim c As New System.Drawing.Pen(Brushes.Silver)
        Dim k As New System.Drawing.Rectangle(0, 0, Me.Width - 1, Me.Height - 1)
        c.Color = Color.Silver
        e.Graphics.DrawRectangle(c, k)
        c.Color = Color.FromArgb(109, 168, 218)
        e.Graphics.DrawLine(c, TextBox1.Location.X - 5, TextBox1.Location.Y + TextBox1.Height + 3, TextBox1.Location.X + TextBox1.Width + 10, TextBox1.Location.Y + TextBox1.Height + 3)
        e.Graphics.DrawLine(c, TextBox2.Location.X - 5, TextBox2.Location.Y + TextBox2.Height + 3, TextBox2.Location.X + TextBox2.Width + 10, TextBox2.Location.Y + TextBox2.Height + 3)
        c.Color = Color.FromArgb(220, 228, 250)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim writer As New System.IO.StreamWriter(Application.StartupPath.ToString + "\packages\malayalam\map\pre_trans.map", True)
        writer.Write(vbNewLine + TextBox1.Text + ":" + TextBox2.Text)
        writer.Close()
        Me.Close()
    End Sub
End Class