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
        public string ChooseDirectory(string currentdir)
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                if(currentdir != "")
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

        public void CreateFiles(string dir, string file)
        {
            try
            {
                //stores valid/invalid paths from the outline
                Queue<string> validPaths = new Queue<string>();
                Queue<string> invalidPaths = new Queue<string>();
                Queue<string> invalidMessages = new Queue<string>();
                //Used to read from the file
                string line = "";
                //Used to keep track of the current folder level
                int sublevel = 0;
                int newlevel = 0;
                //Used to provide comprehensive error checking
                int linenum = 0;
                //Used to create filepaths
                string[] newdir = new string[20];

                //Reads file
                using (StreamReader sr = new StreamReader(file))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        linenum++;
                        newlevel = line.LastIndexOf("\t") + 1;
                        //if (newdir[sublevel] != null)//newlevel <= sublevel && 
                        //{
                        //    try
                        //    {
                        //        Path.GetFullPath(newdir[sublevel]);
                        //        validPaths.Enqueue(newdir[sublevel]);
                        //    }
                        //    catch (Exception)
                        //    {
                        //        invalidPaths.Enqueue(newdir[sublevel]);
                        //        invalidMessages.Enqueue(newdir[sublevel] + " on line " + linenum);
                        //    }

                        //}
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
                            invalidPaths.Enqueue(newdir[newlevel]);
                            invalidMessages.Enqueue(newdir[newlevel] + " on line " + linenum);
                        }
                        
                    }
                    //if (Path.GetFullPath(newdir[sublevel]) != null)
                    //{
                    //    validPaths.Enqueue(newdir[sublevel]);
                    //}
                    //else
                    //{
                    //    invalidPaths.Enqueue(newdir[sublevel]);
                    //}
                }

                //Makes Directories or notifies
                if (invalidPaths.Count == 0)
                {
                    //Create Files
                    //while (validPaths.Count > 0)
                    //{
                    //    Directory.CreateDirectory(validPaths.Dequeue());
                    //}
                    MessageBox.Show("Files Created Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    //Echo Error Lines
                    string invalid = "Incorrect Filepath(s)\n";

                    while (invalidMessages.Count > 0)
                    {
                        invalid += invalidMessages.Dequeue() + "\n";
                    }

                    MessageBox.Show(invalid, "Invalid Filepath Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
