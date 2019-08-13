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
                Queue<string> validPaths = new Queue<string>();
                Queue<string> invalidPaths = new Queue<string>();
                string line = "";
                int sublevel = 0;
                int newlevel = 0;
                int linenum = 0;
                string[] newdir = new string[20];

                //Reads file
                using (StreamReader sr = new StreamReader(file))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        linenum++;
                        newlevel = line.LastIndexOf("\t") + 1;
                        if (newdir[sublevel] != null)//newlevel <= sublevel && 
                        {
                            try
                            {
                                Path.GetFullPath(newdir[sublevel]);
                                validPaths.Enqueue(newdir[sublevel]);
                            }
                            catch (Exception)
                            {
                                invalidPaths.Enqueue(newdir[sublevel] + " on line " + linenum);
                            }

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
                    while (validPaths.Count > 0)
                    {
                        Directory.CreateDirectory(validPaths.Dequeue());
                    }
                    MessageBox.Show("Files Created Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    //Echo Error Lines
                    string invalid = "Incorrect Filepath(s)\n";

                    while (invalidPaths.Count > 0)
                    {
                        invalid += invalidPaths.Dequeue() + "\n";
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
