using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO; // access FolderBrowser

namespace Week2
{
    public partial class Form1 : Form
    {
        string currentDir = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var fb = new FolderBrowserDialog();
                if(fb.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    currentDir = fb.SelectedPath; //get the selected folder by user;

                    textBoxDirectory.Text = currentDir; //giao dien

                    //get all image file form dic
                    var dirInfo = new DirectoryInfo(currentDir);

                    //get file
                    var files = dirInfo.GetFiles().Where(c => (c.Extension.Equals(".jpg") || c.Extension.Equals(".jpeg") || c.Extension.Equals(".bmp") || c.Extension.Equals(".png")));
                    foreach (var image in files)
                    {
                        //add ten anh vao list box
                        listBoxImages.Items.Add(image.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error: " + ex.Message + " " + ex.Source);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // lay anh duoc chon
                var selectedImage = listBoxImages.SelectedItems[0].ToString();
                if(!string.IsNullOrEmpty(selectedImage) && !string.IsNullOrEmpty(currentDir))
                {
                    //make the full path to the image
                    var fullPath = Path.Combine(currentDir, selectedImage);

                    //set an image form file to the picturebox
                    pictureBoxImagePreview.Image = Image.FromFile(fullPath);
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
