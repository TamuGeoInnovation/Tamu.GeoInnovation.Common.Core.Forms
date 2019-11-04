using System;
using System.IO;
using System.Windows.Forms;
using USC.GISResearchLab.Common.Utils.Filenames;
using USC.GISResearchLab.Common.Utils.Files;

namespace USC.GISResearchLab.Common.Utils.FileBrowsers
{
    /// <summary>
    /// Summary description for FileBrowserUtils.
    /// </summary>
    public class FileBrowserUtils
    {
        public static string TYPE_SHAPEFILE = "Shapefile (*.shp)|*.shp";
        public static string TYPE_DATABASE = "Access Databases (*.mdb)|*.mdb";
        public static string TYPE_ACCESS_DATABASE_2007 = "Access 2007 Databases (*.accdb)|*.accdb";
        public static string TYPE_TEXT = "Text Files (*.txt)|*.txt";
        public static string TYPE_CSV = "CSV Files (*.csv)|*.csv";
        public static string TYPE_DBF = "DBF Tables (*.dbf)|*.dbf";
        public static string TYPE_ICON = "Icon Files(*.ico)|*.ico";
        public static string TYPE_KML = "KML Files(*.kml)|*.kml";
        public static string TYPE_XML = "XMLFiles(*.xml)|*.xml";

        public FileBrowserUtils()
        {
        }

        public static string[] browseForOpenFile(string initialDirectory, string[] fileTypes, bool includeAllFiles)
        {
            return browseForOpenFile(initialDirectory, fileTypes, includeAllFiles, 0);
        }

        public static string BrowseAndSelectSaveFile(string types, string title, string initialName)
        {
            return BrowseAndSelectSaveFile(types, title, initialName, true, 0);
        }

        public static string BrowseAndSelectSaveFile(string types, string title, string initialName, bool promptReplace)
        {
            return BrowseAndSelectSaveFile(types, title, initialName, promptReplace, 0);
        }

        public static string BrowseAndSelectSaveFile(string types, string title, string initialName, bool promptReplace, int maxLength)
        {
            string ret = "";
            try
            {
                SaveFileDialog saveDlg = new SaveFileDialog();
                saveDlg.CheckPathExists = true;
                saveDlg.Filter = types;
                saveDlg.OverwritePrompt = promptReplace;
                saveDlg.Title = title;
                saveDlg.RestoreDirectory = true;
                saveDlg.FileName = initialName;

                DialogResult dr = saveDlg.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    string fullFilename = saveDlg.FileName;
                    string directory = FileUtils.GetDirectoryPath(fullFilename);
                    string filename = FileUtils.GetFileNameWithoutExtension(fullFilename);
                    string ext = FileUtils.GetExtension(fullFilename);
                    if (maxLength > 0)
                    {
                        if (filename.Length > maxLength)
                        {
                            filename = FilenameUtils.TrimName(filename, maxLength, 0);
                        }
                    }
                    ret = directory + filename + ext;
                }
            }
            catch (Exception e)
            {
                throw new Exception("An error occured browsing and choosing filename", e);
            }
            return ret;
        }

        public static string BrowseAndSelectOpenFile(string title)
        {
            return BrowseAndSelectOpenFile("", title, "");
        }

        public static string BrowseAndSelectOpenFile(string types, string title)
        {
            return BrowseAndSelectOpenFile(types, title, "");
        }

        public static string BrowseAndSelectOpenFile(string types, string title, string initialName)
        {
            string ret = "";
            try
            {
                OpenFileDialog openDlg = new OpenFileDialog();
                openDlg.CheckPathExists = true;
                openDlg.Filter = types;
                openDlg.CheckFileExists = true;
                openDlg.Title = title;
                openDlg.RestoreDirectory = true;

                DialogResult dr = openDlg.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    ret = openDlg.FileName;
                }
            }
            catch (Exception e)
            {
                throw new Exception("An error occured browsing and choosing filename", e);
            }
            return ret;
        }

        public static string[] browseForOpenFile(string initialDirectory, string[] fileTypes, bool includeAllFiles, int filterIndex)
        {
            string[] ret = new string[2];
            ret[0] = "";
            ret[1] = "";

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = getFileTypesArrayList(fileTypes, includeAllFiles);
            dialog.InitialDirectory = initialDirectory;
            if (filterIndex >= 1 && filterIndex <= dialog.Filter.Length)
            {
                filterIndex++;
                dialog.FilterIndex = filterIndex;
            }

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ret[0] = dialog.FileName;
                ret[1] = (dialog.FilterIndex - 1).ToString();
            }

            return ret;
        }

        public static string[] browseForSaveFile(string initialDirectory, string[] fileTypes, bool includeAllFiles)
        {
            return browseForSaveFile(initialDirectory, fileTypes, includeAllFiles, 0);
        }

        public static string[] browseForSaveFile(string initialDirectory, string[] fileTypes, bool includeAllFiles, int filterIndex)
        {
            string[] ret = new string[2];
            ret[0] = "";
            ret[1] = "";

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = getFileTypesArrayList(fileTypes, includeAllFiles);
            dialog.InitialDirectory = initialDirectory;
            if (filterIndex >= 1 && filterIndex <= dialog.Filter.Length)
            {
                filterIndex++;
                dialog.FilterIndex = filterIndex;
            }

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ret[0] = dialog.FileName;
                ret[1] = (dialog.FilterIndex - 1).ToString();
            }

            return ret;
        }

        public static string BrowseForFolder(string initialDirectory)
        {
            string ret = "";

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (initialDirectory == null || initialDirectory.Equals(""))
            {
                dialog.SelectedPath = @"C:\";
            }
            else
            {
                FileInfo f = new FileInfo(initialDirectory);
                dialog.SelectedPath = f.DirectoryName;
            }

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ret = dialog.SelectedPath.ToString().Trim() + "\\";
            }

            return ret;
        }

        public static string getFileTypesArrayList(string[] values, bool includeAllFiles)
        {
            string ret = "";

            //"nmea files (*.nmea)|*.nmea|All files (*.*)|*.*";
            // or 
            //"nmea files:nmea|All files:*.*";

            if (values != null)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    if (i > 0)
                    {
                        ret += "|";
                    }

                    string current = values[i];
                    string display;

                    // the case of  "display:ext|display2:ext2"; - have to reformat it
                    if (current.IndexOf(':') >= 0)
                    {
                        string[] parts = current.Split(':');
                        string type = parts[0];
                        string ext = parts[1];
                        display = type + " (*." + ext + ")" + " |*." + ext;
                    }
                    // the case of "display (*.ext)|*.ext|display2 (*ext2)|*exz2"; - don't have to do anything
                    else
                    {
                        display = current;
                    }
                    ret += display;
                }
            }

            if (includeAllFiles)
            {
                if (!String.IsNullOrEmpty(ret))
                {
                    ret += "|";
                }
                ret += "All files (*.*)|*.*";
            }

            return ret;
        }
    }
}

