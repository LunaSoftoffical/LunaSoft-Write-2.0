Imports System.IO
Imports System.IO.Compression
Public Class Form2
    Dim lu_updateServer = "http://www.lunasoft.rf.gd/"
    Dim lu_productVersion = My.Settings.Product_Version
    Dim lu_updaterVersion = My.Settings.Updater_Version
    Dim lu_productName = "write"
    Dim lu_checkFile = "$server/$product/latest.txt"
    Dim lu_updateFile = "$server/$product/$nv.exe"
    Dim lu_tempfolder = "C:\LunaSoft\Write\UpdateDir"
    Dim lu_autoCheck = True
    Dim lu_autoDownload = True
    Dim lu_autoRun = True
    Dim lu_latest = lu_productVersion
    Dim lu_isChecking = False
    Private Sub updater_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = lu_isChecking
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.Text = "Welcome to LunaSoft Write Update!"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Label1.Text = "Checking for updates..."
        Timer1.Start()
        ProgressBar1.Value = 0
        checkUpdates()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ProgressBar1.Increment(+10)
        If ProgressBar1.Value = 100 Then

        End If
    End Sub
    Function replaceVars(input As String)
        Return input.Replace("$server", lu_updateServer).Replace("$product", lu_productName).Replace("$cv", lu_productVersion).Replace("$nv", lu_latest).Replace("$temp_folder", My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData)
    End Function
    Sub checkUpdates()
        Try
            Label2.Text = "Check For Updates: Checking..."
            My.Computer.Network.DownloadFile(replaceVars(lu_checkFile), replaceVars(lu_tempfolder) & "\latest.tmp", "", "", False, 3600, True)
            lu_latest = My.Computer.FileSystem.ReadAllText(replaceVars(lu_tempfolder) & "\latest.tmp")
            My.Computer.FileSystem.DeleteFile(replaceVars(lu_tempfolder) & "\latest.tmp")
            If lu_latest = lu_productVersion Then
                Label2.Text = "Check For Updates: Up to date"
            Else
                Label2.Text = "Check For Updates: Out of date."
                If lu_autoDownload Then
                    downloadUpdate()
                End If
            End If
        Catch
            Label2.Text = "Check For Update: Error."
        End Try
    End Sub
    Sub downloadUpdate()
        Try
            Label2.Text = "Download Update Package: Downloading..."
            My.Computer.Network.DownloadFile(replaceVars(lu_updateFile), replaceVars(lu_tempfolder) & "\latest.tmp.exe", "", "", False, 3600, True)
            Label2.Text = "Download Update Package: Done."
            If lu_autoRun Then
                runUpdate()
            End If
        Catch ex As Exception
            Label2.Text = "Download Update Package: Error."
            MsgBox(ex.ToString)
        End Try
    End Sub
    Sub runUpdate()
        Try
            Process.Start(replaceVars(lu_tempfolder) & "\latest.tmp.exe")
            Application.Exit()
        Catch ex As Exception
            Label2.Text = "Launch Updated Version: Error."
        End Try
    End Sub
End Class