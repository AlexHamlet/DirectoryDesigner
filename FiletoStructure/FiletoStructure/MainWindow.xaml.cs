using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        bool DirectoryFlag, FileFlag, OutlineFlag;

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                imgWarning.Source = Imaging.CreateBitmapSourceFromHBitmap(SystemIcons.Warning.ToBitmap().GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                DirectoryFlag = false;
                FileFlag = false;
                OutlineFlag = false;
                mwl = new MainWindowLogic();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong!\n" + ex.ToString());
            }

        }

        private void RefreshWarnings()
        {
            try
            {
                imgWarning.Visibility = Visibility.Hidden;
                cbReplace.Visibility = Visibility.Hidden;
                lblReplace.Visibility = Visibility.Hidden;
                btnShowMe.Visibility = Visibility.Hidden;
                scrError.Visibility = Visibility.Hidden;
                btnMakeDir.IsEnabled = true;
                string errormessage = "";
                if (!DirectoryFlag && !txtbxDir.IsFocused && txtbxDir.Text != "")
                {
                    errormessage += "Top level path not found" + Environment.NewLine;
                    imgWarning.Visibility = Visibility.Visible;
                    scrError.Visibility = Visibility.Visible;
                    btnMakeDir.IsEnabled = false;
                }
                if (!FileFlag && !txtbxFile.IsFocused && txtbxFile.Text != "")
                {
                    errormessage += "Outline file not found" + Environment.NewLine;
                    scrError.Visibility = Visibility.Visible;
                    imgWarning.Visibility = Visibility.Visible;
                    btnMakeDir.IsEnabled = false;
                }
                if (!OutlineFlag && !txtbxFile.IsFocused && txtbxFile.Text != "")
                {
                    errormessage += "Unable to parse file" + Environment.NewLine;
                    lblReplace.Visibility = Visibility.Visible;
                    scrError.Visibility = Visibility.Visible;
                    imgWarning.Visibility = Visibility.Visible;
                    btnShowMe.Visibility = Visibility.Visible;
                    cbReplace.Visibility = Visibility.Visible;
                    btnMakeDir.IsEnabled = false;
                }
                if(txtbxDir.Text == "" || txtbxFile.Text == "")
                {
                    btnMakeDir.IsEnabled = false;
                }
                scrError.Content = errormessage;
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
                string dir = mwl.ChooseDirectory(txtbxDir.Text);
                if (dir != "")
                {
                    txtbxDir.Text = dir;
                    txtbxDir.Focus();
                    btnFile.Focus();
                }
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
                string file = mwl.ChooseFile(txtbxFile.Text);
                if (file != "")
                {
                    txtbxFile.Text = file;
                    txtbxFile.Focus();
                    btnMakeDir.Focus();
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
                    DirectoryFlag = false;
                    //btnMakeDir.IsEnabled = false;
                }
                else
                {
                    DirectoryFlag = true;
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
                    FileFlag = false;
                    //btnMakeDir.IsEnabled = false;
                }
                else
                {
                    FileFlag = true;
                    if (DirectoryFlag)
                    {
                        OutlineFlag = mwl.CheckOutline(txtbxDir.Text, txtbxFile.Text);
                        lblPath.Text = "Longest new Path is "+mwl.longestpath+" characters long.";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong!\n" + ex.ToString());
            }
        }

        private void TxtbxDir_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                RefreshWarnings();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong!\n" + ex.ToString());
            }
        }

        private void TxtbxDir_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                RefreshWarnings();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong!\n" + ex.ToString());
            }
        }

        private void TxtbxFile_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                RefreshWarnings();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong!\n" + ex.ToString());
            }
        }

        private void TxtbxFile_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                RefreshWarnings();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong!\n" + ex.ToString());
            }
        }

        private void BtnShowMe_Click(object sender, RoutedEventArgs e)
        {
            mwl.ShowMe();
        }

        private void BtnMakeDir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mwl.CreateFiles();
                Process.Start(txtbxDir.Text);
                //this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong!\n" + ex.ToString());
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
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
