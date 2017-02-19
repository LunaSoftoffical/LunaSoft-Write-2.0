Public Class About
    Private Sub About_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label2.Text = "LunaSoft Write Version: " & My.Settings.Product_Version
        If My.Settings.TabletModeonstartup = "True" Then
            MaximizeBox = False
            MinimizeBox = False
            TopMost = True
        End If
        If My.Settings.TabletModeonstartup = "False" Then
            MaximizeBox = False
            MinimizeBox = True
            TopMost = False
        End If
        Label3.Text = "Service Pack: " & My.Settings.Servicepack
        Label4.Text = "Update: " & My.Settings.Update
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Process.Start("http://www.lunasoft.site88.net")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        LEvb.Show()
    End Sub
End Class

