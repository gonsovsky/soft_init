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

namespace prototypeExpander
{
 
    public class Prototype : ObjectInstance
    {
        public Prototype(ObjectInstance prototype)
            : base(prototype)
        {
            this.PopulateFunctions();
        }
 

        [JSFunction(Name = "installService")]
        public string InstallService(string servicFile, string serviceName)
        {
            servicFile = Path.GetFullPath(servicFile);
            try
            {
                return new NtService()
                    .InstallService(servicFile, serviceName);
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }
    }
}
