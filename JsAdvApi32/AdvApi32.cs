using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jurassic;
using Jurassic.Library;
using System.Security.Authentication.ExtendedProtection;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using JsMan;
using System.ComponentModel.DataAnnotations;

namespace JsdvApi32
{
    public class AdvApi32 : JsApi
    {
        public AdvApi32(ObjectInstance prototype) : base(prototype) { }

        [JSFunction(Name = "createService")]
        public CreateServiceOut CreateService(CreateServiceIn x)
        {
            x.Norm();
            var results = new List<ValidationResult>();
            var context = new ValidationContext(x);
            if (!Validator.TryValidateObject(x, context, results, true))
                return new CreateServiceOut(Engine.Object)
                {
                    Result = false,
                    Message = string.Join(". ", results.Select(a => a.ErrorMessage)),
                    ErrorCode = -1
                };

            int SC_MANAGER_CREATE_SERVICE = 0x0002;
            IntPtr sc_handle = OpenSCManager(null, null, SC_MANAGER_CREATE_SERVICE);
            try
            {
                if (sc_handle.ToInt32() == 0)
                    return new CreateServiceOut(Engine.Object)
                    {
                        Result = false,
                        Message = $"OpenSCManager failure.",
                        ErrorCode = GetLastError()
                    };

                IntPtr sv_handle = CreateService(sc_handle, x.lpSvcName, x.lpDisplayName,
                        x.dwDesiredAccess, x.dwServiceType, x.dwStartType, x.dwErrorControl, x.lpPathName,
                        x.lpLoadOrderGroup, x.lpdwTagId, x.lpDependencies, x.lpServiceStartName, x.lpPassword);
                if (sv_handle.ToInt32() == 0)
                    return new CreateServiceOut(Engine.Object)
                    {
                        Result = false,
                        Message = $"CreateService failure.",
                        ErrorCode = GetLastError()
                    };

                return new CreateServiceOut(Engine.Object)
                {
                    Result = true,
                    Message = $"ok",
                    ErrorCode = 0
                };
            }
            finally
            {
                if (sc_handle != IntPtr.Zero)
                    CloseServiceHandle(sc_handle);
            }
        }

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr OpenSCManager(string lpMachineName, string lpSCDB, int scParameter);
        [DllImport("Advapi32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr CreateService(IntPtr SC_HANDLE, string lpSvcName, string lpDisplayName,
        int dwDesiredAccess, int dwServiceType, int dwStartType, int dwErrorControl, string lpPathName,
        string lpLoadOrderGroup, int lpdwTagId, string lpDependencies, string lpServiceStartName, string lpPassword);
        [DllImport("advapi32.dll")]
        private static extern void CloseServiceHandle(IntPtr SCHANDLE);
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
        private static extern int StartService(IntPtr SVHANDLE, int dwNumServiceArgs, string lpServiceArgVectors);
        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr OpenService(IntPtr SCHANDLE, string lpSvcName, int dwNumServiceArgs);
        [DllImport("advapi32.dll")]
        private static extern int DeleteService(IntPtr SVHANDLE);
        [DllImport("kernel32.dll")]
        private static extern int GetLastError();
    }
}