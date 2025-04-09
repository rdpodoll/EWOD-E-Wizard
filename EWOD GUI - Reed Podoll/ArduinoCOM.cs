//////////////////////////
///Author: Reed Podoll
///
///2024-2025 FANTasy Lab
///Kansas State University
///
/// This script allows the user to select which serial port
/// the arduino mega is connected to for the "EWOD E-Wizard" operations.
//////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EWOD_GUI___Reed_Podoll
{
    /// <summary>
    /// Runs the ArduinoCOM GUI
    /// </summary>
    public partial class ArduinoCOM : Form
    {
        private int COMNum;
        /// <summary>
        /// Initializes the GUI to appear
        /// </summary>
        public ArduinoCOM()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Gets called from EWOD.cs to receive the COM numebr
        /// </summary>
        /// <returns>COM number for serial port</returns>
        public int getCOM()
        {
            return COMNum;
        }

        /// <summary>
        /// Converts user input into the COM number, saves in system to be received by EWOD.cs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxEnter_Click(object sender, EventArgs e)
        {
            COMNum = Convert.ToInt32(uxCOM.Text);
            this.Close();
        }
    }
}
