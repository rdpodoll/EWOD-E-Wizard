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
    public partial class uxEwod : Form
    {
        private List<Button> buttons = new List<Button>();
        private bool[,] on = new bool[21, 28];
        private Button[,] buttonMap = new Button[21, 28];
        private int _row;
        private int _col;
        private bool _locked = false;
        private int _rightCount = 0;
        private int _leftCount = 0;
        private int _state = 0;
        private System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
        // Bits shifted into the HV507
        private int[,] ZeroArray = {
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},  // Electrodes 1-16
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},  // Electrodes 17-32
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},  // Electrodes 33-48
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},  // Electrodes 49-64
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},  // Electrodes 65-80
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},  // Electrodes 81-96
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},  // Electrodes 97-112
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}}; // Electrodes 113-128

// Electrode locater for previous array
    private int[,] NumberArray = {
        {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16},
        {17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32},
        {33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48},
        {49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64},
        {65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80},
        {81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96},
        {97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112},
        {113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 123, 124, 125, 126, 127, 128}};

// PCB electrode layout
    private int[,] ChipArray = {
        {17, 0, 0, 17, 0, 0, 17, 0, 0, 25, 0, 0, 25, 0, 0, 18, 0, 0, 18, 0, 0, 26, 0, 0, 26, 0, 0, 26},
        {20, 0, 0, 21, 0, 0, 22, 0, 0, 23, 0, 0, 24, 0, 0, 60, 0, 0, 61, 0, 0, 62, 0, 0, 63, 0, 0, 64},
        {29, 0, 0, 30, 0, 0, 31, 0, 0, 32, 0, 0, 28, 0, 0, 33, 0, 0, 38, 0, 0, 46, 0, 0, 51, 0, 0, 52},
        {1, 0, 0, 1, 0, 0, 5, 0, 0, 5, 0, 0, 27, 0, 0, 27, 0, 0, 45, 0, 0, 45, 0, 0, 53, 0, 0, 53},
        {9, 0, 0, 9, 0, 0, 13, 0, 0, 13, 0, 0, 19, 0, 0, 19, 0, 0, 37, 0, 0, 37, 0, 0, 54, 0, 0, 54},
        {10, 125, 123, 10, 115, 124, 14, 125, 123, 14, 115, 124, 41, 125, 123, 41, 115, 124, 44, 125, 123, 44, 115, 124, 55, 125, 123, 55},
        {2, 0, 0, 2, 0, 0, 6, 0, 0, 6, 0, 0, 34, 0, 0, 34, 0, 0, 47, 0, 0, 47, 0, 0, 56, 0, 0, 56},
        {11, 0, 0, 11, 0, 0, 15, 0, 0, 15, 0, 0, 42, 0, 0, 42, 0, 0, 39, 0, 0, 39, 0, 0, 59, 0, 0, 59},
        {3, 0, 0, 3, 0, 0, 7, 0, 0, 7, 0, 0, 35, 0, 0, 35, 0, 0, 40, 0, 0, 40, 0, 0, 50, 0, 0, 50},
        {12, 0, 0, 12, 0, 0, 16, 0, 0, 16, 0, 0, 43, 0, 0, 43, 0, 0, 48, 0, 0, 48, 0, 0, 58, 0, 0, 58},
        {4, 125, 123, 4, 115, 124, 8, 125, 123, 8, 115, 124, 36, 125, 123, 36, 115, 124, 57, 125, 123, 57, 115, 124, 49, 125, 123, 49},
        {101, 0, 0, 101, 0, 0, 97, 0, 0, 97, 0, 0, 92, 0, 0, 92, 0, 0, 71, 0, 0, 71, 0, 0, 65, 0, 0, 65},
        {110, 0, 0, 110, 0, 0, 105, 0, 0, 105, 0, 0, 85, 0, 0, 85, 0, 0, 72, 0, 0, 72, 0, 0, 66, 0, 0, 66},
        {102, 0, 0, 102, 0, 0, 98, 0, 0, 98, 0, 0, 93, 0, 0, 93, 0, 0, 89, 0, 0, 89, 0, 0, 67, 0, 0, 67},
        {111, 0, 0, 111, 0, 0, 106, 0, 0, 106, 0, 0, 86, 0, 0, 86, 0, 0, 90, 0, 0, 90, 0, 0, 68, 0, 0, 68},
        {103, 125, 123, 103, 115, 124, 99, 125, 123, 99, 115, 124, 94, 125, 123, 94, 115, 124, 83, 125, 123, 83, 115, 124, 69, 125, 123, 69},
        {112, 0, 0, 112, 0, 0, 107, 0, 0, 107, 0, 0, 87, 0, 0, 87, 0, 0, 91, 0, 0, 91, 0, 0, 70, 0, 0, 70},
        {104, 0, 0, 104, 0, 0, 100, 0, 0, 100, 0, 0, 95, 0, 0, 95, 0, 0, 84, 0, 0, 84, 0, 0, 78, 0, 0, 78},
        {127, 0, 0, 126, 0, 0, 109, 0, 0, 108, 0, 0, 96, 0, 0, 88, 0, 0, 80, 0, 0, 77, 0, 0, 75, 0, 0, 73},
        {128, 0, 0, 119, 0, 0, 118, 0, 0, 117, 0, 0, 116, 0, 0, 82, 0, 0, 81, 0, 0, 79, 0, 0, 76, 0, 0, 74},
        {114, 0, 0, 114, 0, 0, 114, 0, 0, 122, 0, 0, 122, 0, 0, 113, 0, 0, 113, 0, 0, 121, 0, 0, 121, 0, 0, 121}};

        public uxEwod()
        {
            InitializeComponent();

            ArduinoCOM f = new ArduinoCOM();
            f.ShowDialog();
            int val = f.getCOM();
            serialPort1.PortName = "COM" + val.ToString();
            serialPort1.Open();
            t.Interval = 500; // specify interval time as you want
            t.Tick += new EventHandler(timer_Tick);
            foreach (Button button in Controls)
            {
                button.Enabled = true;
                button.AutoSize = true;
                button.Click += ButtonClick;
                buttons.Add(button);
            }
            int count = 0;
            for (int i = 0; i < 21; i++)
            {
                for (int j = 0; j < 28; j++)
                {
                    if ((i % 5 == 0 && i != 0 && i != 20) || j % 3 == 0)
                    {
                        on[i, j] = false;
                        buttonMap[i, j] = buttons[count];
                        count++;
                    }
                    else
                    {
                        on[i, j] = true;

                    }
                }
            }

            uxUp.Enabled = false;
            uxUp.Click -= ButtonClick;

            uxLeft.Enabled = false;
            uxLeft.Click -= ButtonClick;

            uxRight.Enabled = false;
            uxRight.Click -= ButtonClick;

            uxDown.Enabled = false;
            uxDown.Click -= ButtonClick;

            uxLock.Enabled = false;
            uxLock.Click -= ButtonClick;

            uxUnlock.Enabled = false;
            uxUnlock.Click -= ButtonClick;

            uxPath.Enabled = false;
            uxPath.Click -= ButtonClick;
        }
        /*
        private void uxEwod_Load(object sender, EventArgs e)
        {

        }
        */
        public void ButtonClick(object sender, EventArgs e)
        {
            uxUp.Enabled = true;
            uxLeft.Enabled = true;
            uxRight.Enabled = true;
            uxDown.Enabled = true;
            uxLock.Enabled = true;
            uxPath.Enabled = true;
            Button clickedButton = sender as Button;
            if (!(_locked))
            {
                foreach (Button button in Controls)
                {
                    button.BackColor = Color.White;
                    button.Enabled = true;
                }
                
            }
            on[_row, _col] = false;
            //here you can check which button was clicked by the sender
            //clickedButton.Enabled = false;
            
            clickedButton.BackColor = Color.Blue;

            for (int i = 0; i < 21; i++)
            {
                for (int j = 0; j < 28; j++)
                {
                    if (on[i, j] == false)
                    {
                        if (clickedButton.Handle.Equals(buttonMap[i, j].Handle))
                        {
                            _row = i;
                            _col = j;
                            if (_locked)
                            {
                                OnElectrode(clickedButton);
                            }
                            else
                            {
                                ElectrodeReset();
                                OnElectrode(clickedButton);
                            }
                            serialPort1.Write(BuildSend());
                        }
                    }
                }
            }
            on[_row, _col] = true;
        }

        public string BuildSend()
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    sb.Append(ZeroArray[i, j].ToString());
                }
            }
            return sb.ToString();
        }

        public void OnElectrode(Button clickedButton)
        {
            int val = ChipArray[_row, _col];
            int row = 0;
            int col = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    if(val == NumberArray[i, j])
                    {
                        row = i;
                        col = j;
                    }
                }
            }
            if (ZeroArray[row, col] == 1)
            {
                ZeroArray[row, col] = 0;
                clickedButton.BackColor = Color.White;
            }
            else
            {
                ZeroArray[row, col] = 1;
            }
        }

        public void ElectrodeReset()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    ZeroArray[i, j] = 0;
                }
            }
        }
        public void Move(int row, int col, int dir)
        {
            if (dir == 0)
            {
                if (row != 0)
                {
                    if (on[row - 1, col] != true)
                    {
                        on[_row, _col] = false;
                        _row = row - 1;

                        if (!(_locked))
                        {
                            foreach (Button button in Controls)
                            {
                                button.BackColor = Color.White;
                                button.Enabled = true;
                            }
                        }

                        //buttonMap[_row, _col].Enabled = false;
                        buttonMap[_row, _col].BackColor = Color.Blue;
                        on[_row, _col] = true;
                        if (_locked)
                        {
                            OnElectrode(buttonMap[_row, _col]);
                        }
                        else
                        {
                            ElectrodeReset();
                            OnElectrode(buttonMap[_row, _col]);
                        }
                        serialPort1.Write(BuildSend());
                    }
                }
            }
            else if (dir == 1)
            {
                if (col != 27)
                {
                    if (on[row, col + 1] != true)
                    {
                        on[_row, _col] = false;
                        _col = col + 1;

                        if (!(_locked))
                        {
                            foreach (Button button in Controls)
                            {
                                button.BackColor = Color.White;
                                button.Enabled = true;
                            }
                        }
                        //buttonMap[_row, _col].Enabled = false;
                        buttonMap[_row, _col].BackColor = Color.Blue;
                        on[_row, _col] = true;
                        if (_locked)
                        {
                            OnElectrode(buttonMap[_row, _col]);
                        }
                        else
                        {
                            ElectrodeReset();
                            OnElectrode(buttonMap[_row, _col]);
                        }
                        serialPort1.Write(BuildSend());
                    }
                }
            }
            else if (dir == 2)
            {
                if (row != 20)
                {
                    if (on[row + 1, col] != true)
                    {
                        on[_row, _col] = false;
                        _row = row + 1;
                        if (!(_locked))
                        {
                            foreach (Button button in Controls)
                            {
                                button.BackColor = Color.White;
                                button.Enabled = true;
                            }
                        }

                        //buttonMap[_row, _col].Enabled = false;
                        buttonMap[_row, _col].BackColor = Color.Blue;
                        on[_row, _col] = true;
                        if (_locked)
                        {
                            OnElectrode(buttonMap[_row, _col]);
                        }
                        else
                        {
                            ElectrodeReset();
                            OnElectrode(buttonMap[_row, _col]);
                        }
                        serialPort1.Write(BuildSend());
                    }
                }
            }
            else
            {
                if (col != 0)
                {
                    if (on[row, col - 1] != true)
                    {
                        on[_row, _col] = false;
                        _col = col - 1;
                        if (!(_locked))
                        {
                            foreach (Button button in Controls)
                            {
                                button.BackColor = Color.White;
                                button.Enabled = true;
                            }
                        }

                        //buttonMap[_row, _col].Enabled = false;
                        buttonMap[_row, _col].BackColor = Color.Blue;
                        on[_row, _col] = true;
                        if (_locked)
                        {
                            OnElectrode(buttonMap[_row, _col]);
                        }
                        else
                        {
                            ElectrodeReset();
                            OnElectrode(buttonMap[_row, _col]);
                        }
                        serialPort1.Write(BuildSend());
                    }
                }
            }
        }

        private void uxUp_Click(object sender, EventArgs e)
        {
            Move(_row, _col, 0);
        }

        private void uxRight_Click(object sender, EventArgs e)
        {
            Move(_row, _col, 1);
        }

        private void uxDown_Click(object sender, EventArgs e)
        {
            Move(_row, _col, 2);
        }

        private void uxLeft_Click(object sender, EventArgs e)
        {
            Move(_row, _col, 3);
        }

        private void uxLock_Click(object sender, EventArgs e)
        {
            uxUnlock.Enabled = true;
            uxLock.Enabled = false;
            _locked = true;
        }

        private void uxUnlock_Click(object sender, EventArgs e)
        {
            uxLock.Enabled = true;
            uxUnlock.Enabled = false;
            _locked = false;
        }

        private void uxPath_Click(object sender, EventArgs e)
        {
            uxLock.Enabled = true;
            uxUnlock.Enabled = false;
            _locked = false;

            t.Start();
            uxPath.Enabled = false;
        }
        void timer_Tick(object sender, EventArgs e)
        {
            //Call method
            if (_rightCount < 27)
            {
                Move(_row, _col, 1);
                _rightCount++;
            }
            else
            {
                Move(_row, _col, 3);
                _leftCount++;
            }
            if (_leftCount >= 27)
            {
                _leftCount = 0;
                _rightCount = 0;
                uxPath.Enabled = true;
                t.Stop();
            }
        }
    }
}
