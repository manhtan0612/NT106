using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;




namespace File_Explorer
{

    public partial class Form1 : Form
    {
        bool cut = false;
        string temp;
        int count = 1;
        string sourceFilePath;
        string sourceFolderPath;
        List<string> pathHistory = new List<string>();

        bool ok = false;
        public Form1()
        {
            InitializeComponent();
        }



        private void button4_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fd = new FolderBrowserDialog())
            {
                if (fd.ShowDialog() == DialogResult.OK)
                {

                    string selectedPath = fd.SelectedPath;
                    temp = selectedPath;
                    DSThuMucFile(selectedPath);
                    textBox1.Text = selectedPath;
                    pathHistory.Add(temp);

                }
            }


        }

        private void DSThuMucFile(string path)
        {
            int i = 1;
            listView1.Items.Clear();
            string[] folders = Directory.GetDirectories(path);
            string[] files = Directory.GetFiles(path);
            foreach (string folder in folders)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(folder);
                ListViewItem item1 = new ListViewItem(directoryInfo.Name);
                item1.SubItems.Add(directoryInfo.LastWriteTime.ToString());
                item1.SubItems.Add("File folder".ToString());
                listView1.Items.Add(item1);
            }
            foreach (string file in files)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(file);
                ListViewItem item1 = new ListViewItem(Path.GetFileNameWithoutExtension(file)); //Ten File
                item1.SubItems.Add(directoryInfo.LastWriteTime.ToString()); // Thoi gian truy cap File lan cuoi 
                item1.SubItems.Add(TypeOfFile(file)); // Kieu cua File
                FileInfo fileInfo = new FileInfo(file);
                item1.SubItems.Add(FormatSize(fileInfo.Length).ToString()); // Kich thuoc File 

                listView1.Items.Add(item1);
            }
        }
        private string TypeOfFile(string path)
        {
            switch (Path.GetExtension(path))
            {
                case ".pptx":
                    return "Mircosoft Powerpoint Presention";
                //File Văn Bản
                case ".doc":
                    return "Microsoft Word Document";
                case ".docx":
                    return "Microsoft Word Document";
                case ".txt":
                    return "Text Document";
                case ".pdf":
                    return "PDF File";
                //File Hình Ảnh
                case ".jpg":
                    return "JPEG Image";
                case ".png":
                    return "PNG Image";
                case ".gif":
                    return "GIF Image";
                case ".ico":
                    return "ICO Image";
                //File Âm Thanh
                case ".mp3":
                    return "MP3 Audio";
                case ".wav":
                    return "WAV Audio";
                case ".flac":
                    return "FLAC Audio File";
                //File Video
                case ".mp4":
                    return "MP4 File";
                case ".avi":
                    return "AVI File";
                case ".mkv":
                    return "Matroska Video File";
                //File Nén
                case ".zip":
                    return "Compressed (zipped) Folder";
                case ".rar":
                    return "WinRAR archive";
                case ".7z":
                    return "7-Zip Filet";
                //File Thực Thi

                case ".exe":
                    return "Application";
                case ".msi":
                    return "Windows Installer Package";
                case ".bat":
                    return "Windows Batch File";
                case ".py":
                    return "Python File";
                case ".js":
                    return "JavaScript File";
                case ".cpp":
                    return "C++ Source File";
                case ".html":
                    return "HTML Document";


                case ".lnk":
                    return "Shortcut";

                case ".rdp":
                    return "Remote Desktop Connection";
                case ".htm":
                    return "CocCoc HTML Document";
                case ".reg":
                    return "Registration Entries";


            }
            return "";
        }




        static string FormatSize(long bytes)
        {
            const int scale = 1024;
            string[] orders = new string[] { "GB", "MB", "KB", "B" };
            long max = (long)Math.Pow(scale, orders.Length - 1);

            foreach (string order in orders)
            {
                if (bytes > max)
                    return string.Format("{0:##.##} {1}", decimal.Divide(bytes, max), order);
                max /= scale;
            }
            return "0 B";
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < pathHistory.Count; i++)
            {
                if (pathHistory[i] == temp)
                {
                    DSThuMucFile(pathHistory[i - 1]);
                    temp = pathHistory[i - 1];
                    textBox1.Text = temp;
                    break;
                }

            }

        }
        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < pathHistory.Count - 1; i++)
            {
                if (pathHistory[i] == temp)
                {
                    DSThuMucFile(pathHistory[i + 1]);
                    temp = pathHistory[i + 1];
                    textBox1.Text = temp;
                    break;
                }

            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem lv1 = listView1.SelectedItems[0];
                string name = lv1.SubItems[0].Text;
                string[] file = Directory.GetFiles(temp);
                string[] folder = Directory.GetDirectories(temp);
                foreach (string file2 in file)
                {
                    if (name == Path.GetFileNameWithoutExtension(file2))
                    {
                        sourceFilePath = file2;
                        FileInfo fileInfo = new FileInfo(file2);
                         fileInfo.Attributes |= FileAttributes.Hidden;
                        DSThuMucFile(temp);
                        
                    }
                }
                
            }
            cut = true;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem lv1 = listView1.SelectedItems[0];
                string name = lv1.SubItems[0].Text;
                string[] file = Directory.GetFiles(temp);
                foreach (string file2 in file)
                {
                    if (name == Path.GetFileNameWithoutExtension(file2))
                    {
                        switch (Path.GetExtension(file2))
                        {
                            case ".txt": case ".log": Process.Start("notepad.exe", Path.GetFullPath(file2)); break;
                            case ".mp4":
                            case ".avi":
                            case ".mkv":
                            case ".mp3":
                            case ".wav":
                            case ".aac":
                            case ".png":
                            case ".ico":
                            case ".jpg": Process.Start("explorer.exe", Path.GetFullPath(file2)); break;

                            case ".zip": case ".rar": case ".7z": Process.Start("WinRAR.exe", Path.GetFullPath(file2)); break;


                            case ".html": case ".htm": Process.Start("chrome.exe", Path.GetFullPath(file2)); break;
                            case ".xlsx":
                            case ".xls":
                            case ".xlsm":
                            case ".xltx":
                                ProcessStartInfo startInfo = new ProcessStartInfo
                                {
                                    FileName = "EXCEL.EXE",
                                    Arguments = "\"" + Path.GetFullPath(file2) + "\"",
                                    UseShellExecute = true
                                };
                                Process.Start(startInfo);
                                break;

                            case ".ppt":
                            case ".pptx":
                            case ".pps":
                            case ".ppsx":
                                ProcessStartInfo startInfo1 = new ProcessStartInfo
                                {
                                    FileName = "POWERPNT.EXE",
                                    Arguments = "\"" + Path.GetFullPath(file2) + "\"",
                                    UseShellExecute = true
                                };
                                Process.Start(startInfo1);
                                break;
                            case ".docx":
                            case ".doc":
                                ProcessStartInfo startInfo2 = new ProcessStartInfo
                                {
                                    FileName = "WINWORD.EXE",
                                    Arguments = "\"" + Path.GetFullPath(file2) + "\"",
                                    UseShellExecute = true
                                };
                                Process.Start(startInfo2);
                                break;
                            case ".bmp":
                            case ".gif":
                                Process.Start("mspaint.exe", Path.GetFullPath(file2)); break;
                            case ".css":
                            case ".js":
                            case ".py":
                            case ".cpp":
                            case ".java":
                            case ".c":
                                ProcessStartInfo startInfo3 = new ProcessStartInfo
                                {
                                    FileName = "Code.exe",
                                    Arguments = "\"" + Path.GetFullPath(file2) + "\"",
                                    UseShellExecute = true
                                };
                                Process.Start(startInfo3);
                                break;

                        }
                    }


                }
                string[] folder = Directory.GetDirectories(temp);
                foreach (string folder1 in folder)
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(folder1);
                    if (name == directoryInfo.Name)
                    {
                        DSThuMucFile(folder1);
                        temp = folder1;

                        pathHistory.Add(temp);
                        textBox1.Text = temp;
                    }
                }

            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }





        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {


            if (!Directory.Exists(temp + "//New folder"))
            {
                DirectoryInfo newfolder = new DirectoryInfo(temp + "//New folder");
                newfolder.Create();
            }
            else
            {
                DirectoryInfo newfolder = new DirectoryInfo(temp + "//New folder" + " (" + count + ")");
                count++;
                newfolder.Create();

            }

            DSThuMucFile(temp);
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem lv1 = listView1.SelectedItems[0];
                string name = lv1.SubItems[0].Text;
                string[] file = Directory.GetFiles(temp);
                string[] folder = Directory.GetDirectories(temp);
                foreach (string file2 in file)
                {
                    if (name == Path.GetFileNameWithoutExtension(file2))
                    {
                        sourceFilePath = file2;
                    }
      
                }
            }
        }

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem lv1 = listView1.SelectedItems[0];
                string name = lv1.SubItems[0].Text;
                string[] file = Directory.GetFiles(temp);
                string[] folder = Directory.GetDirectories(temp);
                foreach (string file2 in file)
                {
                    if (name == Path.GetFileNameWithoutExtension(file2))
                    {
                        File.Delete(file2);
                        DSThuMucFile(temp);
                    }
                }
                foreach (string folder1 in folder)
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(folder1);
                    if (name == directoryInfo.Name)
                    {
                        Directory.Delete(folder1);
                        DSThuMucFile(temp);
                    }
                }
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File.Copy(sourceFilePath, Path.Combine(temp, Path.GetFileName(sourceFilePath)), overwrite: true);
            DSThuMucFile(temp);
            if(cut == true)
            {
                File.Delete(sourceFilePath);
            }
            }
        }
    }

