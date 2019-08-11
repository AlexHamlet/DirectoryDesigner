using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace FiletoStructure
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnDir_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            txtbxDir.Text = fbd.SelectedPath;
        }

        private void BtnFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            txtbxFile.Text = ofd.FileName;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(txtbxDir.Text != "" && txtbxFile.Text != "")
            {
                CreateFiles(txtbxDir.Text, txtbxFile.Text);
            }
        }

        private void CreateFiles(string dir, string file)
        {
            try
            {
                string line = "";
                int sublevel = 0;
                int newlevel = 0;
                string[] newdir = new string[20];
                using (StreamReader sr = new StreamReader(file))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        newlevel = line.LastIndexOf("\t") + 1;
                        if (newlevel <= sublevel && newdir[sublevel] != null)
                        {
                            Directory.CreateDirectory(newdir[sublevel]);
                        }
                        if (newlevel > 0)
                        {
                            newdir[newlevel] = Path.Combine(newdir[newlevel - 1], line.Remove(0, newlevel));
                        }
                        else
                        {
                            newdir[newlevel] = Path.Combine(dir, line.Remove(0, newlevel));
                        }
                        sublevel = newlevel;
                    }
                    Directory.CreateDirectory(newdir[newlevel]);
                }
            }
            catch (Exception)
            {

                throw;
            }
            //System.IO.Directory.CreateDirectory(dir);
        }
    }
}
