using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UcanDotNET;
using Joystik;
using System.Xml.Serialization;
using System.IO;

namespace MRO
{
    public partial class frmMain : Form
    {
        Device devPKP_PU1,
            devPKP_PU2,
            devPU_K,
            devPK_K,
            devPU_NR,
            devPK_NR,
            devPU_RD,
            devPK_RD,
            devBU,
            devUSO,
            devPKP_BU,
            devBroadCast,
            MTTD;

        Message 
            comCaptureDevice,
            comReleaseDevice,
            a1_35,

            a5_1,
            a5_2,
            a5_3,
            a5_4,
            a5_5,
            a5_6,
            a5_7,
            a5_8,
            a5_9,
            a5_10,
            a5_11,
            a5_12,
            a5_13,
            a5_14,
            a5_15,
            a5_16,
            a5_17,
            a5_18,
            a5_19,
            a5_20,
            a5_21,
            a5_22,
            a5_23,
            a5_24,
            a5_25,
            a5_26,
            a5_27,
            a5_28,
            a5_29,
            a5_30,
            a5_31,
            a5_32,
            a5_33,
            a5_34,
            a5_35,
            a5_36,
            a5_37,
            a5_38,
            a5_39,
            a5_40,
            a5_41,
            
            a6_1,
            a6_2,
            a6_3,
            a6_4,
            a6_5_0,
            a6_5_1,
            a6_5_2,
            a6_5_3,
            a6_5_4,
            a6_5_5,
            a6_6,
            a6_7,
            a6_8,
            a6_9,
            a6_10,
            a6_11,
            
            a7_3,

            a8_2,
            a8_3;

        List<Device> Devices = new List<Device>();  //список устройств, что мы имитируем
        List<Message> lstMessages = new List<Message>();

        USBcanServer CanSrv = new USBcanServer();
        MyJoystick myJoystick;
        Message lastMsg; //храним ссылку на последнюю отправленную команду

        string[] status_mas = { "/", "-", "\\", "|" };
        bool[] lastJoystickStateButtons = { false, false, false, false, false };
        byte index_status_mas = 0;
        public byte Index_status_mas
        {
            get
            {
                index_status_mas++;
                if (index_status_mas > status_mas.Length - 1) index_status_mas = 0;
                return index_status_mas;
            }
        }


        public frmLog _frmLog;
        public frmBU _frmBU;
        public frmOther _frmOther;
        public frmPU _frmPU;
        public frmDalnomer _frmDalnomer;
        public frmDebug _frmDebug;
        public frmSettings _frmSettings;
        public frmShutDown _frmShutDown;
        //public frmVideoSettings _frmVideoSet;

        byte bRet;

        double kHor = 0, kVert = 0; //коэффициенты масштабирования для использования джойстика
        Values valuesScrolls = new Values(); //в структуре храним значения нум-полей

        int selectedIndex = 0;
        byte cntOfAnglesRate = 0;

        public int iCnt1280 = 0, iCnt1280_ = 0, iCnt1280__ = 0;
        int cntFPSrate = 0;
        Multimedia.Timer tmrSend1540 = new Multimedia.Timer();
        bool flag_a5_3;

        private SettingXML sets;

        public SDI_Capture sdi;


        //Bitmap bitmap;
        //Graphics imgr;
        public frmMain()
        {
            InitializeComponent();
            Init();
        }

        void Init()
        {
            DeviceInit();
            MessageInit();
            sdi = new SDI_Capture(this);

            _frmLog = new frmLog();
            _frmBU = new frmBU();
            _frmOther = new frmOther();
            _frmPU = new frmPU();
            _frmDalnomer = new frmDalnomer();
            _frmDebug = new frmDebug();
            _frmSettings = new frmSettings(this);
            _frmShutDown = new frmShutDown();
            //_frmVideoSet = new frmVideoSettings();
            _frmLog._frmMain = _frmBU._frmMain = _frmOther._frmMain = _frmPU._frmMain = _frmDalnomer._frmMain = _frmDebug._frmMain  = _frmShutDown._frmMain = this;

            //pict1.Image = sdi.bitmap;
            sets = new SettingXML();

            sdi.eventFPS += sdi_eventFPS;
            sdi.eventFPS_FrameReceived += sdi_eventFPS_FrameReceived;
            sdi.updateBitmap += sdi_updateBitmap;

            LoadSettings();

            //cmbA5_5_MarkColor.SelectedIndex = 0;
            //cmbA5_5_MarkColor.SelectedItem = cmbA5_5_MarkColor.Items[0];

            tmrSendA5_1.Enabled = true; //стартуем таймер посылки запроса от выбранного абонента в ПКП-БУ

            tmrSend1540.Period = 20; //стартуем таймер посылки команды с дескриптором 1540 (Угол наведения и угловая скорость МТТД по горизонту) 
            tmrSend1540.Tick += tmrSend1540_Tick;
            tmrSend1540.Resolution = 1;
            tmrSend1540.Mode = Multimedia.TimerMode.Periodic;
            tmrSend1540.Start();

            //bitmap  = new Bitmap(1280, 1024, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            //imgr = Graphics.FromImage(bitmap);
        }

        void sdi_eventFPS_FrameReceived(double fps)
        {
            Invoke((MethodInvoker)delegate()
            {
                _frmSettings.lblFPS_Received.Text = "FPS_FrameReceived: " + String.Format("{0}", Math.Round(fps, 1));
            });
        }

       
        void sdi_updateBitmap(Bitmap bmp)
        {
            Invoke((MethodInvoker)delegate()
            {
                //imgr.DrawImage(bmp, 0, 0, 1920, 1080);
                if (pict1 != null)
                    pict1.Image = bmp;
            });
        }


        void sdi_eventFPS(double fps)
        {
            // cntFPSrate++;
            //if (cntFPSrate >= 60)
            //{
                Invoke((MethodInvoker)delegate()
                {
                    _frmSettings.lblFPSTotal.Text = "FPS_Total: " + String.Format("{0}", Math.Round(fps, 1));
                });
                cntFPSrate = 0;
            //}
        }

        void tmrSend1540_Tick(object sender, EventArgs e)
        {
            if (chkA1_35.Checked)
            {
                Invoke((MethodInvoker)delegate
                {
                    a1_35.Data[0] = (byte)((uint)((double)numA1_35.Value / 0.0054931640625));
                    a1_35.Data[1] = (byte)((uint)((double)numA1_35.Value / 0.0054931640625) >> 8);

                    SendMessage(devBU, devBroadCast, a1_35);
                });
            }
        }

        void cboDevices_DropDownClosed(object sender, EventArgs e)
        {
            tipMessage.Hide(cboDevices);
        }
        
