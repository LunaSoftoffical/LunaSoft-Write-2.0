Imports System.IO
Imports System
Imports System.Management
Imports System.Linq
Public Class Form1
    Dim file As System.IO.StreamWriter
    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        SaveFileDialog1.ShowDialog()
        If My.Computer.FileSystem.FileExists(SaveFileDialog1.FileName) Then
            Dim ask As MsgBoxResult
            ask = MsgBox("File already exists, would you like to replace it?", MsgBoxStyle.YesNo, "File Exists")
            If ask = MsgBoxResult.No Then
                SaveFileDialog1.ShowDialog()
            ElseIf ask = MsgBoxResult.Yes Then
                My.Computer.FileSystem.WriteAllText(SaveFileDialog1.FileName, TextBox1.Text, False)
                Dim filename As String
                filename = SaveFileDialog1.FileName
                Me.Text = filename & " - " & "LunaSoft Write 2.0 Technical Preview"
            End If
        Else
            Try
                My.Computer.FileSystem.WriteAllText(SaveFileDialog1.FileName, TextBox1.Text, False)
                Dim filename As String
                filename = SaveFileDialog1.FileName
                Me.Text = filename & " - " & "LunaSoft Write 2.0 Technical Preview"
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        If TextBox1.Modified Then
            Dim ask As MsgBoxResult
            ask = MsgBox("Do you want to save the changes", MsgBoxStyle.YesNoCancel, "Open Document")
            If ask = MsgBoxResult.No Then
                OpenFileDialog1.ShowDialog()
                TextBox1.Text = My.Computer.FileSystem.ReadAllText(OpenFileDialog1.FileName)
            ElseIf ask = MsgBoxResult.Cancel Then

            ElseIf ask = MsgBoxResult.Yes Then
                SaveFileDialog1.ShowDialog()
                My.Computer.FileSystem.WriteAllText(SaveFileDialog1.FileName, TextBox1.Text, False)
                TextBox1.Clear()
            End If
        Else
            OpenFileDialog1.ShowDialog()
            Try
                TextBox1.Text = My.Computer.FileSystem.ReadAllText(OpenFileDialog1.FileName)
                Dim filename As String
                filename = OpenFileDialog1.FileName
                Me.Text = filename & " - " & My.Settings.Titlebar
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub FontToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FontToolStripMenuItem.Click
        FontDialog1.ShowDialog()
        TextBox1.Font = FontDialog1.Font
        My.Settings.Default_Font = FontDialog1.Font
    End Sub

    Private Sub UndoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UndoToolStripMenuItem.Click
        If TextBox1.CanUndo Then
            TextBox1.Undo()
        Else
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)

    End Sub
    Private Sub SelectAllTextCtrlAToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectAllTextCtrlAToolStripMenuItem.Click
        TextBox1.SelectAll()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Computer.FileSystem.DirectoryExists("C:\LunaSoft\Write\UpdateDir") Then
            My.Settings.FirstBoot = "False"
            If My.Computer.FileSystem.DirectoryExists("C:\LunaSoft\Write\Extensions") Then
                My.Settings.FirstBoot = "False"
            Else
                My.Settings.FirstBoot = "True"
            End If
        Else
            My.Settings.FirstBoot = "True"
        End If
        If My.Settings.FirstBoot = "True" Then
            MsgBox("Before we can run this application, critical components will be installed.")
            My.Computer.FileSystem.CreateDirectory("C:\LunaSoft\Write\UpdateDir")
            My.Computer.FileSystem.CreateDirectory("C:\LunaSoft\Write\Extensions")
            My.Settings.FirstBoot = "False"
            Me.Text = "Untitled - " & My.Settings.Titlebar
            TextBox1.Font = My.Settings.Default_Font
            FontDialog1.Font = My.Settings.Default_Font
            TextBox1.ForeColor = My.Settings.Font_Colour
            ColorDialog1.Color = My.Settings.Font_Colour
            ColorDialog2.Color = My.Settings.Back_Colour
            Me.Size = My.Settings.Window_size
            Me.Location = My.Settings.Windowlastlocation
            WordWrapToolStripMenuItem.CheckOnClick = True
        End If
        If My.Settings.FirstBoot = "False" Then
            If TextBox1.Modified = False Then
                CutToolStripMenuItem.Enabled = False
                CopyToolStripMenuItem.Enabled = False
                UndoToolStripMenuItem.Enabled = False
                PasteToolStripMenuItem.Enabled = True
                ToolStripMenuItem1.Enabled = False
                FindToolStripMenuItem.Enabled = False
                SelectAllTextCtrlAToolStripMenuItem.Enabled = False
            End If
            If My.Settings.TabletModeonstartup = "True" Then
                Me.WindowState = FormWindowState.Maximized
                MenuStrip1.Font = My.Settings.Menustrip_size_font_tabletmode
                ActivateTabletModeToolStripMenuItem.Enabled = False
                ExitTabletModeToolStripMenuItem.Enabled = True
                MaximizeBox = False
                MinimizeBox = False
                FormBorderStyle = FormBorderStyle.FixedDialog
                TopMost = True
            End If
            If My.Settings.TabletModeonstartup = "False" Then
                Me.WindowState = FormWindowState.Normal
                MenuStrip1.Font = My.Settings.Menustrip_size_font_normal
                ActivateTabletModeToolStripMenuItem.Enabled = True
                ExitTabletModeToolStripMenuItem.Enabled = False
                MaximizeBox = True
                MinimizeBox = True
                FormBorderStyle = FormBorderStyle.Sizable
                TopMost = False
            End If
            Me.Text = "Untitled - " & My.Settings.Titlebar
            TextBox1.Font = My.Settings.Default_Font
            FontDialog1.Font = My.Settings.Default_Font
            TextBox1.ForeColor = My.Settings.Font_Colour
            ColorDialog1.Color = My.Settings.Font_Colour
            ColorDialog2.Color = My.Settings.Back_Colour
            Me.Size = My.Settings.Window_size
            Me.Location = My.Settings.Windowlastlocation
            WordWrapToolStripMenuItem.CheckOnClick = True
        End If
    End Sub

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        If TextBox1.Modified Then
            Dim ask As MsgBoxResult
            ask = MsgBox("Do you want to save the changes", MsgBoxStyle.YesNoCancel, "New Document")
            If ask = MsgBoxResult.No Then
                TextBox1.Clear()
                Me.Text = "Untitled -" & My.Settings.Titlebar
            ElseIf ask = MsgBoxResult.Cancel Then
            ElseIf ask = MsgBoxResult.Yes Then
                SaveFileDialog1.ShowDialog()
                My.Computer.FileSystem.WriteAllText(SaveFileDialog1.FileName, TextBox1.Text, False)
                TextBox1.Clear()
                Me.Text = "Untitled - " & My.Settings.Titlebar
            End If
        Else
            Me.Text = "Untitled - " & My.Settings.Titlebar
            TextBox1.Clear()
        End If
    End Sub

    Private Sub CutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CutToolStripMenuItem.Click
        My.Computer.Clipboard.Clear()
        If TextBox1.SelectionLength > 0 Then
            My.Computer.Clipboard.SetText(TextBox1.SelectedText)

        End If
        TextBox1.SelectedText = ""
    End Sub

    Private Sub CopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click
        My.Computer.Clipboard.Clear()
        If TextBox1.SelectionLength > 0 Then
            My.Computer.Clipboard.SetText(TextBox1.SelectedText)
        End If
    End Sub

    Private Sub PasteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasteToolStripMenuItem.Click
        If My.Computer.Clipboard.ContainsText Then
            TextBox1.Paste()
        End If
    End Sub

    Private Sub FindToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FindToolStripMenuItem.Click
        Dim a As String
        Dim b As String
        a = InputBox("Enter text to be found")
        b = InStr(TextBox1.Text, a)
        If b Then
            TextBox1.Focus()
            TextBox1.SelectionStart = b - 1
            TextBox1.SelectionLength = Len(a)
        Else
            MsgBox("Text not found.")
        End If
    End Sub

    Private Sub TextAlignMentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TextAlignMentToolStripMenuItem.Click

    End Sub

    Private Sub LeftToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LeftToolStripMenuItem.Click
        TextBox1.TextAlign = HorizontalAlignment.Left
        LeftToolStripMenuItem.Checked = True
        CenterToolStripMenuItem.Checked = False
        RightToolStripMenuItem.Checked = False
    End Sub

    Private Sub CenterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CenterToolStripMenuItem.Click
        TextBox1.TextAlign = HorizontalAlignment.Center
        LeftToolStripMenuItem.Checked = False
        CenterToolStripMenuItem.Checked = True
        RightToolStripMenuItem.Checked = False
    End Sub

    Private Sub RightToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RightToolStripMenuItem.Click
        TextBox1.TextAlign = HorizontalAlignment.Right
        LeftToolStripMenuItem.Checked = False
        CenterToolStripMenuItem.Checked = False
        RightToolStripMenuItem.Checked = True
    End Sub

    Private Sub AboutThisSoftwareToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutThisSoftwareToolStripMenuItem.Click
        About.Show()
    End Sub

    Private Sub SaveAsToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SaveAsToolStripMenuItem1.Click
        SaveFileDialog1.ShowDialog()
        If My.Computer.FileSystem.FileExists(SaveFileDialog1.FileName) Then
            Dim ask As MsgBoxResult
            ask = MsgBox("File already exists, would you like to replace it?", MsgBoxStyle.YesNo, "File Exists")
            If ask = MsgBoxResult.No Then
                SaveFileDialog1.ShowDialog()
            ElseIf ask = MsgBoxResult.Yes Then
                My.Computer.FileSystem.WriteAllText(SaveFileDialog1.FileName, TextBox1.Text, False)
                Dim filename As String
                filename = SaveFileDialog1.FileName
                Me.Text = filename & " - " & My.Settings.Titlebar
            End If
        Else
            Try
                My.Computer.FileSystem.WriteAllText(SaveFileDialog1.FileName, TextBox1.Text, False)
                Dim filename As String
                filename = SaveFileDialog1.FileName
                Me.Text = filename & " - " & My.Settings.Titlebar
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub FontColourToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FontColourToolStripMenuItem.Click
        ColorDialog1.ShowDialog()
        TextBox1.ForeColor = ColorDialog1.Color
        My.Settings.Font_Colour = ColorDialog1.Color
    End Sub

    Private Sub BackgroundColorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BackgroundColorToolStripMenuItem.Click
        ColorDialog2.ShowDialog()
        TextBox1.BackColor = ColorDialog2.Color
        My.Settings.Back_Colour = ColorDialog2.Color
    End Sub

    Private Sub StatusStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs)

    End Sub

    Private Sub ToolStripStatusLabel1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ToolStripStatusLabel1_Click_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click

    End Sub

    Private Sub WordWrapToolStripMenuItem_Click(sender As Object, e As EventArgs)
        TextBox1.WordWrap = True
    End Sub

    Private Sub TextBox1_TextChanged_1(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If TextBox1.Text = String.Empty Then
            TextBox1.Modified = False
        End If
        If TextBox1.Modified = False Then
            CutToolStripMenuItem.Enabled = False
            CopyToolStripMenuItem.Enabled = False
            UndoToolStripMenuItem.Enabled = False
            PasteToolStripMenuItem.Enabled = True
            ToolStripMenuItem1.Enabled = False
            FindToolStripMenuItem.Enabled = False
            SelectAllTextCtrlAToolStripMenuItem.Enabled = False
        End If
        If TextBox1.Modified = True Then
            CutToolStripMenuItem.Enabled = True
            CopyToolStripMenuItem.Enabled = True
            UndoToolStripMenuItem.Enabled = True
            PasteToolStripMenuItem.Enabled = True
            ToolStripMenuItem1.Enabled = True
            FindToolStripMenuItem.Enabled = True
            SelectAllTextCtrlAToolStripMenuItem.Enabled = True
        End If
    End Sub

    Private Sub LoadHTMLFormatToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadHTMLFormatToolStripMenuItem.Click
        TextBox1.Text = "
<html>
<head>
<title>Your website name here</title>
</head>
     <body>
          <font face=""Segoe UI"">
               <h2>Site Title here</h2>
               <hr>
          </font>
     </body>
</html>"
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Dim Ask As MsgBoxResult
        My.Settings.Window_size = Me.Size
        My.Settings.Windowlastlocation = Me.Location
        If TextBox1.Modified = True Then
            Ask = MsgBox("Do you want to save your changes?", MsgBoxStyle.YesNo)
            If Ask = MsgBoxResult.Yes Then
                If TextBox1.Modified = True Then
                    SaveFileDialog1.ShowDialog()
                    If My.Computer.FileSystem.FileExists(SaveFileDialog1.FileName) Then
                        Ask = MsgBox("File already exists, would you like to replace it?", MsgBoxStyle.YesNo, "File Exists")
                        If Ask = MsgBoxResult.No Then
                            SaveFileDialog1.ShowDialog()
                        ElseIf Ask = MsgBoxResult.Yes Then
                            My.Computer.FileSystem.WriteAllText(SaveFileDialog1.FileName, TextBox1.Text, False)
                        End If
                    Else
                        Try
                            My.Computer.FileSystem.WriteAllText(SaveFileDialog1.FileName, TextBox1.Text, False)
                        Catch ex As Exception
                        End Try
                    End If
                End If
            End If
        End If
        If Ask = MsgBoxResult.No Then

        End If
    End Sub

    Private Sub WordWrapToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles WordWrapToolStripMenuItem.Click
        If WordWrapToolStripMenuItem.Checked = True Then
            TextBox1.WordWrap = True
        End If
        If WordWrapToolStripMenuItem.Checked = False Then
            TextBox1.WordWrap = False
        End If
    End Sub

    Private Sub MenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub

    Private Sub LunaSoftExtensionsToolStripMenuItem_Click(sender As Object, e As EventArgs)
        LSES.Show()
    End Sub

    Private Sub FormatToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FormatToolStripMenuItem.Click

    End Sub

    Private Sub FeedbackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FeedbackToolStripMenuItem.Click
        Process.Start("https://goo.gl/forms/4itU47creQ1DtEmC2")
    End Sub

    Private Sub EditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem.Click

    End Sub

    Private Sub ToolStripMenuItem1_Click_1(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        TextBox1.Clear()
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        Application.Exit()
    End Sub

    Private Sub ActivateTabletModeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ActivateTabletModeToolStripMenuItem.Click
        Me.WindowState = FormWindowState.Maximized
        MenuStrip1.Font = My.Settings.Menustrip_size_font_tabletmode
        ActivateTabletModeToolStripMenuItem.Enabled = False
        ExitTabletModeToolStripMenuItem.Enabled = True
        My.Settings.TabletModeonstartup = "True"
        MaximizeBox = False
        MinimizeBox = False
        FormBorderStyle = FormBorderStyle.FixedDialog
        TopMost = True
    End Sub

    Private Sub ExitTabletModeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitTabletModeToolStripMenuItem.Click
        Me.WindowState = FormWindowState.Normal
        MenuStrip1.Font = My.Settings.Menustrip_size_font_normal
        ActivateTabletModeToolStripMenuItem.Enabled = True
        ExitTabletModeToolStripMenuItem.Enabled = False
        My.Settings.TabletModeonstartup = "False"
        MaximizeBox = True
        MinimizeBox = True
        FormBorderStyle = FormBorderStyle.Sizable
        TopMost = False
    End Sub

    Private Sub UpdateBetaToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Form2.Show()
    End Sub

    Private Sub UpdateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpdateToolStripMenuItem.Click
        Form2.Show()
    End Sub
End Class