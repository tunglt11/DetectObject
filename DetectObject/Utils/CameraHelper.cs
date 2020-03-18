using GeneralDef;
using NETSDKHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DetectObject.Utils
{
    public class CameraHelper
    {
        public List<PlayPanel> playPanels;
        public List<DeviceInfo> deviceInfos;

        public CameraHelper(List<PlayPanel> playPanels, List<DeviceInfo> deviceInfos)
        {
            this.playPanels = playPanels;
            this.deviceInfos = deviceInfos;
            int iRet = NETDEVSDK.NETDEV_Init();
            if (NETDEVSDK.TRUE != iRet)
            {
                MessageBox.Show("it is not a admin oper");
            }
            SetSavePath();
        }

        public CameraHelper()
        {
            playPanels = new List<PlayPanel>();
            deviceInfos = new List<DeviceInfo>();
        }

        public void SetupCamera(string cameraName)
        {
            var deviceInfo = GetDeviceInfo(cameraName);
            Task.Run(() =>
            {
                LoginLocalDevice(deviceInfo);
            });
        }

        public void AddCamera(TableLayoutPanel tableLayoutPanel, string cameraName, Point point)
        {
            var playPanel = FindPlayPanel(cameraName);
            if (playPanel != null)
            {
                //xóa play panel cũ
                playPanels.Remove(playPanel);
            }

            playPanel = new PlayPanel();
            playPanel.Padding = new Padding(0);
            playPanel.Margin = new Padding(0);
            playPanel.BackColor = Color.Black;
            playPanel.Name = cameraName;
            playPanel.Dock = DockStyle.Fill;
            playPanel.setBorderColor(Color.White, 1);

            playPanels.Add(playPanel);
            tableLayoutPanel.Controls.Clear();
            if (tableLayoutPanel.InvokeRequired)
            {
                tableLayoutPanel.BeginInvoke((MethodInvoker)delegate ()
                {
                    tableLayoutPanel.Controls.Add(playPanel, point.X, point.Y);
                });
            }
            else
            {
                tableLayoutPanel.Controls.Add(playPanel, point.X, point.Y);
            }
        }

        //login local device
        private void LoginLocalDevice(DeviceInfo deviceInfo)
        {
            //NETDEMO.NETDEV_LOGIN_TYPE_E loginFlag = NETDEMO.NETDEV_LOGIN_TYPE_E.NETDEV_NEW_LOGIN;
            //int DeviceNodeIndex = 0;
            for (int i = 0; i < deviceInfos.Count(); i++)
            {
                if (NETDEMO.NETDEMO_DEVICE_TYPE_E.NETDEMO_DEVICE_INVALID == deviceInfos[i].m_eDeviceType)
                {
                    continue;
                }

                if (deviceInfo.m_ip == deviceInfos[i].m_ip
                    && deviceInfo.m_port == deviceInfos[i].m_port
                    && deviceInfos[i].m_lpDevHandle != IntPtr.Zero)
                {
                    //MessageBox.Show("The device already exists!");
                    return;
                }

                //if (deviceInfo.m_ip == deviceInfos[i].m_ip
                //    && deviceInfo.m_port == deviceInfos[i].m_port
                //    && deviceInfos[i].m_lpDevHandle == IntPtr.Zero)//again login
                //{
                //    loginFlag = NETDEMO.NETDEV_LOGIN_TYPE_E.NETDEV_AGAIN_LOGIN;
                //    DeviceNodeIndex = i;
                //}
            }

            NETDEV_DEVICE_LOGIN_INFO_S pstDevLoginInfo = new NETDEV_DEVICE_LOGIN_INFO_S();
            NETDEV_SELOG_INFO_S pstSELogInfo = new NETDEV_SELOG_INFO_S();
            pstDevLoginInfo.szIPAddr = deviceInfo.m_ip;
            pstDevLoginInfo.dwPort = deviceInfo.m_port;
            pstDevLoginInfo.szUserName = deviceInfo.m_userName;
            pstDevLoginInfo.szPassword = deviceInfo.m_password;
            pstDevLoginInfo.dwLoginProto = (int)NETDEV_LOGIN_PROTO_E.NETDEV_LOGIN_PROTO_ONVIF;

            IntPtr lpDevHandle = NETDEVSDK.NETDEV_Login_V30(ref pstDevLoginInfo, ref pstSELogInfo);
            if (lpDevHandle == IntPtr.Zero)
            {
                return;
            }

            //if (loginFlag == NETDEMO.NETDEV_LOGIN_TYPE_E.NETDEV_AGAIN_LOGIN)
            //{
            //    deviceInfos[DeviceNodeIndex].m_lpDevHandle = lpDevHandle;
            //}

            DeviceInfo deviceInfoTemp = new DeviceInfo();
            deviceInfoTemp.m_lpDevHandle = lpDevHandle;
            deviceInfoTemp.m_ip = deviceInfo.m_ip;
            deviceInfoTemp.m_port = deviceInfo.m_port;
            deviceInfoTemp.m_userName = deviceInfo.m_userName;
            deviceInfoTemp.m_password = deviceInfo.m_password;
            deviceInfoTemp.m_eDeviceType = deviceInfo.m_eDeviceType;
            deviceInfoTemp.m_cameraName = deviceInfo.m_cameraName;

            int iRet;
            //get the channel list
            int pdwChlCount = 256;
            IntPtr pstVideoChlList = new IntPtr();
            //pstVideoChlList = Marshal.AllocHGlobal(NETDEVSDK.NETDEV_LEN_32 * Marshal.SizeOf(typeof(NETDEV_VIDEO_CHL_DETAIL_INFO_S)));
            pstVideoChlList = Marshal.AllocHGlobal(256 * Marshal.SizeOf(typeof(NETDEV_VIDEO_CHL_DETAIL_INFO_S)));
            iRet = NETDEVSDK.NETDEV_QueryVideoChlDetailList(deviceInfoTemp.m_lpDevHandle, ref pdwChlCount, pstVideoChlList);
            if (NETDEVSDK.TRUE == iRet)
            {
                deviceInfoTemp.m_channelNumber = pdwChlCount;
                NETDEV_VIDEO_CHL_DETAIL_INFO_S stCHLItem = new NETDEV_VIDEO_CHL_DETAIL_INFO_S();
                for (int i = 0; i < pdwChlCount; i++)
                {
                    IntPtr ptrTemp = new IntPtr(pstVideoChlList.ToInt64() + Marshal.SizeOf(typeof(NETDEV_VIDEO_CHL_DETAIL_INFO_S)) * i);
                    stCHLItem = (NETDEV_VIDEO_CHL_DETAIL_INFO_S)Marshal.PtrToStructure(ptrTemp, typeof(NETDEV_VIDEO_CHL_DETAIL_INFO_S));

                    ChannelInfo channelInfo = new ChannelInfo();
                    channelInfo.m_devVideoChlInfo = stCHLItem;
                    deviceInfoTemp.m_channelInfoList.Add(channelInfo);
                    deviceInfoTemp.m_channelNumber = deviceInfos.Count + 1;
                }
                deviceInfos.Add(deviceInfoTemp);
            }

            Marshal.FreeHGlobal(pstVideoChlList);
            NETDEV_DEVICE_INFO_S pstDevInfo = new NETDEV_DEVICE_INFO_S();
            NETDEVSDK.NETDEV_GetDeviceInfo(deviceInfoTemp.m_lpDevHandle, ref pstDevInfo);
            deviceInfoTemp.m_stDevInfo = pstDevInfo;
        }

        private DeviceInfo GetDeviceInfo(string cameraName)
        {
            DeviceInfo deviceInfo = null;
            var config = ConfigurationSettings.AppSettings.Get(cameraName);
            if (!string.IsNullOrEmpty(config))
            {
                config = config.Replace("rtsp://", "");
                var arrayConfig = config.Split('@');
                var nameInfor = arrayConfig[0].Split(':');
                var ipInfor = arrayConfig[1].Split(':');

                deviceInfo = new DeviceInfo();
                deviceInfo.m_userName = nameInfor[0];
                deviceInfo.m_password = nameInfor[1];
                deviceInfo.m_ip = ipInfor[0];
                deviceInfo.m_port = Convert.ToInt16(ipInfor[1]);
                deviceInfo.m_cameraName = cameraName;
                deviceInfo.m_eDeviceType = NETDEMO.NETDEMO_DEVICE_TYPE_E.NETDEMO_DEVICE_IPC_OR_NVR;
            }

            return deviceInfo;
        }

        private PlayPanel FindPlayPanel(string cameraName)
        {
            foreach (var playPanel in playPanels)
            {
                if (playPanel.Name == cameraName)
                {
                    return playPanel;
                }
            }

            return null;
        }

        public DeviceInfo FindDeviceInfo(string cameraName)
        {
            foreach (var device in deviceInfos)
            {
                if (device.m_cameraName == cameraName)
                {
                    return device;
                }
            }

            return null;
        }

        public void StartRealPlay(string cameraName)
        {
            var curRealPanel = FindPlayPanel(cameraName);
            var deviceInfo = FindDeviceInfo(cameraName);
            if (curRealPanel != null && deviceInfo != null)
            {
                curRealPanel.initPlayPanel();
                NETDEV_PREVIEWINFO_S stPreviewInfo = new NETDEV_PREVIEWINFO_S();
                stPreviewInfo.dwLinkMode = (int)NETDEV_PROTOCAL_E.NETDEV_TRANSPROTOCAL_RTPTCP;
                stPreviewInfo.dwChannelID = 1;
                stPreviewInfo.dwStreamType = (int)NETDEV_LIVE_STREAM_INDEX_E.NETDEV_LIVE_STREAM_INDEX_MAIN;
                stPreviewInfo.hPlayWnd = curRealPanel.Handle;
                IntPtr Handle = NETDEVSDK.NETDEV_RealPlay(deviceInfo.m_lpDevHandle, ref stPreviewInfo, IntPtr.Zero, IntPtr.Zero);
                if (Handle == IntPtr.Zero)
                {
                    return;
                }
                curRealPanel.m_playStatus = true;
                curRealPanel.m_playhandle = Handle;

                //NETDEVSDK.NETDEV_SetIVAEnable(Handle, 1);
                //NETDEVSDK.NETDEV_SetIVAShowParam(7);
            }
        }

        public bool StopRealPlay(string cameraName)
        {
            var playPanel = FindPlayPanel(cameraName);
            if (playPanel != null)
            {
                if (IntPtr.Zero == playPanel.m_playhandle)
                {
                    return false;
                }

                if (NETDEVSDK.FALSE == NETDEVSDK.NETDEV_StopRealPlay(playPanel.m_playhandle))
                {
                    return false;
                }
            }

            return true;
        }

        public Bitmap TakeSnapshot(string cameraName)
        {
            var curRealPanel = FindPlayPanel(cameraName);
            var deviceInfo = FindDeviceInfo(cameraName);
            Bitmap bitmap = null;
            if (curRealPanel != null && curRealPanel != null)
            {
                String strTemp = string.Copy(LocalSetting.m_strPicSavePath);
                DateTime oDate = DateTime.Now;
                String strCurTime = oDate.ToString("yyMMddHHmmss", DateTimeFormatInfo.InvariantInfo);
                LocalSetting.m_strPicSavePath += "\\";
                LocalSetting.m_strPicSavePath += deviceInfo.m_ip;
                LocalSetting.m_strPicSavePath += "_";
                LocalSetting.m_strPicSavePath += cameraName;
                LocalSetting.m_strPicSavePath += "_";
                LocalSetting.m_strPicSavePath += strCurTime;

                byte[] picSavePath;
                GetUTF8Buffer(LocalSetting.m_strPicSavePath, NETDEVSDK.NETDEV_LEN_260, out picSavePath);

                int iRet = NETDEVSDK.NETDEV_CapturePicture(curRealPanel.m_playhandle, picSavePath, (int)NETDEV_PICTURE_FORMAT_E.NETDEV_PICTURE_JPG);
                //showSuccessLogInfo(m_deviceInfoList[m_CurSelectTreeNodeInfo.dwDeviceIndex].m_ip + " chl:" + (getChannelID()), "CapturePicture");
                bitmap = (Bitmap)Image.FromFile(LocalSetting.m_strPicSavePath + ".jpg");
                LocalSetting.m_strPicSavePath = strTemp;
            }

            return bitmap;
        }

        public void StartRecord(string cameraName)
        {
            var curRealPanel = FindPlayPanel(cameraName);
            var deviceInfo = FindDeviceInfo(cameraName);
            if (curRealPanel.m_playStatus == false)
            {
                return;
            }

            if (curRealPanel.m_recordStatus == false)
            {
                String temp = string.Copy(LocalSetting.m_strRecordSavePath);
                LocalSetting.m_strRecordSavePath += CommonFunc.FormatFileNameVideo();

                byte[] localRecordPath;
                GetUTF8Buffer(LocalSetting.m_strRecordSavePath, NETDEVSDK.NETDEV_LEN_260, out localRecordPath);
                int iRet = NETDEVSDK.NETDEV_SaveRealData(curRealPanel.m_playhandle, localRecordPath, (int)NETDEV_MEDIA_FILE_FORMAT_E.NETDEV_MEDIA_FILE_MP4);
                if (NETDEVSDK.FALSE == iRet)
                {
                    return;
                }
                curRealPanel.m_recordStatus = true;
                LocalSetting.m_strRecordSavePath = temp;
            }
        }

        public void StopRecord(string cameraName)
        {
            var curRealPanel = FindPlayPanel(cameraName);
            if (curRealPanel != null)
            {
                if (curRealPanel.m_playStatus == false)
                {
                    return;
                }

                if (curRealPanel.m_recordStatus)
                {
                    int iRet = NETDEVSDK.NETDEV_StopSaveRealData(curRealPanel.m_playhandle);

                    if (NETDEVSDK.FALSE == iRet)
                    {
                        return;
                    }
                    curRealPanel.m_recordStatus = false;
                }
            }
        }

        public bool IsSetupBefore()
        {
            return deviceInfos.Count > 0;
        }

        private void GetUTF8Buffer(string inputString, int bufferLen, out byte[] utf8Buffer)
        {
            utf8Buffer = new byte[bufferLen];
            byte[] tempBuffer = System.Text.Encoding.UTF8.GetBytes(inputString);
            for (int i = 0; i < tempBuffer.Length; ++i)
            {
                utf8Buffer[i] = tempBuffer[i];
            }
        }

        public void SetSavePath()
        {
            String m_currentPath = Application.StartupPath;
            string pictureSavePath = m_currentPath + "\\Pic\\";
            string recordSavePath = m_currentPath + "\\Record\\";
            string scannedPath = m_currentPath + "\\Scanned\\";
            LocalSetting.setPath(pictureSavePath, recordSavePath, scannedPath);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

    public class LocalSetting
    {
        public static string m_strPicSavePath = null;
        public static string m_strRecordSavePath = null;
        public static string m_strScannedPath = null;
        public static void setPath(string picturePath, string recordPath, string scannedPath)
        {
            try
            {
                if (!Directory.Exists(picturePath))
                {
                    Directory.CreateDirectory(picturePath);
                }

                if (!Directory.Exists(recordPath))
                {
                    Directory.CreateDirectory(recordPath);
                }

                if (!Directory.Exists(scannedPath))
                {
                    Directory.CreateDirectory(scannedPath);
                }

                m_strPicSavePath = picturePath;
                m_strRecordSavePath = recordPath;
                m_strScannedPath = scannedPath;
            }
            catch (Exception)
            {
                MessageBox.Show("create path fail", "warning");
            }
        }
    }
}
