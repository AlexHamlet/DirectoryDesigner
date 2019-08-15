using System;
using System.Collections.Generic;
using System.Drawing;
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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessageBox = System.Windows.Forms.MessageBox;
using Path = System.IO.Path;

namespace FiletoStructure
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        MainWindowLogic mwl;
        bool DirectoryFlag, OutlineFlag;

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                imgFolderWarning.Source = Imaging.CreateBitmapSourceFromHBitmap(SystemIcons.Warning.ToBitmap().GetHbitmap(), IntPtr.Zero, Int32Rect.Empty,BitmapSizeOptions.FromEmptyOptions());
                imgOutlineWarning.Source = Imaging.CreateBitmapSourceFromHBitmap(SystemIcons.Warning.ToBitmap().GetHbitmap(), IntPtr.Zero, Int32Rect.Empty,BitmapSizeOptions.FromEmptyOptions());
                HideWarnings();
                DirectoryFlag = false;
                OutlineFlag = false;
                mwl = new MainWindowLogic();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong!\n" + ex.ToString());
            }
            
        }

        private void HideWarnings()
        {
            try
            {
                imgFolderWarning.Visibility = Visibility.Hidden;
                lblFolderWarning.Visibility = Visibility.Hidden;
                imgOutlineWarning.Visibility = Visibility.Hidden;
                lblOutlineWarning.Visibility = Visibility.Hidden;
                cbReplace.Visibility = Visibility.Hidden;
                lblReplace.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong!\n" + ex.ToString());
            }
        }

        private void ShowFolderWarning()
        {
            try
            {
                imgFolderWarning.Visibility = Visibility.Visible;
                lblFolderWarning.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong!\n" + ex.ToString());
            }
        }

        private void ShowOutlineWarning()
        {
            try
            {
                imgOutlineWarning.Visibility = Visibility.Visible;
                lblOutlineWarning.Visibility = Visibility.Visible;
                cbReplace.Visibility = Visibility.Visible;
                lblReplace.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong!\n" + ex.ToString());
            }
        }

        private void BtnDir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtbxDir.Text = mwl.ChooseDirectory();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong!\n" + ex.ToString());
            }
            
        }

        private void BtnFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtbxFile.Text = mwl.ChooseFile();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong!\n" + ex.ToString());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DirectoryFlag && OutlineFlag)
                {
                    //mwl.CreateFiles(txtbxDir.Text, txtbxFile.Text);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("You must specify a valid Folder and Outline before creating directories!", "Invalid Field(s)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong!\n" + ex.ToString());
            }
            
        }

        private void TxtbxDir_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (!Directory.Exists(txtbxDir.Text))
                {
                    ShowFolderWarning();
                    DirectoryFlag = false;
                }
                else
                {
                    HideWarnings();
                    DirectoryFlag = true;
                }
                if (!OutlineFlag && txtbxFile.Text != "")
                {
                    ShowOutlineWarning();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong!\n" + ex.ToString());
            }
        }

        private void TxtbxFile_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (!File.Exists(txtbxFile.Text))
                {
                    ShowOutlineWarning();
                    OutlineFlag = false;
                }
                else
                {
                    HideWarnings();
                    OutlineFlag = true;
                }
                if (!DirectoryFlag && txtbxDir.Text != "")
                {
                    ShowFolderWarning();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong!\n" + ex.ToString());
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong!\n" + ex.ToString());
            }
        }
    }
}
