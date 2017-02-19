Public Class LSES
    Dim Ask As MsgBoxResult
    Private Sub ManageExtensionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ManageExtensionsToolStripMenuItem.Click
        Ask = MsgBox("Sorry but the backend of this feature has not been finished, press ok if you want to access this feature.", MsgBoxStyle.OkCancel)
        If Ask = MsgBoxResult.Ok Then
            Manage.Show()
        End If
        If Ask = MsgBoxResult.Cancel Then

        End If
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub MenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub

    Private Sub LSES_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Button1.Enabled = False
        If My.Settings.TabletModeonstartup = "True" Then
            Me.WindowState = FormWindowState.Maximized
            MenuStrip1.Font = My.Settings.Menustrip_size_font_tabletmode
            MaximizeBox = False
            MinimizeBox = False
            FormBorderStyle = FormBorderStyle.FixedDialog
            TopMost = True
        End If
        If My.Settings.TabletModeonstartup = "False" Then
            Me.WindowState = FormWindowState.Normal
            MenuStrip1.Font = My.Settings.Menustrip_size_font_normal
            MaximizeBox = True
            MinimizeBox = True
            FormBorderStyle = FormBorderStyle.Sizable
            TopMost = False
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Process.Start("")
    End Sub
End Class