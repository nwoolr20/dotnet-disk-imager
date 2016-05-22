﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace dotNetDiskImager.Models
{
    public class Utils
    {
        [DllImport("kernel32.dll")]
        static extern uint SetThreadExecutionState(uint esFlags);
        const uint ES_CONTINUOUS = 0x80000000;
        const uint ES_SYSTEM_REQUIRED = 0x00000001;

        public static bool CheckMappedDrivesEnable()
        {
            try
            {
                int value = (int)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System", "EnableLinkedConnections", -1);
                if (value == 1)
                {
                    return true;
                }
            }
            catch { }

            return false;
        }

        public static bool SetMappedDrivesEnable()
        {
            try
            {
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System", "EnableLinkedConnections", 1, RegistryValueKind.DWord);
                return true;
            }
            catch { }
            return false;
        }

        public static bool PreventComputerSleep()
        {
            return (SetThreadExecutionState(ES_CONTINUOUS | ES_SYSTEM_REQUIRED) != 0);
        }

        public static bool AllowComputerSleep()
        {
            return (SetThreadExecutionState(ES_CONTINUOUS) != 0);
        }
    }
}
