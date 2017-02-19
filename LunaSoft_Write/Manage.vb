Public Class Manage
    Private Sub Manage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Settings.TabletModeonstartup = "True" Then
            Me.WindowState = FormWindowState.Maximized
            MaximizeBox = False
            MinimizeBox = False
            FormBorderStyle = FormBorderStyle.FixedDialog
            TopMost = True
        End If
        If My.Settings.TabletModeonstartup = "False" Then
            Me.WindowState = FormWindowState.Normal
            MaximizeBox = True
            MinimizeBox = True
            FormBorderStyle = FormBorderStyle.Sizable
            TopMost = False
        End If
        Dim extensions = My.Settings.ExtensionsDirectory
        My.Computer.FileSystem.GetFiles(My.Settings.ExtensionsDirectory)
    End Sub
End Class