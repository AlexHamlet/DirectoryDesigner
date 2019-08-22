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
        //Holds wether the outline is able to be fixed
        public bool creatable;
        //Holds a message about the outline
        public string outlineStatus;
        //stores valid/invalid paths from the outline
        private Queue<string> validPaths;
        private Queue<string> invalidPaths;
        private Queue<string> invalidMessages;

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindowLogic()
        {
            longestpath = 0;
            creatable = false;
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
        /// Allows the user to choose a file
        /// </summary>
        /// <returns>The filepath of the chosen file</returns>
        public string ChooseFile(string currentfile)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if (currentfile != "")
                {
                    try
                    {
                        Path.GetFullPath(currentfile);
                        ofd.InitialDirectory = currentfile;
                    }
                    catch (Exception)
                    {
                    }
                }
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
        /// <returns>Wether or not the Outline can be created</returns>
        public Boolean CheckOutline(string dir, string file)
        {
            try
            {
                validPaths.Clear();
                invalidPaths.Clear();
                invalidMessages.Clear();
                //Used to read from the file
                string line = "";
                outlineStatus = "";
                //Used to keep track of the current folder level
                int sublevel = 0;
                int newlevel = 0;
                //Used to provide comprehensive error checking
                int linenum = 0;
                longestpath = 0;
                //Used to create filepaths
                string[] newdir = new string[20];
                bool invalidchar = false;
                //Holds return value
                bool retval = true;

                //Reads file
                using (StreamReader sr = new StreamReader(file))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        try
                        {
                            //Error Checking
                            linenum++;

                            //Ignore blank lines
                            if (line.Replace("\t", "") == "")
                            {
                                continue;
                            }
                            //Get subdirectory level
                            int index = 0;
                            if (line.Length > 1)
                            {
                                string indexgrabber = line;
                                while (indexgrabber.ElementAt(0).ToString() == "\t")
                                {
                                    indexgrabber = indexgrabber.Remove(0, 1);
                                    index++;
                                }
                            }
                            //Ensures that this directory has a parent directory
                            if (index > sublevel + 1)
                            {
                                throw new InvalidDataException("("+linenum+")"+"Missing folder level");
                            }
                            newlevel = index;

                            if (line.Contains("<") || line.Contains(">") || line.Contains(":") || line.Contains("\"") ||
                                line.Contains("/") || line.Contains("\\") || line.Contains("|") || line.Contains("?") || line.Contains("*"))
                            {
                                line = FixPath(line);
                                invalidchar = true;
                            }
                            else
                            {
                                invalidchar = false;
                            }
                            if (newlevel > 0)
                            {
                                //newdir[newlevel] = Path.Combine(newdir[newlevel - 1], line.Remove(0, newlevel));
                                newdir[newlevel] = Path.Combine(newdir[newlevel - 1], line.Replace("\t", ""));
                            }
                            else
                            {
                                //newdir[newlevel] = Path.Combine(dir, line.Remove(0, newlevel));
                                newdir[newlevel] = Path.Combine(dir, line.Replace("/t", ""));
                            }
                            if (invalidchar)
                            {
                                throw new Exception("Invalid Filepath");
                            }
                            if (newdir[newlevel].Length > longestpath)
                            {
                                longestpath = newdir[newlevel].Length;
                            }
                            validPaths.Enqueue(newdir[newlevel]);
                            sublevel = newlevel;
                        }
                        catch (InvalidDataException ex)
                        {
                            outlineStatus = "Missing Parent Directories";
                            invalidMessages.Enqueue(ex.Message);
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
                if (outlineStatus == "")
                {
                    string[] paths = validPaths.Union(invalidPaths).ToArray();
                    foreach (string s in paths)
                    {
                        Directory.CreateDirectory(s);
                    }
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
                string[] im = invalidMessages.ToArray();
                foreach (string s in im)
                {
                    invalid += s + "\n";
                }

                MessageBox.Show(invalid, "Invalid Filepath Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return invalid;
            }
            return "";
        }

        public string FixPath(string pathfixer)
        {
            //TODO: Exclude <, >, :, ", /, \, |, ?, *
            try
            {
                pathfixer = pathfixer.Replace("<", "_");
                pathfixer = pathfixer.Replace(">", "_");
                pathfixer = pathfixer.Replace(":", "_");
                pathfixer = pathfixer.Replace("\"", "_");
                pathfixer = pathfixer.Replace("/", "_");
                pathfixer = pathfixer.Replace("\\", "_");
                pathfixer = pathfixer.Replace("|", "_");
                pathfixer = pathfixer.Replace("?", "_");
                pathfixer = pathfixer.Replace("*", "_");
                return pathfixer;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
