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
    public partial class ArduinoCOM : Form
    {
        private int COMNum;
        public ArduinoCOM()
        {
            InitializeComponent();
        }

        public int getCOM()
        {
            return COMNum;
        }

        private void uxEnter_Click(object sender, EventArgs e)
        {
            COMNum = Convert.ToInt32(uxCOM.Text);
            this.Close();
        }
    }
}
