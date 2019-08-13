﻿using System;
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

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                mwl = new MainWindowLogic();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Search Window could not be opened\n" + ex.ToString());
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
                MessageBox.Show("Search Window could not be opened\n" + ex.ToString());
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
                MessageBox.Show("Search Window could not be opened\n" + ex.ToString());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtbxDir.Text != "" && txtbxFile.Text != "")
                {
                    mwl.CreateFiles(txtbxDir.Text, txtbxFile.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Search Window could not be opened\n" + ex.ToString());
            }
            
        }

    }
}