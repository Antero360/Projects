using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ETM
{
    public partial class Form1 : Form
    {
        private bool resizeFlag = false;

        public Form1()
        {
            InitializeComponent();
            this.processView.SizeChanged += new EventHandler(ListView_SizeChange);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //resize the columns so that they have equal width
            /*
            foreach (ColumnHeader column in processView.Columns)
            {
                column.Width = (int)(processView.Width / 6);
            }
            */
            ShowAllProcesses();
        }

        /*
         * Summary: returns an Expando object representing a process
         * with description and owner of process using the process identification number
         */
        public ExpandoObject GetMoreInfo(int pid)
        {
            //create dynamic object
            dynamic process = new ExpandoObject();
            process.Description = "";
            process.Username = "Unknown";

            //get a list of processes from Win32_Process
            string query = String.Format("Select * From Win32_Process Where ProcessID = {0}",pid);
            ManagementObjectSearcher queryEngine = new ManagementObjectSearcher(query);
            ManagementObjectCollection processList = queryEngine.Get();

            //iterate through list and
            foreach (ManagementObject processObject in processList)
            {
                //get username
                string[] propertyList = new string[] {string.Empty, string.Empty};
                int responseValue = Convert.ToInt32(processObject.InvokeMethod("GetOwner", propertyList));
                if (responseValue == 0)
                {
                    process.Username = propertyList[0];
                }

                //get description if any
                if (processObject["ExecutablePath"] != null)
                {
                    try
                    {
                        FileVersionInfo info = FileVersionInfo.GetVersionInfo(processObject["ExecutablePath"].ToString());
                    }
                    catch
                    {
                    }
                }
            }
            return process;
        }

        /*
         * Summary: converts byte number to readable values
         */
        public string BytesToFormatValue(long originalValue)
        {
            //list of byte suffixes
            List<string> suffixList = new List<string> {" B", " KB", " MB", " GB", " TB", " PB"};

            for (int x = 0; x < suffixList.Count; x++)
            {
                long tempValue = (originalValue / ((int)Math.Pow(1024, (x + 1))));
                if (tempValue == 0)
                {
                    return (originalValue / ((int)Math.Pow(1024, x))) + suffixList[x];
                }
            }
            return originalValue.ToString();
        }

        /*
         * Summary: renders all processes running on Windows to a listview 
         */
        public void ShowAllProcesses()
        {
            //use an array to hold all running processes
            Process[] runningProcesses = Process.GetProcesses();

            //show total processes count
            //totalProcesses.Text = string.Format("{0} currently running.", runningProcesses.Length);
            int processCount = 0;

            //create a list to hold process icons
            ImageList processIcons = new ImageList();

            //iterate through all processes and show their information
            foreach (Process process in runningProcesses)
            {
                //find the status of the process, is it responding or not
                /*
                 * the ?: operator, or conditional operator, replaces the if-else conditional statement
                 * the format of this operator goes as follows:
                 * boolean expression ? (response if expression is true) : (response if expression is false)
                 */
                string processStatus = (process.Responding == true ? "Responding" : "Not Responding" );

                //get process extra information
                dynamic processExtraInfo = GetMoreInfo(process.Id);

                //create data source to bind to listview
                string[] newProcessRow = {
                    //col 1: process id
                    process.Id.ToString(),
                    //col 2: process name
                    process.ProcessName,
                    //col 3: process description
                    processExtraInfo.Description,
                    //col 4: process status
                    processStatus,
                    //col 5: process owner
                    processExtraInfo.Username,
                    //col 6: process memory usage
                    BytesToFormatValue(process.PrivateMemorySize64),
                    
                };

                //get process icon, if any
                try
                {
                    processIcons.Images.Add(
                        //unique identifier for the icon
                        process.Id.ToString(),
                        //get the icon and add to list
                        Icon.ExtractAssociatedIcon(process.MainModule.FileName).ToBitmap()
                    );
                }
                catch
                {
                }

                //create a new row of information for the listview
                ListViewItem displayRow = new ListViewItem(newProcessRow)
                {
                    //index the icon from icon list
                    ImageIndex = processIcons.Images.IndexOfKey(process.Id.ToString())
                };

                //add the row to the listview
                processCount++;
                processView.Items.Add(displayRow);
                totalProcesses.Text = string.Format("{0} currently running.", processCount);
            }

            //set the large and small image lists to icon list
            processView.LargeImageList = processIcons;
            processView.SmallImageList = processIcons;
        }

        private void ClearProcessList()
        {
            processView.Items.Clear();
            totalProcesses.Text = string.Format("{0} currently running.", 0);
        }

        private void ListView_SizeChange(object sender, EventArgs e)
        {
            //handles overlapping calls to SizeChange
            if (!resizeFlag)
            {
                //raise the flag
                resizeFlag = true;

                ListView proView = sender as ListView;
                if (proView != null)
                {
                    //get total sum of the width based on the column tag
                    float totalTagSum = 0;
                    for (int x = 0; x < proView.Columns.Count; x++)
                        totalTagSum += Convert.ToInt32(proView.Columns[x].Tag);

                    //calculate width of each column
                    for (int x = 0; x < proView.Columns.Count; x++)
                    {
                        float colWidthPercent = (Convert.ToInt32(proView.Columns[x].Tag) / totalTagSum);
                        proView.Columns[x].Width = (int)(colWidthPercent * proView.ClientRectangle.Width);
                    }
                }
            }

            //reset the resizing flag
            resizeFlag = false;
        }

        private void StartProcess_Click(object sender, EventArgs e)
        {
            //make sure that user input has correct format
            if (task2Run.Text.ToUpper().Contains(".EXE") == true)
            {
                try
                {
                    //start a new process
                    Process process = new Process();
                    process.StartInfo.FileName = task2Run.Text;
                    process.Start();

                    //clear current list and refresh
                    ClearProcessList();

                    MessageBox.Show("Process has been been initiated. Reloading list...");

                    //reload list
                    ShowAllProcesses();
                }
                catch (Exception error)
                {
                    MessageBox.Show(string.Format("ERROR! '{0}' is not a valid process.", task2Run.Text));
                }
            }
            else
            {

                MessageBox.Show(string.Format("ERROR! '{0}' is not an executable process. Please make sure to enter a process name with the extension '.exe'.", task2Run.Text));
            }
        }

        private void KillProcess_Click(object sender, EventArgs e)
        {
            //get the selected process
            ListViewItem current = processView.SelectedItems[0];
            Process process = Process.GetProcessById(int.Parse(current.Text.ToString()));
            
            //kill the selected process
            process.Kill();

            //clear list
            ClearProcessList();

            MessageBox.Show("Process has been killed successfully. Reloading list...");

            //reload list
            ShowAllProcesses();
        }
    }
}
