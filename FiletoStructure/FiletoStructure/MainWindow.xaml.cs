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
        bool DirectoryFlag, OutlineFlag;

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                imgWarning.Source = Imaging.CreateBitmapSourceFromHBitmap(SystemIcons.Warning.ToBitmap().GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                DirectoryFlag = false;
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
                if (!DirectoryFlag && txtbxDir.Text != "")
                {
                    errormessage += "Top level path not found" + Environment.NewLine;
                    imgWarning.Visibility = Visibility.Visible;
                    scrError.Visibility = Visibility.Visible;
                    btnMakeDir.IsEnabled = false;
                }
                if (!OutlineFlag && txtbxFile.Text != "")
                {
                    errormessage += "Outline file not found" + Environment.NewLine;
                    scrError.Visibility = Visibility.Visible;
                    imgWarning.Visibility = Visibility.Visible;
                    btnMakeDir.IsEnabled = false;
                }
                if (DirectoryFlag && txtbxDir.Text != "" && OutlineFlag && txtbxFile.Text != "")
                {
                    try
                    {
                        Directory.Exists(txtbxDir.Text);
                        File.Exists(txtbxFile.Text);
                        if (!mwl.CheckOutline(txtbxDir.Text, txtbxFile.Text))
                        {
                            errormessage += "Unable to parse file" + Environment.NewLine;
                            lblReplace.Visibility = Visibility.Visible;
                            scrError.Visibility = Visibility.Visible;
                            imgWarning.Visibility = Visibility.Visible;
                            btnShowMe.Visibility = Visibility.Visible;
                            cbReplace.Visibility = Visibility.Visible;
                            btnMakeDir.IsEnabled = false;
                        }
                    }
                    catch (Exception)
                    {
                    }
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
                txtbxFile.Text = mwl.ChooseFile();
                txtbxFile.Focus();
                btnMakeDir.Focus();
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
                if (DirectoryFlag && OutlineFlag && txtbxDir.Text != "" && txtbxFile.Text != "")
                {
                    //mwl.CheckOutline(txtbxDir.Text, txtbxFile.Text);
                    mwl.CreateFiles();
                    Process.Start(txtbxDir.Text);
                    //this.Close();
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
            //try
            //{
            //    if (!Directory.Exists(txtbxDir.Text))
            //    {
            //        ShowFolderWarning();
            //        DirectoryFlag = false;
            //    }
            //    else
            //    {
            //        HideWarnings();
            //        DirectoryFlag = true;
            //    }
            //    if (!OutlineFlag && txtbxFile.Text != "")
            //    {
            //        ShowOutlineWarning();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Something went wrong!\n" + ex.ToString());
            //}
        }

        private void TxtbxFile_TextChanged(object sender, TextChangedEventArgs e)
        {
            //try
            //{
            //    if (!File.Exists(txtbxFile.Text))
            //    {
            //        ShowOutlineWarning();
            //        OutlineFlag = false;
            //    }
            //    else
            //    {
            //        HideWarnings();
            //        OutlineFlag = true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Something went wrong!\n" + ex.ToString());
            //}
        }

        private void TxtbxDir_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtbxDir.Text != "")
                {
                    if (!Directory.Exists(txtbxDir.Text))
                    {
                        DirectoryFlag = false;
                        RefreshWarnings();
                    }
                    else
                    {
                        DirectoryFlag = true;
                        RefreshWarnings();
                    }
                }
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
                DirectoryFlag = true;
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
                OutlineFlag = true;
                RefreshWarnings();
                try
                {
                    Directory.Exists(txtbxDir.Text);
                    File.Exists(txtbxFile.Text);
                    if (true)
                    {
                        mwl.CheckOutline(txtbxDir.Text, txtbxFile.Text);
                    }
                }
                catch (Exception)
                {
                }
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
                if (txtbxFile.Text != "")
                {
                    if (!File.Exists(txtbxFile.Text))
                    {
                        OutlineFlag = false;
                        RefreshWarnings();
                    }
                    else
                    {
                        OutlineFlag = true;
                        RefreshWarnings();
                    }
                }
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
