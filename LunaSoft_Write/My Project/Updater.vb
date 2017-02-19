Public Class Updater
    Dim lu_updateServer = "http://www.lunasoft.ga"
    Dim lu_productVersion = My.Settings.Updater_Version
    Dim lu_productName = "spider"
    Dim lu_checkFile = "$server/$product/latest.txt"
    Dim lu_updateFile = "$server/$product/$nv.exe"
    Dim lu_tempfolder = "$temp_folder"
    Dim lu_autoCheck = True
    Dim lu_autoDownload = True
    Dim lu_autoRun = True

    Dim lu_latest = lu_productVersion
    Dim lu_isChecking = False

    Private Sub updater_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = lu_isChecking
    End Sub
    Function replaceVars(input As String)
        Return input.Replace("$server", lu_updateServer).Replace("$product", lu_productName).Replace("$cv", lu_productVersion).Replace("$nv", lu_latest).Replace("$temp_folder", My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData)
    End Function
    Sub checkUpdates()
        Try
            My.Computer.Network.DownloadFile(replaceVars(lu_checkFile), replaceVars(lu_tempfolder) & "\latest.tmp", "", "", False, 36000, True)
            lu_latest = My.Computer.FileSystem.ReadAllText(replaceVars(lu_tempfolder) & "\latest.tmp")
            My.Computer.FileSystem.DeleteFile(replaceVars(lu_tempfolder) & "\latest.tmp")
            If lu_latest = lu_productVersion Then
                Label2.Show()
                Label2.Text = "You are already up to date"
            Else
                If lu_autoDownload Then
                    downloadUpdate()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
            Label2.Show()
            Label2.Text = "We encountered a problem :/"
            ProgressBar1.Value = 100
        End Try
    End Sub
    Sub downloadUpdate()
        Try
            'Process.Start(replaceVars(lu_updateFile))
            My.Computer.Network.DownloadFile(replaceVars(lu_updateFile), replaceVars(lu_tempfolder) & "\latest.tmp.exe", "", "", False, 36000, True)
            If lu_autoRun Then
                runUpdate()
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Sub runUpdate()
        Try
            Process.Start(replaceVars(lu_tempfolder) & "\latest.tmp.exe")
            Application.Exit()
        Catch ex As Exception
            Button1.Enabled = True
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        checkUpdates()
        ProgressBar1.Value = "0"
    End Sub

    Private Sub Updater_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        checkUpdates()
        Timer1.Start()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ProgressBar1.Increment(+10)
        If ProgressBar1.Value = 100 Then

        End If
    End Sub
End Class