Public Class LEvb
    Private Sub LEvb_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Notetext
        Notetext = My.Computer.FileSystem.ReadAllText("C:\LunaSoft\Write\Latestadditions.txt")
        TextBox1.Text = Notetext
    End Sub
End Class
