using JsMan;
using Jurassic;
using Jurassic.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using JsMan;

namespace JsdvApi32
{
    public class CreateServiceIn : ObjectInstance, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (lpSvcName.IsNullOrEmptyJS() == true)
                errors.Add(new ValidationResult("lpSvcName is null or empty"));
            if (System.IO.File.Exists(lpPathName) == false)
                errors.Add(new ValidationResult("lpPathName is not exists"));
            if (dwDesiredAccess != SERVICE_ALL_ACCESS)
                errors.Add(new ValidationResult("dwDesiredAccess is not valid"));
            if (dwServiceType != SERVICE_WIN32_OWN_PROCESS)
                errors.Add(new ValidationResult("dwServiceType is not valid"));
            if (dwStartType != SERVICE_AUTO_START)
                errors.Add(new ValidationResult("dwStartType is not valid"));
            if (dwErrorControl != SERVICE_ERROR_NORMAL)
                errors.Add(new ValidationResult("dwErrorControl is not valid"));
            return errors;
        }

        public void Norm()
        {
            var props = GetType().GetProperties()
                .Where(prop => prop.IsDefined(typeof(JsNullAttribute), false));
            foreach (var propertyInfo in props)
            {
                var x = propertyInfo.GetValue(this);
                if (x != null && x.ToString().IsNullOrEmptyJS())
                propertyInfo.SetValue(this, null);
            }
        }


        [JSProperty]
        [JsNull]
        public string lpSvcName { get; set; }

        [JSProperty]
        [JsNull]
        public string lpDisplayName { get; set; }

        #region DesiredAccess
        [JSProperty]
        public int dwDesiredAccess { get; set; }

        static int STANDARD_RIGHTS_REQUIRED = 0xF0000;
        static int SERVICE_QUERY_CONFIG = 0x0001;
        static int SERVICE_CHANGE_CONFIG = 0x0002;
        static int SERVICE_QUERY_STATUS = 0x0004;
        static int SERVICE_ENUMERATE_DEPENDENTS = 0x0008;
        static int SERVICE_START = 0x0010;
        static int SERVICE_STOP = 0x0020;
        static int SERVICE_PAUSE_CONTINUE = 0x0040;
        static int SERVICE_INTERROGATE = 0x0080;
        static int SERVICE_USER_DEFINED_CONTROL = 0x0100;

        [JSField]
        public int SERVICE_ALL_ACCESS =
                STANDARD_RIGHTS_REQUIRED |
                SERVICE_QUERY_CONFIG |
                SERVICE_CHANGE_CONFIG |
                SERVICE_QUERY_STATUS |
                SERVICE_ENUMERATE_DEPENDENTS |
                SERVICE_START |
                SERVICE_STOP |
                SERVICE_PAUSE_CONTINUE |
                SERVICE_INTERROGATE |
                SERVICE_USER_DEFINED_CONTROL;
        #endregion

        #region ServiceType
        [JSProperty]
        public int dwServiceType { get; set; }

        [JSField]
        public int SERVICE_WIN32_OWN_PROCESS = 0x00000010;
        #endregion

        #region StartType
        [JSProperty]
        public int dwStartType { get; set; }

        [JSField]
        public int SERVICE_AUTO_START = 0x00000002;
        #endregion

        #region ErrorControl
        [JSProperty]
        public int dwErrorControl { get; set; }

        [JSField]
        public int SERVICE_ERROR_NORMAL = 0x00000001;
        #endregion

        [JSProperty]
        [JsNull]
        public string lpPathName { get; set; }
        [JSProperty]
        public string lpLoadOrderGroup { get; set; }
        [JSProperty]
        public int lpdwTagId { get; set; }
        [JSProperty]
        [JsNull]
        public string lpDependencies { get; set; }
        [JSProperty]
        [JsNull]
        public string lpServiceStartName { get; set; }
        [JSProperty]
        [JsNull]
        public string lpPassword { get; set; }

        public CreateServiceIn(ObjectInstance prototype)
            : base(prototype)
        {
            this.PopulateFunctions();
            this.PopulateFields();
        }
    }
}