        void cboDevices_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) { return; } // added this line thanks to Andrew's comment
            string text = cboDevices.GetItemText(cboDevices.Items[e.Index]);
            //string text = Devices[e.Index].Description;
            e.DrawBackground();
            using (SolidBrush br = new SolidBrush(e.ForeColor))
            { e.Graphics.DrawString(text, e.Font, br, e.Bounds); }
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected && cboDevices.DroppedDown)
            //{ tipMessage.Show(text, cboDevices, e.Bounds.Right, e.Bounds.Bottom); }
            { tipMessage.Show(Devices[e.Index].Description, cboDevices, e.Bounds.Right, e.Bounds.Bottom); }
            e.DrawFocusRectangle();
        }

        void FillDevices()
        {
            //БУ и МТТД не должны быть в общем списке
            Devices.Clear();
            Devices.Add(devPKP_PU1);
            Devices.Add(devPKP_PU2);
            Devices.Add(devPU_K);
            Devices.Add(devPK_K);
            Devices.Add(devPU_NR);
            Devices.Add(devPK_NR);
            Devices.Add(devPU_RD);
            Devices.Add(devPK_RD);
            Devices.Add(devUSO);

            cboDevices.DrawMode = DrawMode.OwnerDrawFixed;
            cboDevices.DropDownClosed += cboDevices_DropDownClosed;
            cboDevices.DataSource = Devices;
            cboDevices.DisplayMember = "Name";
            //cboDevices.Items.AddRange(Devices.Select(a => a.Name).ToArray());
            cboDevices.SelectedIndex = 0;
        }

        void SendMessage(Device devSource, Device devDestination, Message msg)
        {
            lastMsg = msg;

            //реализация формирования CAN-пакета и отправка
            byte channel = (byte)USBcanServer.eUcanChannel.USBCAN_CHANNEL_CH0;
            USBcanServer.tCanMsgStruct[] canMsg = new USBcanServer.tCanMsgStruct[1];
            int iCnt = 0;

            //canMsg[0].m_bData = new byte[msg.Data.Length];
            canMsg[0].m_bData = new byte[8];
            canMsg[0].m_bDLC = msg.Data == null ? (byte)0 : (byte)msg.Data.Length;

            if(msg.Data!=null)
                Array.Copy(msg.Data, canMsg[0].m_bData, msg.Data.Length);
            
            //canMsg[0].m_bData = msg.Data;
            canMsg[0].m_bFF = (byte)USBcanServer.eUcanMsgFrameFormat.USBCAN_MSG_FF_EXT;
            canMsg[0].m_dwTime = 1000;

            canMsg[0].m_dwID = devSource.Address; // &0x3f;
            canMsg[0].m_dwID |= devDestination.Address << 6;
            canMsg[0].m_dwID |= msg.Descriptor << 12;
            canMsg[0].m_dwID |= msg.Priority << 24;


            bRet = CanSrv.WriteCanMsg(channel, ref canMsg, ref iCnt);

            //if (!_frmDebug.IsHandleCreated) return;
            Invoke((MethodInvoker)delegate
            {
                _frmDebug.lblCanReturnRECEIVED.Text = ((USBcanServer.eUcanReturn)bRet).ToString();
            });


            //Запись в лог об отправке команды
            if ((msg.Descriptor == 1538 && _frmDebug.chk1538.Checked) ||
                        (msg.Descriptor == 1539 && _frmDebug.chk1539.Checked) ||
                        (msg.Descriptor == 1366 && _frmDebug.chk1366.Checked) ||
                        (msg.Descriptor == 1 && _frmDebug.chkZapros.Checked) ||
                        (msg.Descriptor == 2 && _frmDebug.chkAck.Checked) ||
                        (msg.Descriptor == 1540 && _frmDebug.chk1540.Checked))
                UpdateCountsDebug(msg.Descriptor, devSource.Address);
            else
                _frmLog.AddToList(canMsg[0], true);
        }

        void MessageInit()
        {
            comCaptureDevice =  new Message(0, 256, 0, 1);
            comReleaseDevice =  new Message(1, 257, 0, 1);

            a1_35 =             new Message(10, 1540, 20, 5);

            a5_1 =              new Message(0, 1, 1500, 0);
            a5_2 =              new Message(20, 303, 0, 1);
            a5_3 =              new Message(15, 304,  0, 0);
            a5_4 =              new Message(15, 307,  0, 0);
            a5_5 =              new Message(15, 308,  0, 2);
            a5_6 =              new Message(19, 309,  0, 0);
            a5_7 =              new Message(20, 310,  0, 1);
            a5_8 =              new Message(20, 311,  0, 0);
            a5_9 =              new Message(20, 312, 0, 1);
            a5_10 =             new Message(20, 313, 0, 0);
            
            a5_11 =             new Message(20, 314, 0, 0);
            a5_12 =             new Message(20, 315, 0, 0);
            a5_13 =             new Message(20, 316, 0, 0);
            a5_14 =             new Message(25, 371, 0, 0);
            a5_15 =             new Message(25, 372, 0, 0);
            a5_16 =             new Message(25, 373, 0, 0);
            a5_17 =             new Message(25, 374, 0, 0);
            a5_18 =             new Message(25, 375, 0, 0);
            a5_19 =             new Message(25, 376, 0, 0);
            a5_20 =             new Message(20, 356, 0, 3);

            a5_21 =             new Message(5, 319, 0, 2);
            a5_22 =             new Message(7, 320, 0, 1);
            a5_23 =             new Message(20, 321, 0, 0);
            a5_24 =             new Message(20, 324, 0, 0);
            a5_25 =             new Message(30, 328, 0, 0);
            a5_26 =             new Message(11, 358, 0, 0);
            a5_27 =             new Message(5, 274, 0, 0);
            a5_28 =             new Message(5, 359, 0, 0);
            a5_29 =             new Message(0, 361, 0, 4);
            a5_30 =             new Message(0, 362, 0, 4);

            a5_31 =             new Message(4, 333, 0, 0);
            a5_32 =             new Message(3, 334, 0, 0);
            a5_33 =             new Message(9, 336, 0, 0);
            a5_34 =             new Message(3, 338, 0, 4);
            a5_35 =             new Message(6, 337, 0, 4);
            a5_36 =             new Message(20, 380, 0, 0);
            a5_37 =             new Message(20, 381, 0, 0);
            a5_38 =             new Message(27, 349, 0, 0);
            a5_39 =             new Message(27, 353, 0, 0);
            a5_40 =             new Message(1, 354, 0, 0);
            a5_41 =             new Message(5, 355, 0, 0);

            a6_1 =              new Message(0, 2, 0, 0);
            a6_2 =              new Message(25, 1539, 500, 6);
            a6_3 =              new Message(10, 363, 0, 2);
            a6_4 =              new Message(0, 3, 0, 1);

            a6_5_0 =            new Message(31, 1280, 0, 2);
            a6_5_1 =            new Message(31, 1281, 0, 2);
            a6_5_2 =            new Message(31, 1282, 0, 2);
            a6_5_3 =            new Message(31, 1283, 0, 2);
            a6_5_4 =            new Message(31, 1284, 0, 2);
            a6_5_5 =            new Message(31, 1285, 0, 2);

            a6_6 =              new Message(31, 1286, 0, 3);
            a6_7 =              new Message(20, 365, 0, 3);
            a6_8 =              new Message(5, 1366, 1500, 5);
            a6_9 =              new Message(10, 1538, 10, 8);
            a6_10 =             new Message(23, 367, 0, 2);
            a6_11 =             new Message(23, 368, 0, 2);

            a7_3 =              new Message(17, 299, 0, 4);

            a8_2 =              new Message(0, 2, 1500, 0);
            a8_3 =              new Message(17, 370, 0, 4);


            lstMessages.Clear();
            lstMessages.Add(comCaptureDevice);
            lstMessages.Add(comReleaseDevice);
            lstMessages.Add(a1_35);
            lstMessages.Add(a5_1);
            lstMessages.Add(a5_2);
            lstMessages.Add(a5_3);
            lstMessages.Add(a5_4);
            lstMessages.Add(a5_5);
            lstMessages.Add(a5_6);
            lstMessages.Add(a5_7);
            lstMessages.Add(a5_8);
            lstMessages.Add(a5_9);
            lstMessages.Add(a5_10);
            lstMessages.Add(a5_11);
            lstMessages.Add(a5_12);
            lstMessages.Add(a5_13);
            lstMessages.Add(a5_14);
            lstMessages.Add(a5_15);
            lstMessages.Add(a5_16);
            lstMessages.Add(a5_17);
            lstMessages.Add(a5_18);
            lstMessages.Add(a5_19);
            lstMessages.Add(a5_20);
            lstMessages.Add(a5_21);
            lstMessages.Add(a5_22);
            lstMessages.Add(a5_23);
            lstMessages.Add(a5_24);
            lstMessages.Add(a5_25);
            lstMessages.Add(a5_26);
            lstMessages.Add(a5_27);
            lstMessages.Add(a5_28);
            lstMessages.Add(a5_29);
            lstMessages.Add(a5_30);
            lstMessages.Add(a5_31);
            lstMessages.Add(a5_32);
            lstMessages.Add(a5_33);
            lstMessages.Add(a5_34);
            lstMessages.Add(a5_35);
            lstMessages.Add(a5_36);
            lstMessages.Add(a5_37);
            lstMessages.Add(a5_38);
            lstMessages.Add(a5_39);
            lstMessages.Add(a5_40);
            lstMessages.Add(a5_41);
            lstMessages.Add(a6_1);
            lstMessages.Add(a6_2);
            lstMessages.Add(a6_3);
            lstMessages.Add(a6_4);
            lstMessages.Add(a6_5_0);
            lstMessages.Add(a6_5_1);
            lstMessages.Add(a6_5_2);
            lstMessages.Add(a6_5_3);
            lstMessages.Add(a6_5_4);
            lstMessages.Add(a6_5_5);
            lstMessages.Add(a6_6);
            lstMessages.Add(a6_7);
            lstMessages.Add(a6_8);
            lstMessages.Add(a6_9);
            lstMessages.Add(a6_10);
            lstMessages.Add(a6_11);
            lstMessages.Add(a7_3);
            lstMessages.Add(a8_2);
            lstMessages.Add(a8_3);
        }

        void DeviceInit()
        {
            devPKP_PU1 = new Device(36, "Панель управления прицела командира панорамного 7605.00.00.000 - 1");
            devPKP_PU2 = new Device(37, "Панель управления прицела командира панорамного 7605.00.00.000 - 2");
            devPU_K = new Device(40, "Пульт управления командира");
            devPK_K = new Device(32, "Панельный компьютер командира");
            devPU_NR = new Device(39, "Пульт управления начальника разведки");
            devPK_NR = new Device(31, "Панельный компьютер начальника разведки");
            devPU_RD = new Device(38, "Пульт управления разведчика-дальномерщика");
            devPK_RD = new Device(30, "Панельный компьютер разведчика-дальномерщика");
            devBU = new Device(21, "Блок управления АЮИЖ.468364.130");
            devUSO = new Device(34, "Устройство сопряжения с объектом");
            devPKP_BU = new Device(35, "Блок управления прицела командира панорамного 7605.00.00.000");
            devBroadCast = new Device(63, "Условное устройство для широковещания");
            MTTD = new Device(20, "Модуль теле-тепловизионный дальномерный");

            FillDevices();
        }

        private void btnA5_3_Click(object sender, EventArgs e)
        {
            //SendMessage(Devices[selectedIndex], devPKP_BU, a5_3);

            
        }

        private void btnA5_4_Click(object sender, EventArgs e)
        {
            //SendMessage(Devices[selectedIndex], devPKP_BU, a5_4);
        }

        public void btnA5_6_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPKP_BU, a5_6);
        }

        private void btnA5_8_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPKP_BU, a5_8);
        }

        private void btnA5_10_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPKP_BU, a5_10);
        }

        public void btnA5_11_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPKP_BU, a5_11);
        }

        public void btnA5_12_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPKP_BU, a5_12);
        }

        public void btnA5_13_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPKP_BU, a5_13);
        }

        public void btnA5_14_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPKP_BU, a5_14);
        }

        public void btnA5_15_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPKP_BU, a5_15);
        }

        public void btnA5_16_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPKP_BU, a5_16);
        }

        public void btnA5_17_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPKP_BU, a5_17);
        }

        public void btnA5_18_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPKP_BU, a5_18);
        }

        public void btnA5_19_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPKP_BU, a5_19);
        }

        private void numA5_21_ValueChanged(object sender, EventArgs e)
        {
            _frmDalnomer.trackA5_21.Value = (int)_frmDalnomer.numA5_21.Value;
        }

        private void trackA5_21_Scroll(object sender, EventArgs e)
        {
            _frmDalnomer.numA5_21.Value = _frmDalnomer.trackA5_21.Value;
        }

        public void btnA5_21_Click(object sender, EventArgs e)
        {
            a5_21.Data[0] = (byte)((int)_frmDalnomer.numA5_21.Value);
            a5_21.Data[1] = (byte)(((int)_frmDalnomer.numA5_21.Value >> 8) & 0xFF);


            SendMessage(Devices[selectedIndex], devPKP_BU, a5_21);
        }

        private void btnA5_23_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPKP_BU, a5_23);
        }

        private void btnA5_24_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPKP_BU, a5_24);
        }

        public void btnA5_25_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPKP_BU, a5_25);
        }

        private void btnA5_26_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPKP_BU, a5_26);
        }

        public void btnA5_27_Click(object sender, EventArgs e)
        {
            A5_27_Process();
        }

        void A5_27_Process()
        {
            SendMessage(Devices[selectedIndex], devPKP_BU, a5_27);
        }

        public void btnA5_28_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPKP_BU, a5_28);
        }

        private void numA5_29_Hor_ValueChanged(object sender, EventArgs e)
        {
            trackA5_29_Hor.Value = (int)numA5_29_Hor.Value;

            if(numA5_29_Hor.Focused)
                if (!chkA5_29.Checked)
                    SendA5_29();
        }

        private void trackA5_29_Scroll(object sender, EventArgs e)
        {
            numA5_29_Hor.Value = trackA5_29_Hor.Value;
        }

        private void trackA5_29_Vert_Scroll(object sender, EventArgs e)
        {
            numA5_29_Vert.Value = trackA5_29_Vert.Value;
        }

        private void numA5_29_Vert_ValueChanged(object sender, EventArgs e)
        {
            trackA5_29_Vert.Value = (int)numA5_29_Vert.Value;

            if (numA5_29_Vert.Focused)
                if (!chkA5_29.Checked)
                    SendA5_29();
            
        }

        private void btnA5_29_Click(object sender, EventArgs e)
        {
            SendA5_29();
        }

        void SendA5_29()
        {
            a5_29.Data[0] = (byte)((int)((double)numA5_29_Hor.Value / 0.091552734375));
            a5_29.Data[1] = (byte)((int)((double)numA5_29_Hor.Value / 0.091552734375) >> 8);
            a5_29.Data[2] = (byte)((int)((double)numA5_29_Vert.Value / 0.091552734375));
            a5_29.Data[3] = (byte)((int)((double)numA5_29_Vert.Value / 0.091552734375) >> 8);
            //label7.Text = String.Format("{0:X} - {1:X} - {2:X} - {3:X}", a5_29.Data[0], a5_29.Data[1], a5_29.Data[2], a5_29.Data[3]);

            SendMessage(Devices[selectedIndex], devPKP_BU, a5_29);
        }

        private void numA5_30_Hor_ValueChanged(object sender, EventArgs e)
        {
            trackA5_30_Hor.Value = (int)numA5_30_Hor.Value;
            //if(!chkA5_41.Checked)
                //SendA5_30();
        }

        void SendA5_30()
        {
            a5_30.Data[0] = (byte)((int)((double)numA5_30_Hor.Value / 0.0275));
            a5_30.Data[1] = (byte)((int)((double)numA5_30_Hor.Value / 0.0275) >> 8);
            a5_30.Data[2] = (byte)((int)((double)numA5_30_Vert.Value / 0.0275));
            a5_30.Data[3] = (byte)((int)((double)numA5_30_Vert.Value / 0.0275) >> 8);

            SendMessage(Devices[selectedIndex], devPKP_BU, a5_30);
        }

        private void numA5_30_Vert_ValueChanged(object sender, EventArgs e)
        {
            trackA5_30_Vert.Value = (int)numA5_30_Vert.Value;


            //if (!chkA5_41.Checked)
                //SendA5_30();
        }

        private void btnA5_30_Click(object sender, EventArgs e)
        {
            a5_30.Data[0] = (byte)((int)((double)numA5_30_Hor.Value / 0.0275));
            a5_30.Data[1] = (byte)((int)((double)numA5_30_Hor.Value / 0.0275) >> 8);
            a5_30.Data[2] = (byte)((int)((double)numA5_30_Vert.Value / 0.0275));
            a5_30.Data[3] = (byte)((int)((double)numA5_30_Vert.Value / 0.0275) >> 8);


            //label7.Text = String.Format("{0:X} - {1:X} - {2:X} - {3:X}", a5_29.Data[0], a5_29.Data[1], a5_29.Data[2], a5_29.Data[3]);

            SendMessage(Devices[selectedIndex], devPKP_BU, a5_30);
        }

        private void btnA5_31_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPKP_BU, a5_31);
        }

        private void btnA5_32_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPKP_BU, a5_32);
        }

        public void btnA5_34_Click(object sender, EventArgs e)
        {
            SendA5_34();
        }

        void SendA5_34()
        {
            a5_34.Data[0] = (byte)((int)((double)_frmOther.numA5_34_Hor.Value / 0.0275));
            a5_34.Data[1] = (byte)((int)((double)_frmOther.numA5_34_Hor.Value / 0.0275) >> 8);
            a5_34.Data[2] = (byte)((int)((double)_frmOther.numA5_34_Vert.Value / 0.0275));
            a5_34.Data[3] = (byte)((int)((double)_frmOther.numA5_34_Vert.Value / 0.0275) >> 8);


            //label7.Text = String.Format("{0:X} - {1:X} - {2:X} - {3:X}", a5_29.Data[0], a5_29.Data[1], a5_29.Data[2], a5_29.Data[3]);

            SendMessage(Devices[selectedIndex], devPKP_BU, a5_34);
        }

        private void trackA5_35_Hor_Scroll(object sender, EventArgs e)
        {
            numA5_35_Hor.Value = trackA5_35_Hor.Value;
        }

        private void numA5_35_Hor_ValueChanged(object sender, EventArgs e)
        {
            trackA5_35_Hor.Value = (int)numA5_35_Hor.Value;
        }

        private void trackA5_35_Vert_Scroll(object sender, EventArgs e)
        {
            numA5_35_Vert.Value = trackA5_35_Vert.Value;
        }

        private void numA5_35_Vert_ValueChanged(object sender, EventArgs e)
        {
            trackA5_35_Vert.Value = (int)numA5_35_Vert.Value;
        }

        private void btnA5_35_Click(object sender, EventArgs e)
        {
            a5_35.Data[0] = (byte)((int)((double)numA5_35_Hor.Value / 0.091552734375));
            a5_35.Data[1] = (byte)((int)((double)numA5_35_Hor.Value / 0.091552734375) >> 8);
            a5_35.Data[2] = (byte)((int)((double)numA5_35_Vert.Value / 0.091552734375));
            a5_35.Data[3] = (byte)((int)((double)numA5_35_Vert.Value / 0.091552734375) >> 8);


            //label7.Text = String.Format("{0:X} - {1:X} - {2:X} - {3:X}", a5_35.Data[0], a5_35.Data[1], a5_35.Data[2], a5_35.Data[3]);

            SendMessage(Devices[selectedIndex], devPKP_BU, a5_35);
        }

        public void btnA8_1_Click(object sender, EventArgs e)
        {
            SendMessage(devBU, devPKP_BU, a5_1);
        }

        public void btnA8_3_Click(object sender, EventArgs e)
        {
            a8_3.Data[0] = (byte)((int)((double)_frmBU.numA8_3_Hor.Value / 0.091552734375));
            a8_3.Data[1] = (byte)((int)((double)_frmBU.numA8_3_Hor.Value / 0.091552734375) >> 8);
            a8_3.Data[2] = (byte)((int)((double)_frmBU.numA8_3_Vert.Value / 0.091552734375));
            a8_3.Data[3] = (byte)((int)((double)_frmBU.numA8_3_Vert.Value / 0.091552734375) >> 8);

            SendMessage(devBU, devPKP_BU, a8_3);
        }

        private void radA5_2_Click(object sender, EventArgs e)
        {
            RadioButton rad = (RadioButton)sender;

            if (rad.Checked)
            {
                byte b = 0;
                switch (rad.Name.Substring(8))
                {
                    case "FocusTPVK": b = 2; break;
                    case "UsilTPVK": b = 3; break;
                    case "Strob": b = 4; break;
                    case "FocusDTK": b = 1; break;
                }

                a5_2.Data[0] = b;
                SendMessage(Devices[selectedIndex], devPKP_BU, a5_2);
            } 
        }

        private void radA5_5_Click(object sender, EventArgs e)
        {
            /*RadioButton rad = (RadioButton)sender;

            if (rad.Checked)
            {
                byte b = 0;
                byte val = 0;
                switch (rad.Name.Substring(8))
                {
                    case "FocusTPVK": b = 2; val = (byte)numA5_5_TPBK.Value; break;
                    case "UsilTPVK": b = 3; val = (byte)numA5_5_UsilTPVK.Value;  break;
                    case "FocusDTK": b = 1; val = (byte)numA5_5_DTK.Value;  break;
                    case "MarkColor":
                        {
                            b = 0;
                            val = (byte)cmbA5_5_MarkColor.SelectedIndex;
                        }
                        break;
                }

                a5_5.Data[0] = b;
                a5_5.Data[1] = val;

                SendMessage(Devices[selectedIndex], devPKP_BU, a5_5);
            }*/

            A5_5_Process();
        }

        void A5_5_Process()
        {
            byte b = 0, val = 0;
            if (radA5_5_MarkColor.Checked)
            {
                b = 0;  val = (byte)cmbA5_5_MarkColor.SelectedIndex;
            }
            else if (radA5_5_FocusDTK.Checked)
            {
                b = 1; val = (byte)numA5_5_DTK.Value;
            }
            else if (radA5_5_FocusTPVK.Checked)
            {
                b = 2; val = (byte)numA5_5_TPBK.Value;
            }
            else if (radA5_5_UsilTPVK.Checked)
            {
                b = 3; val = (byte)numA5_5_UsilTPVK.Value;
            }

            a5_5.Data[0] = b;
            a5_5.Data[1] = val;

            SendMessage(Devices[selectedIndex], devPKP_BU, a5_5);
        }

        public void chkA5_7_CheckedChanged(object sender, EventArgs e)
        {
            a5_7.Data[0] = 0;
            if (_frmOther.chkA5_7_IndNizhPol.Checked) a5_7.Data[0] |= (1 << 0);
            if (_frmOther.chkA5_7_IndPricel.Checked) a5_7.Data[0] |= (1 << 1);
            if (_frmOther.chkA5_7_VsplyvInd.Checked) a5_7.Data[0] |= (1 << 2);
            if (_frmOther.chkA5_7_IndKrugDiag.Checked) a5_7.Data[0] |= (1 << 3);
            if (_frmOther.chkA5_7_ShowMenu.Checked) a5_7.Data[0] |= (1 << 4);
            if (_frmOther.chkA5_7_CvetPolya.Checked) a5_7.Data[0] |= (1 << 5);
            if (_frmOther.chkA5_7_IndUglVizir.Checked) a5_7.Data[0] |= (1 << 6);

            SendMessage(Devices[selectedIndex], devPKP_BU, a5_7);
        }

        private void radA5_9_Click(object sender, EventArgs e)
        {
            a5_9.Data[0] = 0;
            if (radA5_9_TPVK.Checked) a5_9.Data[0] |= (1 << 0);
            if (radA5_9_UPZ.Checked) a5_9.Data[0] |= (1 << 2);
            if (radA5_9_OPZ.Checked) a5_9.Data[0] |= (3 << 2);
            if (radA5_9_16.Checked) a5_9.Data[0] |= (1 << 4);
            if (radA5_9_2.Checked) a5_9.Data[0] |= (2 << 4);
            if (radA5_9_32.Checked) a5_9.Data[0] |= (3 << 4);

            SendMessage(Devices[selectedIndex], devPKP_BU, a5_9);
        }

        public void chkA5_20_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            //if (chk.Checked) { chk.BackColor = Color.LightGreen; }
            //else { chk.BackColor = Color.Transparent; }
            chk.Checked = false;

            //A5_20_Process();
            
        }

        public void chkPU_MouseDown(object sender, MouseEventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            chk.Tag = true;

            A5_20_Process();
        }

        public void chkPU_MouseUp(object sender, MouseEventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            chk.Tag = false;

            A5_20_Process();
        }

        public void A5_20_MenuTehnAndInzh()
        {
            A5_20_Process();
        }

        void A5_20_Process()
        {
            a5_20.Data[0] = 0;
            a5_20.Data[1] = 0;
            a5_20.Data[2] = 0;



            if (_frmPU.chkA5_20_Menu.Tag != null && (bool)_frmPU.chkA5_20_Menu.Tag) a5_20.Data[0] |= (1 << 0);
            if (_frmPU.chkA5_20_Left.Tag != null && (bool)_frmPU.chkA5_20_Left.Tag) a5_20.Data[0] |= (1 << 1);
            if (_frmPU.chkA5_20_Right.Tag != null && (bool)_frmPU.chkA5_20_Right.Tag) a5_20.Data[0] |= (1 << 2);
            if (_frmPU.chkA5_20_Up.Tag != null && (bool)_frmPU.chkA5_20_Up.Tag) a5_20.Data[0] |= (1 << 3);
            if (_frmPU.chkA5_20_Down.Tag != null && (bool)_frmPU.chkA5_20_Down.Tag) a5_20.Data[0] |= (1 << 4);
            if (_frmPU.chkA5_20_Obogrev.Tag != null && (bool)_frmPU.chkA5_20_Obogrev.Tag) a5_20.Data[0] |= (1 << 5);
            if (_frmPU.chkA5_20_Focus.Tag != null && (bool)_frmPU.chkA5_20_Focus.Tag) a5_20.Data[0] |= (1 << 6);
            if (_frmPU.chkA5_20_Usil.Tag != null && (bool)_frmPU.chkA5_20_Usil.Tag) a5_20.Data[0] |= (1 << 7);

            if (_frmPU.chkA5_20_Svetofilter.Tag != null && (bool)_frmPU.chkA5_20_Svetofilter.Tag) a5_20.Data[1] |= (1 << 0);
            if (_frmPU.chkA5_20_Polar.Tag != null && (bool)_frmPU.chkA5_20_Polar.Tag) a5_20.Data[1] |= (1 << 1);
            if (_frmPU.chkA5_20_Day.Tag != null && (bool)_frmPU.chkA5_20_Day.Tag) a5_20.Data[1] |= (1 << 2);
            if (_frmPU.chkA5_20_Uvel.Tag != null && (bool)_frmPU.chkA5_20_Uvel.Tag) a5_20.Data[1] |= (1 << 3);
            if (_frmPU.chkA5_20_Umen.Tag != null && (bool)_frmPU.chkA5_20_Umen.Tag) a5_20.Data[1] |= (1 << 4);
            if (_frmPU.chkA5_20_Marka.Tag != null && (bool)_frmPU.chkA5_20_Marka.Tag) a5_20.Data[1] |= (1 << 5);

            if (_frmPU.radA5_20_I.Checked) a5_20.Data[1] |= (1 << 6);
            if (_frmPU.radA5_20_Dezh.Checked) a5_20.Data[1] |= (2 << 6);


            SendMessage(Devices[selectedIndex], devPKP_BU, a5_20);
        }

        public void radA5_20_Click(object sender, EventArgs e)
        {
            A5_20_Process();
        }

        private void radA5_22_Click(object sender, EventArgs e)
        {
            a5_22.Data[0] = 0;

            if (radA5_22_Vkl.Checked) a5_22.Data[0] |= 1;
            if (radA5_22_Dezh.Checked) a5_22.Data[0] |= 2;

            SendMessage(Devices[selectedIndex], devPKP_BU, a5_22);
        }

        private void btnCaptureDevice_Click(object sender, EventArgs e)
        {
            comCaptureDevice.Data[0] = devPKP_BU.Address;

            SendMessage(Devices[selectedIndex], devBroadCast, comCaptureDevice);
        }

        private void btnReleaseDevice_Click(object sender, EventArgs e)
        {
            comReleaseDevice.Data[0] = devPKP_BU.Address;

            SendMessage(Devices[selectedIndex], devBroadCast, comReleaseDevice);
        }


        private void btnA5_41_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPKP_BU, a5_41);
        }

        public void btnA5_38_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPKP_BU, a5_38);
        }

        public void btnA5_39_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPKP_BU, a5_39);
        }

        public void btnA5_40_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPKP_BU, a5_40);
        }

        private void btnA5_33_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPKP_BU, a5_33);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //_frmShutDown.Show();

            CanSrv.CanMsgReceivedEvent += CanSrv_CanMsgReceivedEvent;
            CanSrv.StatusEvent += CanSrv_StatusEvent;
            CanSrv.FatalDisconnectEvent += CanSrv_FatalDisconnectEvent;
            USBcanServer.ConnectEvent += USBcanServer_ConnectEvent;
            USBcanServer.DisconnectEvent += USBcanServer_DisconnectEvent;
            CanSrv.InitCanEvent += CanSrv_InitCanEvent;

            bool b = CanInit();

            myJoystick = new MyJoystick();
            myJoystick.PeriodWorking = 50;
            myJoystick.msgAppeared += myJoystick_msgAppeared;
            myJoystick.myJoystickStateReceived += myJoystick_myJoystickStateReceived;
            myJoystick.StartWorking();

            timerSendValues.Start();




            

            panelPict.Location = new Point(0, 0);
            panelPict.Size = new Size(1920, 1080);
            videoSourcePlayer.Parent = panelPict;

            this.HorizontalScroll.Enabled = false;
            //this.mode


            _frmLog.Show();
            _frmShutDown.Show();
            //_frmShutDown.Hide();
        }

        void CanSrv_InitCanEvent(byte bDeviceNr_p, byte bChannel_p)
        {
            //throw new NotImplementedException();
        }

        void USBcanServer_DisconnectEvent()
        {
            //CanShutdown();
        }

        void USBcanServer_ConnectEvent()
        {
            bool b = CanInit();
        }

        void CanSrv_FatalDisconnectEvent(byte bDeviceNr_p)
        {
            CanShutdown();
        }

        void myJoystick_myJoystickStateReceived(MyJoystickState state)
        {
            Invoke((MethodInvoker)delegate
            {
                //обработка нажатий кнопок джойстика
                if (state.buttons[0] != lastJoystickStateButtons[0] && state.buttons[0]) A5_27_Process(); //выполняем команду Измерить дальность
                if (state.buttons[1] != lastJoystickStateButtons[1]) 
                    if(state.buttons[1])
                        chkA5_41.Checked = true;  //включаем фиксацию (имитируем нажатие чекбокса)
                if (state.buttons[2] != lastJoystickStateButtons[2]) 
                    if(state.buttons[2])
                        chkA5_41.Checked = false; //выключаем фиксацию
                if (state.buttons[3] != lastJoystickStateButtons[3])   //уменьшаем фокус.
                {
                    if (state.buttons[3])
                    {
                        _frmPU.chkA5_20_Umen.Tag = true;
                        A5_20_Process();
                    }
                    else
                    {
                        _frmPU.chkA5_20_Umen.Tag = false;
                        A5_20_Process();
                    }
                }
                if (state.buttons[4] != lastJoystickStateButtons[4])   //увеличиваем фокус
                {
                    if (state.buttons[4])
                    {
                        _frmPU.chkA5_20_Uvel.Tag = true;
                        A5_20_Process();
                    }
                    else
                    {
                        _frmPU.chkA5_20_Uvel.Tag = false;
                        A5_20_Process();
                    }
                }

                state.buttons.CopyTo(lastJoystickStateButtons, 0);

                //управление ползунками "Управление наведением ПКП-МРО по скорости А5.30"
                if (!chkA5_41.Checked && chkJoy.Checked)
                {
                    kHor = (Math.Abs(trackA5_30_Hor.Minimum) + Math.Abs(trackA5_30_Hor.Maximum)) / ((double)(ushort.MaxValue) - 2 *(double)_frmSettings.numCoefSensHor.Value) ;
                    int codeH = state.x;
                    if (Math.Abs(codeH - ushort.MaxValue / 2) > (double)_frmSettings.numCoefSensHor.Value)
                    {
                        if (codeH > ushort.MaxValue / 2)
                            codeH -= (int)_frmSettings.numCoefSensHor.Value;
                        else
                            codeH += (int)_frmSettings.numCoefSensHor.Value;
                    }
                    else
                        codeH = ushort.MaxValue / 2;

                    double dCodeHor = kHor * (codeH - ushort.MaxValue / 2);
                    int valH = (int)(Math.Round(dCodeHor * (double)_frmSettings.numCoefKrutHor.Value));
                    trackA5_30_Hor.Value = valH;

                    


                    kVert = (Math.Abs(trackA5_30_Vert.Minimum) + Math.Abs(trackA5_30_Vert.Maximum)) / ((double)(ushort.MaxValue) - 2 * (double)_frmSettings.numCoefSensVert.Value);
                    int codeV = state.y;

                    if (Math.Abs(codeV - ushort.MaxValue / 2) > (double)_frmSettings.numCoefSensVert.Value)
                    {
                        if (codeV > ushort.MaxValue / 2)
                            codeV -= (int)_frmSettings.numCoefSensVert.Value;
                        else
                            codeV += (int)_frmSettings.numCoefSensVert.Value;
                    }
                    else
                        codeV = ushort.MaxValue / 2;

                    double dCodeVert = kVert * (codeV - ushort.MaxValue / 2);
                    int valV = (int)(Math.Round(dCodeVert * (double)_frmSettings.numCoefKrutVert.Value));
                    trackA5_30_Vert.Value = -1*valV;
                }
            });
        }

        void myJoystick_msgAppeared(string str)
        {
            _frmLog.AddToList(str);
        }


        void CanSrv_StatusEvent(byte bDeviceNr_p, byte bChannel_p)
        {
            USBcanServer.tStatusStruct statusStruct = new USBcanServer.tStatusStruct();
            CanSrv.GetStatus((byte)USBcanServer.eUcanChannel.USBCAN_CHANNEL_CH0, ref statusStruct);
            _frmLog.AddToList("CAN STATUS UPDATED: " + ((USBcanServer.eUcanCanStatus)statusStruct.m_wCanStatus).ToString());
            Invoke((MethodInvoker)delegate
            {
                //_frmDebug.txtStatus.Text = ((DESCR_CAN_STATUS)statusStruct.m_wCanStatus).ToString();
                //DESCR_CAN_STATUS dEnum = new DESCR_CAN_STATUS();
                _frmDebug.txtStatus.Text = string.Empty;
                for (int i = 0; i < 10; i++)
                {
                    if ((statusStruct.m_wCanStatus & (1 << i)) != 0) 
                    { 
                       _frmDebug.txtStatus.Text+= ((DESCR_CAN_STATUS)(statusStruct.m_wCanStatus & (1 << i))).ToString() + "\n";
                    }
                }
                if (_frmDebug.txtStatus.Text == string.Empty) _frmDebug.txtStatus.Text = "No_Error";
                else
                    _frmDebug.txtStatus.Text = _frmDebug.txtStatus.Text.Substring(0, _frmDebug.txtStatus.Text.Length - 1);

            });
        }

        void CanSrv_CanMsgReceivedEvent(byte bDeviceNr_p, byte bChannel_p)
        {
            byte channel = (byte)USBcanServer.eUcanChannel.USBCAN_CHANNEL_CH0;
            int iCnt = 0;
            USBcanServer.tCanMsgStruct[] msg = new USBcanServer.tCanMsgStruct[1000];

            bRet = CanSrv.ReadCanMsg(ref channel, ref msg, ref iCnt);

            if (iCnt <= 0) return;

            Invoke((MethodInvoker)delegate
            {
                _frmDebug.lblCanReturnSEND.Text = ((USBcanServer.eUcanReturn)bRet).ToString();
            });

            if (bRet == (byte)USBcanServer.eUcanReturn.USBCAN_SUCCESSFUL)
            {
                for (int i = 0; i < iCnt; i++) 
                {
                    byte dAddr = (byte)((msg[i].m_dwID >> 6) & 0x3F);
                    ushort descr = (ushort)((msg[i].m_dwID >> 12) & 0xFFF);


                    if (descr == 1280) iCnt1280++;
                    if (InvokeRequired)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            _frmDebug.lblCnt1280.Text = iCnt1280.ToString();
                        });
                    }
                    else
                        _frmDebug.lblCnt1280.Text = iCnt1280.ToString();


                    if ((descr == 1538 && _frmDebug.chk1538.Checked) ||
                        (descr == 1539 && _frmDebug.chk1539.Checked) ||
                        (descr == 1366 && _frmDebug.chk1366.Checked) ||
                        (descr == 1 && _frmDebug.chkZapros.Checked) ||
                        (descr == 2 && _frmDebug.chkAck.Checked))
                        //(descr == 2 && _frmDebug.chkAckBU.Checked && dAddr == 21))
                        UpdateCountsDebug(descr, dAddr);
                    else
                    {
                        _frmLog.AddToList(msg[i], false);
                    }

                    if (dAddr != 63 && dAddr != Devices[selectedIndex].Address && dAddr != 21) //не обрабатываем входящее сообщение если оно не широковещательное или же не адресовано нам. Только отображаем в логе
                        return;

                    ReactPing(msg[i]); //функция реагирования в ответ на ту или иную посылку
                }
            }
        }

        void ReactPing(USBcanServer.tCanMsgStruct msg)
        {
            byte sAddr = (byte)(msg.m_dwID & 0x3F);
            byte dAddr = (byte)((msg.m_dwID >> 6) & 0x3F);
            ushort descr = (ushort)((msg.m_dwID >> 12) & 0xFFF);

            //определение входящей команды
            Message inMsg = GetMessageByDescriptor(descr);
            if (inMsg == null)
            {
                _frmLog.AddToList("Команда не найдена!");
                return;
            }

            Array.Copy(msg.m_bData, inMsg.Data, inMsg.Data.Length);
            //inMsg.Data = msg.m_bData;

            
            if (inMsg.Type == typeMessage.COMMAND && dAddr!=63)
            {
                //заполняем команду подтверждения дескриптором
                a6_5_0.Data[0] = (byte)descr;
                a6_5_0.Data[1] = (byte)(descr >> 8);

                if(dAddr!=21)
                    SendMessage(Devices[selectedIndex], devPKP_BU, a6_5_0); //шлем подтверждение принятой команды
                else
                    SendMessage(devBU, devPKP_BU, a6_5_0); //шлем подтверждение принятой команды
            }

            switch (descr)
            {
                case 1366:   //обрабатываем широковещательную посылку с текущей дальностью и стробом. Выводим на форму значения.
                    UpdateRangeStrob(inMsg.Data); 
                    break;
                case 1:     //при запросе отклика от ПКП-БУ отвечаем откликом соответствующего устройства.
                    if (dAddr == 21){
                        doAckBU();
                    }
                    else
                        SendMessage(Devices[selectedIndex], devPKP_BU, a6_2);
                    break;
                case 1538:  //обработка широковещательного сообщения с углами наведения и угловыми скоростями по двум осям, вывод на форму
                    cntOfAnglesRate++;
                    if (cntOfAnglesRate >= 10) //прореживаем вывод на экран
                    {
                        UpdateAnglesAndSpeeds(inMsg.Data);
                        cntOfAnglesRate = 0;
                    }
                    break;
                case 1539:  //обработка широковещательного сообщения с состоянием ПКП-МРО, вывод на форму.   
                    UpdateStatePKP_MRO(inMsg.Data);
                    break;
                case 363:   //обработка ответа на запрос дальности

                    UpdateRange(inMsg.Data);
                    break;
                case 1280: //в зависимости от прошлой посланной команды реагируем на пришедшее подтверждение
                    switch (lastMsg.Descriptor)
                    {
                        case 256:
                            UpdateDeviceCaptured();
                            break;
                        case 257:
                            UpdateDeviceReleased();
                            break;
                    }
                    break;
                case 256: //обработка широковещалки с объявлением попытки захвата устройства ПКП_БУ
                    UpdateDeviceCapturedBroadcast(sAddr);
                    break;
                case 257:
                case 3:
                    UpdateDeviceReleased();
                    break;
                case 299:
                    UpdateAnglesForBU(inMsg.Data);
                    break;
                case 365:
                    UpdateNarabotkaDK(inMsg.Data);
                    break;
                case 367:
                    UpdateVremyaTPVK(inMsg.Data);
                    break;
                case 368:
                    UpdateVremyaRefridg(inMsg.Data);
                    break;

            }
        }


        void UpdateVremyaRefridg(byte[] buf)
        {
            if (buf.Length < 2) return;
            Invoke((MethodInvoker)delegate
            {
                _frmOther.txtTRefridg.Text = ((ushort)(buf[0] + (buf[1] << 8))).ToString();
            });
        }

        void UpdateVremyaTPVK(byte[] buf)
        {
            if (buf.Length < 2) return;
            Invoke((MethodInvoker)delegate
            {
                _frmOther.txtTCamera.Text = ((ushort)(buf[0] + (buf[1] << 8))).ToString();
            });
        }

        void UpdateNarabotkaDK(byte[] buf)
        {
            if (buf.Length < 2) return;
            Invoke((MethodInvoker)delegate
            {
                _frmOther.txtNarabotkaDK.Text = ((uint)(buf[0] + (buf[1] << 8) + (buf[2] << 16))).ToString();
            });
        }

        void UpdateAnglesForBU(byte[] buf)
        {
            if (buf.Length < 4) return;
            Invoke((MethodInvoker)delegate
            {
                txtA7_3_AngleHor.Text = ((buf[0] + (buf[1] << 8)) * 0.091552734375).ToString();
                txtA7_3_AngleVert.Text = ((short)(buf[2] + (buf[3] << 8)) * 0.091552734375).ToString();
            });
        }

        void UpdateDeviceCapturedBroadcast(byte sAddr)
        {
            Invoke((MethodInvoker)delegate
            {
                lblCapturedDevice.Text = ((DEVICE_ADDR)sAddr).ToString();
            });
        }

        void UpdateDeviceReleased()
        {
            Invoke((MethodInvoker)delegate
            {
                lblCapturedDevice.Text = "";
            });
        }

        void UpdateDeviceCaptured()
        {
            //if (buf.Length < 2) return;
            Invoke((MethodInvoker)delegate
            {
               // ushort descr = (ushort)(buf[0] + (buf[1] << 8));
                lblCapturedDevice.Text = (Devices[selectedIndex].Name).ToString();
            });
        }

        void UpdateCountsDebug(ushort descr, byte Addr)
        {
            Invoke((MethodInvoker)delegate
            {
                switch (descr)
                {
                    case 1538:
                        if (_frmDebug.chk1538.Checked) _frmDebug.lblCnt1538.Text = (_frmDebug.iCnt1538++).ToString();
                        break;
                    case 1539:
                        if (_frmDebug.chk1539.Checked) _frmDebug.lblCnt1539.Text = (_frmDebug.iCnt1539++).ToString();
                        break;
                    case 1366:
                        if (_frmDebug.chk1366.Checked) _frmDebug.lblCnt1366.Text = (_frmDebug.iCnt1366++).ToString();
                        break;
                    case 1:
                        if (Addr == 21)
                            _frmDebug.lblCntZaprosBU.Text = _frmDebug.iCntZaprosBU++.ToString();
                        else
                            _frmDebug.lblZaprosSELECTED.Text = _frmDebug.iCntZaprosSELECTED++.ToString();
                        break;
                    case 2:
                        if (Addr == 21)
                            _frmDebug.lblCntAckBU.Text = (_frmDebug.iCntAckBU++).ToString();
                        else
                            _frmDebug.lblCntAckSELECTED.Text = (_frmDebug.iCntAckSELECTED++).ToString();
                        break;
                    case 1540:
                        _frmDebug.lbl1540.Text = (_frmDebug.iCnt1540++).ToString();
                        break;
                }

            });
        }

        void doAckBU()
        {
            Invoke((MethodInvoker)delegate
            {
                if(_frmBU.chkAckBU.Checked)
                    SendMessage(devBU, devPKP_BU, a8_2);
            });
        }

        void UpdateRange(byte[] buf)
        {
            if (buf.Length < 2) return;
            Invoke((MethodInvoker)delegate
            {
                _frmDalnomer.txtDalnostMeasured.Text = (buf[0] + (buf[1] << 8)).ToString();
            });
        }
        void UpdateStatePKP_MRO(byte[] buf)
        {
            if (buf.Length < 6) return;
            Invoke((MethodInvoker)delegate
            {
                string str = "";
                switch (buf[0] & 0x07)
                {
                    case 0: str = "инициализация"; txtPKP_MRO_Mode.BackColor = Color.Yellow; txtPKP_MRO_Mode.ForeColor = SystemColors.WindowText; break;
                    case 1: str = "наблюдение"; txtPKP_MRO_Mode.BackColor = Color.LightGreen; txtPKP_MRO_Mode.ForeColor = SystemColors.WindowText; break;
                    case 2: str = "автонаведение"; txtPKP_MRO_Mode.BackColor = Color.Black; txtPKP_MRO_Mode.ForeColor = Color.White; break;
                    case 3: str = "фиксация скорости"; txtPKP_MRO_Mode.BackColor = Color.Black; txtPKP_MRO_Mode.ForeColor = Color.White; break;
                    case 4: str = "целеуказание"; txtPKP_MRO_Mode.BackColor = Color.Black; txtPKP_MRO_Mode.ForeColor = Color.White; break;
                    case 5: str = "выверка"; txtPKP_MRO_Mode.BackColor = Color.Violet; txtPKP_MRO_Mode.ForeColor = SystemColors.WindowText; break;
                    case 6: str = "тестирование"; txtPKP_MRO_Mode.BackColor = Color.Violet; txtPKP_MRO_Mode.ForeColor = SystemColors.WindowText; break;
                    case 7: str = "выключение"; txtPKP_MRO_Mode.BackColor = Color.LightCoral; txtPKP_MRO_Mode.ForeColor = Color.White; break;
                }
                txtPKP_MRO_Mode.Text = str;

                if ((buf[0] & 0x08) != 0) indPKP_MRO_Ready.BackColor = Color.LightGreen;    else indPKP_MRO_Ready.BackColor = Color.Red;
                if ((buf[0] & 0x10) != 0) indSS_Ready.BackColor = Color.LightGreen;         else indSS_Ready.BackColor = Color.Red;
                if ((buf[0] & 0x20) != 0) indDK_Ready.BackColor = Color.LightGreen; else indDK_Ready.BackColor = Color.Red;
                if ((buf[0] & 0x40) != 0) indDTK_Ready.BackColor = Color.LightGreen; else indDTK_Ready.BackColor = Color.Red;
                if ((buf[0] & 0x80) != 0) indTPVK_Ready.BackColor = Color.LightGreen; else indTPVK_Ready.BackColor = Color.Red;

                if ((buf[1] & 0x01) != 0) { txtCanal.Text = "ТПВК"; txtCanal.BackColor = Color.Black; txtCanal.ForeColor = Color.White; } else { txtCanal.Text = "ДТК"; txtCanal.BackColor = Color.LightGreen; txtCanal.ForeColor = SystemColors.WindowText; }

                switch ((buf[1]>>1) & 0x03)
                {
                    case 0: str = "ШПЗ"; break;
                    case 1: str = "УПЗ"; break;
                    case 3: str = "ОПЗ"; break;
                    default:
                    case 2: str = "error"; break;
                }
                txtPoleZrenia.Text = str;

                switch ((buf[1] >> 3) & 0x03)
                {
                    case 0: str = "1 крат"; break;
                    case 1: str = "1.6 крат"; break;
                    case 2: str = "2 крат"; break;
                    case 3: str = "3.2 крат"; break;
                }
                txtUvel.Text = str;

                switch ((buf[1] >> 5) & 0x03)
                {
                    case 0: str = "НОРМ"; break;
                    case 1: str = "СВЕТ"; break;
                    case 2: str = "ДЫМ"; break;
                    default:
                    case 3: str = "error"; break;
                }
                txtSvetofilter.Text = str;

                if ((buf[1] & 0x80) != 0) txtPolar.Text = "негатив"; else txtPolar.Text = "позитив";

                if ((buf[2] & 0x01) != 0) txtObogrev.Text = "включен"; else txtObogrev.Text = "выключен";
                switch ((buf[2] >> 1) & 0x03)
                {
                    case 0: str = "белая"; break;
                    case 1: str = "серая"; break;
                    case 3: str = "выключена"; break;
                    case 2: str = "черная"; break;
                }
                txtMark.Text = str;

                if ((buf[2] & 0x08) != 0) indNizhPole.BackColor = Color.LightGreen; else indNizhPole.BackColor = Color.Red;
                if ((buf[2] & 0x10) != 0) indVsplyv.BackColor = Color.LightGreen; else indVsplyv.BackColor = Color.Red;
                if ((buf[2] & 0x20) != 0) indKrugDiag.BackColor = Color.LightGreen; else indKrugDiag.BackColor = Color.Red;
                if ((buf[2] & 0x40) != 0) indMenu.BackColor = Color.LightGreen; else indMenu.BackColor = Color.Red;
                if ((buf[2] & 0x80) != 0) indUgolVizir.BackColor = Color.LightGreen; else indUgolVizir.BackColor = Color.Red;

                txtFocusDTK.Text = buf[3].ToString();
                txtFocusTPVK.Text = buf[4].ToString();
                txtUsil.Text = buf[5].ToString();
            });
        }

        void UpdateAnglesAndSpeeds(byte[] buf)
        {
            if (buf.Length < 8) return;
            Invoke((MethodInvoker)delegate
            {
                double s1 = ((short)(buf[0] + (buf[1] << 8)) * 0.091552734375);
                double s2 = ((short)(buf[2] + (buf[3] << 8)) * 0.0275);
                double s3 = ((buf[4] + (buf[5] << 8)) * 0.091552734375);
                double s4 = ((short)(buf[6] + (buf[7] << 8)) * 0.0275);
                txtUgolVert.Text = Math.Round(s1,2).ToString();
                txtSkorVert.Text = Math.Round(s2, 2).ToString();
                txtUgolHor.Text = Math.Round(s3, 2).ToString();
                txtSkorHor.Text = Math.Round(s4, 2).ToString();
            });
        }

        void UpdateRangeStrob(byte[] buf)
        {
            if (buf.Length < 5) return;
            Invoke((MethodInvoker)delegate
            {
                _frmDalnomer.txtDalnost.Text = (buf[0] + (buf[1] << 8)).ToString();
                if ((buf[2] & 0x01) != 0) _frmDalnomer.txtCelVStrob.Text = "есть цель"; else _frmDalnomer.txtCelVStrob.Text = "нет цели";
                if ((buf[2] & 0x02) != 0) _frmDalnomer.txtCelZaStrob.Text = "более одной"; else _frmDalnomer.txtCelZaStrob.Text = "нет или одна";
                _frmDalnomer.txtStrob.Text = (buf[3] + (buf[4] << 8)).ToString();
            });
        }

        Message GetMessageByDescriptor(ushort descr)
        {
            Message msg = lstMessages.Find(n => n.Descriptor == descr);
            return msg;
        }

        void CanShutdown()
        {
            bRet = CanSrv.Shutdown();
            if (bRet != (byte)USBcanServer.eUcanReturn.USBCAN_SUCCESSFUL)
            {
                //MessageBox.Show("Shutdown error. " + (USBcanServer.eUcanReturn)bRet);
                _frmLog.AddToList("Shutdown error. " + (USBcanServer.eUcanReturn)bRet);
            }
            else
            {
                _frmLog.AddToList("Устройство отключено. " + (USBcanServer.eUcanReturn)bRet);
            }
        }

        void ShowCanInfo()
        {
            bool b = CanSrv.IsCan0Initialized;
            bool b1 = CanSrv.IsCan1Initialized;
            bool b2 = CanSrv.IsHardwareInitialized;
            USBcanServer.tUcanHardwareInfoEx info = new USBcanServer.tUcanHardwareInfoEx();
            USBcanServer.tUcanChannelInfo ch0Inf = new USBcanServer.tUcanChannelInfo();
            USBcanServer.tUcanChannelInfo ch1Inf = new USBcanServer.tUcanChannelInfo();
            byte bb = CanSrv.GetHardwareInfo(ref info, ref ch0Inf, ref ch1Inf);
            bool b4 = USBcanServer.CheckIs_G3(info);
            bool b5 = USBcanServer.CheckIs_G4(info);
            _frmLog.AddToList("IsCan0Initialized".PadRight(30) + b);
            _frmLog.AddToList("IsCan1Initialized".PadRight(30) + b1);
            _frmLog.AddToList("IsHardwareInitialized".PadRight(30) + b2);
            _frmLog.AddToList("Is_G3".PadRight(30) + b4);
            _frmLog.AddToList("Is_G4".PadRight(30) + b5);
            _frmLog.AddToList("Device number of CANmodul".PadRight(30) + info.m_bDeviceNr);
            _frmLog.AddToList("Additional flags".PadRight(30) + info.m_dwFlags);
            _frmLog.AddToList("Version firmware".PadRight(30) + info.m_dwFwVersionEx);
            _frmLog.AddToList("Product code".PadRight(30) + (USBcanServer.eUcanProductCode)info.m_dwProductCode);//
            _frmLog.AddToList("SerialNumber".PadRight(30) + info.m_dwSerialNr);
            _frmLog.AddToList("Size structure".PadRight(30) + info.m_dwSize);
            _frmLog.AddToList("Unique ID".PadRight(30) + info.m_dwUniqueId0);
        }

        bool CanInit()
        {
            bRet = CanSrv.InitHardware();
            _frmLog.AddToList("InitHardware: " + ((USBcanServer.eUcanReturn)bRet).ToString());
            if (bRet != (byte)USBcanServer.eUcanReturn.USBCAN_SUCCESSFUL)
            {
                return false;
            }

            bRet = CanSrv.InitCan((byte)USBcanServer.eUcanChannel.USBCAN_CHANNEL_CH0,
                (short)USBcanServer.eUcanBaudrate.USBCAN_BAUD_250kBit,
                (int)USBcanServer.eUcanBaudrateEx.USBCAN_BAUDEX_USE_BTR01,
                USBcanServer.USBCAN_AMR_ALL,
                USBcanServer.USBCAN_ACR_ALL,
                (byte)USBcanServer.tUcanMode.kUcanModeNormal,
                (byte)USBcanServer.eUcanOutputControl.USBCAN_OCR_DEFAULT);

            _frmLog.AddToList("InitCan: " + ((USBcanServer.eUcanReturn)bRet).ToString());
            if (bRet != (byte)USBcanServer.eUcanReturn.USBCAN_SUCCESSFUL)
            {
                MessageBox.Show("InitCan error. " + (USBcanServer.eUcanReturn)bRet);
                return false;
            }

            ShowCanInfo();
            return true;
        }

        private void trackA5_30_Hor_ValueChanged(object sender, EventArgs e)
        {
            numA5_30_Hor.Value = trackA5_30_Hor.Value;
        }

        private void trackA5_30_Vert_ValueChanged(object sender, EventArgs e)
        {
            numA5_30_Vert.Value = trackA5_30_Vert.Value;
        }

        private void chkA5_41_CheckedChanged(object sender, EventArgs e)
        {
            if (chkA5_41.Checked)
            {
                SendMessage(Devices[selectedIndex], devPKP_BU, a5_41);
                btnA5_30_NULL.Enabled = false;
                btnA5_30.Enabled = false;
                numA5_30_Hor.Enabled = false;
                numA5_30_Vert.Enabled = false;
                trackA5_30_Hor.Enabled = false;
                trackA5_30_Vert.Enabled = false;
                label11.Enabled = false;
                label12.Enabled = false;
            }
            else
            {
                a5_30.Data[0] = (byte)((int)((double)numA5_30_Hor.Value / 0.0275));
                a5_30.Data[1] = (byte)((int)((double)numA5_30_Hor.Value / 0.0275) >> 8);
                a5_30.Data[2] = (byte)((int)((double)numA5_30_Vert.Value / 0.0275));
                a5_30.Data[3] = (byte)((int)((double)numA5_30_Vert.Value / 0.0275) >> 8);


                //label7.Text = String.Format("{0:X} - {1:X} - {2:X} - {3:X}", a5_29.Data[0], a5_29.Data[1], a5_29.Data[2], a5_29.Data[3]);

                SendMessage(Devices[selectedIndex], devPKP_BU, a5_30);

                btnA5_30_NULL.Enabled = true;
                btnA5_30.Enabled = true;
                numA5_30_Hor.Enabled = true;
                numA5_30_Vert.Enabled = true;
                trackA5_30_Hor.Enabled = true;
                trackA5_30_Vert.Enabled = true;
                label11.Enabled = true;
                label12.Enabled = true;
            }
        }

        private void timerSendValues_Tick(object sender, EventArgs e)
        {
            //Шлем данные каждый тик таймера только при их изменении. 
            //Для этого сравниваем значения с их прошлым значением для определения их изменения

            if ((numA5_30_Hor.Value != valuesScrolls.a5_30_Hor) || (numA5_30_Vert.Value != valuesScrolls.a5_30_Vert))
            //if ((Math.Abs(numA5_30_Hor.Value - valuesScrolls.a5_30_Hor) >= _frmSettings.numCoefSensHor.Value) || (Math.Abs(numA5_30_Vert.Value - valuesScrolls.a5_30_Vert) >= _frmSettings.numCoefSensVert.Value))
            {
                if (!chkA5_41.Checked) SendA5_30();

                valuesScrolls.a5_30_Vert = numA5_30_Vert.Value;
                valuesScrolls.a5_30_Hor = numA5_30_Hor.Value;

                
            }

            if ((_frmOther.numA5_34_Hor.Value != valuesScrolls.a5_34_Hor) || (_frmOther.numA5_34_Vert.Value != valuesScrolls.a5_34_Vert))
            {
                if (!_frmOther.chkNoScrollA5_34.Checked) SendA5_34();

                valuesScrolls.a5_34_Hor = _frmOther.numA5_34_Hor.Value;
                valuesScrolls.a5_34_Vert = _frmOther.numA5_34_Vert.Value;

            } 
        }

        private void btnA5_30_NULL_Click(object sender, EventArgs e)
        {
            numA5_30_Hor.Value = 0;
            numA5_30_Vert.Value = 0;
        }

        private void cboDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedIndex = cboDevices.SelectedIndex;
        }

        private void обнаружитьДжойстикToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if(myJoystick.InitJoystick())

            myJoystick.StartWorking();
        }

        private void информацияCANToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowCanInfo();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseMainForm(null);
            //Environment.Exit(0);
        }

        private void логToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _frmLog.Show();
        }

        private void разноеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _frmOther.Show();
        }

        private void панельУправленияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _frmPU.Show();
        }

        private void блокУправленияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _frmBU.Show();
        }

        private void дальномерToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _frmDalnomer.Show();
        }

        private void btnA5_36_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPKP_BU, a5_36);
        }

        private void btnA5_37_Click(object sender, EventArgs e)
        {
            SendMessage(Devices[selectedIndex], devPKP_BU, a5_37);
        }

        private void отладкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _frmDebug.Show();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseMainForm(e);
        }

        void CloseMainForm(FormClosingEventArgs e)
        {
            if (MessageBox.Show("Выйти из программы?", "MRO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification) == System.Windows.Forms.DialogResult.Cancel)
            {
                if(e!=null)
                    e.Cancel = true;
                return;
            }

            //_frmShutDown.Show();

            if (sets != null)
                SaveSettings();

            CanShutdown();

            while (tmrSend1540.IsRunning)
                tmrSend1540.Stop();


            try
            {
                sdi.DisconnectCapture();
                //CloseCurrentVideoSource();
                //Environment.Exit(0);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Environment.Exit(0);
        }

        private void CloseCurrentVideoSource()
        {
            if (videoSourcePlayer.VideoSource != null)
            {
                videoSourcePlayer.VideoSource.SignalToStop();
                //videoSourcePlayer.VideoSource.WaitForStop();

                /*// wait ~ 3 seconds
                for (int i = 0; i < 30; i++)
                {
                    if (!videoSourcePlayer.IsRunning)
                        break;
                    System.Threading.Thread.Sleep(100);
                }

                if (videoSourcePlayer.IsRunning)
                {
                    //videoSourcePlayer.Stop();
                }*/

                videoSourcePlayer.VideoSource = null;
            }
        }


        private void tmrSendA5_1_Tick(object sender, EventArgs e)
        {
            if(chkA5_1_ON.Checked)
                SendMessage(Devices[selectedIndex], devPKP_BU, a5_1);
        }


        #region Сохранение настроек в ХМЛ

        private void LoadSettings()
        {
            try
            {
                sets = LoadList(Application.StartupPath + "\\settings.xml");

                _frmSettings.numCoefKrutHor.Value =  Convert.ToDecimal(sets.CoeffKrutHor);
                _frmSettings.numCoefKrutVert.Value = Convert.ToDecimal(sets.CoefKrutVert);
                _frmSettings.numCoefSensHor.Value = Convert.ToDecimal(sets.CoefSensHor);
                _frmSettings.numCoefSensVert.Value = Convert.ToDecimal(sets.CoefSensVert);
            }
            catch
            {
                MessageBox.Show("Файл настроек XML не может быть загружен. Использованы параметры по умолчанию.", "Ошибка открытия файла", MessageBoxButtons.OK, MessageBoxIcon.Information);
                sets = new SettingXML();

                _frmSettings.numCoefKrutHor.Value = 1;
                _frmSettings.numCoefKrutVert.Value = 1;
                _frmSettings.numCoefSensHor.Value = 1;
                _frmSettings.numCoefSensVert.Value = 1;
            }
        }

        private void SaveSettings()
        {
            sets.CoeffKrutHor = _frmSettings.numCoefKrutHor.Value.ToString();
            sets.CoefKrutVert = _frmSettings.numCoefKrutVert.Value.ToString();
            sets.CoefSensHor = _frmSettings.numCoefSensHor.Value.ToString();
            sets.CoefSensVert = _frmSettings.numCoefSensVert.Value.ToString();

            SaveList(Application.StartupPath + "\\settings.xml", sets);
        }

        private SettingXML LoadList(string fileName)
        {
            XmlSerializer writer = new XmlSerializer(typeof(SettingXML));
            using (TextReader tr = new StreamReader(fileName))
            {
                return (SettingXML)writer.Deserialize(tr);
            }
        }

        private void SaveList(string fileName, SettingXML obj)
        {
            try
            {
                XmlSerializer writer = new XmlSerializer(typeof(SettingXML));
                using (TextWriter tw = new StreamWriter(fileName))
                {
                    writer.Serialize(tw, obj);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error occured while saving XML-file");
            }
        }

        #endregion

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _frmSettings.Show();
        }

        private void cmbA5_5_MarkColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            A5_5_Process();
        }

        private void numA5_5_DTK_ValueChanged(object sender, EventArgs e)
        {
            A5_5_Process();
        }

        private void tmrSendA5_3_Tick(object sender, EventArgs e)
        {
            if(flag_a5_3)
                SendMessage(Devices[selectedIndex], devPKP_BU, a5_3);
            else
                SendMessage(Devices[selectedIndex], devPKP_BU, a5_4);
            if(tmrSendA5_3.Interval > 100)
                tmrSendA5_3.Interval -= 100;
        }

        private void btnA5_3_MouseDown(object sender, MouseEventArgs e)
        {
            if ((Button)sender == btnA5_3)
            {
                SendMessage(Devices[selectedIndex], devPKP_BU, a5_3);
                flag_a5_3 = true;
            }
            else if ((Button)sender == btnA5_4)
            {
                SendMessage(Devices[selectedIndex], devPKP_BU, a5_4);
                flag_a5_3 = false;
            }
            tmrSendA5_3.Interval = 400;
            tmrSendA5_3.Start();
        }

        private void btnA5_3_MouseUp(object sender, MouseEventArgs e)
        {
            tmrSendA5_3.Stop();
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.CloseMainForm(null);
            }
        }

        private void chkA5_29_CheckedChanged(object sender, EventArgs e)
        {
            /*if (chkA5_29.Checked) btnA5_29.Enabled = true;
            else btnA5_29.Enabled = false;*/
            btnA5_29.Enabled = chkA5_29.Checked ? true : false;
        }

        private void trackA5_29_Hor_MouseUp(object sender, MouseEventArgs e)
        {
            if(!chkA5_29.Checked)
                SendA5_29();
        }
    }

    struct Values
    {
        public decimal a5_30_Hor;
        public decimal a5_30_Vert;
        public decimal a5_34_Hor;
        public decimal a5_34_Vert;
    }
}
