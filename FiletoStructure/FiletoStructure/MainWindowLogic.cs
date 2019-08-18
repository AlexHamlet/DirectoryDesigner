using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FiletoStructure
{
    class MainWindowLogic
    {
        //Holds the characterlength of the longest creatable filepath
        public int longestpath;
        //Holds a message about the outline
        public string OutlineStatus;
        //stores valid/invalid paths from the outline
        private Queue<string> validPaths { get; }
        private Queue<string> invalidPaths { get; }
        private Queue<string> invalidMessages { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindowLogic()
        {
            longestpath = 0;
            validPaths = new Queue<string>();
            invalidPaths = new Queue<string>();
            invalidMessages = new Queue<string>();
        }

        /// <summary>
        /// Allows user to pick a directory
        /// </summary>
        /// <param name="currentdir">The name of the directory you wish to start with</param>
        /// <returns>The chosen directory</returns>
        public string ChooseDirectory(string currentdir)
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                if (currentdir != "")
                {
                    try
                    {
                        Path.GetFullPath(currentdir);
                        fbd.SelectedPath = currentdir;
                    }
                    catch (Exception)
                    {
                    }
                }
                fbd.ShowDialog();
                return fbd.SelectedPath;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Allows the user to choose a directory
        /// </summary>
        /// <returns>The chosen directory</returns>
        public string ChooseDirectory()
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.ShowDialog();
                return fbd.SelectedPath;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Allows the user to choose a file
        /// </summary>
        /// <returns>The filepath of the chosen file</returns>
        public string ChooseFile()
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.ShowDialog();
                return ofd.FileName;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// Checks the outline for errors
        /// </summary>
        /// <param name="dir">The directory you want to create the folders in</param>
        /// <param name="file">The outline containing folder names</param>
        /// <returns></returns>
        public Boolean CheckOutline(string dir, string file)
        {
            try
            {
                //Used to read from the file
                string line = "";
                //Used to keep track of the current folder level
                int sublevel = 0;
                int newlevel = 0;
                //Used to provide comprehensive error checking
                int linenum = 0;
                //Used to create filepaths
                string[] newdir = new string[20];
                //Holds return value
                bool retval = true;

                //Reads file
                using (StreamReader sr = new StreamReader(file))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        //Error Checking
                        linenum++;
                        //Get subdirectory level
                        newlevel = line.LastIndexOf("\t") + 1;

                        try
                        {
                            if (newlevel > 0)
                            {
                                newdir[newlevel] = Path.Combine(newdir[newlevel - 1], line.Remove(0, newlevel));
                                Path.GetFullPath(newdir[newlevel]);
                                validPaths.Enqueue(newdir[newlevel]);
                            }
                            else
                            {
                                newdir[newlevel] = Path.Combine(dir, line.Remove(0, newlevel));
                                Path.GetFullPath(newdir[newlevel]);
                                validPaths.Enqueue(newdir[newlevel]);
                            }
                            sublevel = newlevel;
                        }
                        catch (Exception)
                        {
                            retval = false;
                            invalidPaths.Enqueue(newdir[newlevel]);
                            invalidMessages.Enqueue("(" + linenum + ")" + newdir[newlevel]);
                        }
                    }
                }
                return retval;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Creates valid directories from the outline
        /// </summary>
        public void CreateFiles()
        {
            try
            {
                while (validPaths.Count > 0)
                {
                    Directory.CreateDirectory(validPaths.Dequeue());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Provides a list of errors and where they occur
        /// </summary>
        /// <returns>A string of errors</returns>
        public string ShowMe()
        {
            //Makes Directories or notifies
            if (invalidPaths.Count != 0)
            {
                //Echo Error Lines
                string invalid = "Incorrect Filepath(s)\n";

                while (invalidMessages.Count > 0)
                {
                    invalid += invalidMessages.Dequeue() + "\n";
                }

                MessageBox.Show(invalid, "Invalid Filepath Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return invalid;
            }
            return "";
        }
    }
}
