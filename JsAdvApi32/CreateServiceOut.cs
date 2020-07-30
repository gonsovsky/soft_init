using Jurassic;
using Jurassic.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsdvApi32
{
    public class CreateServiceOut : ObjectInstance
    {
        [JSProperty(Name = "result")]
        public bool Result { get; set; }

        [JSProperty(Name = "message")]
        public string Message { get; set; }

        [JSProperty(Name = "errorCode")]
        public int ErrorCode { get; set; }

        public CreateServiceOut(ObjectInstance prototype)
            : base(prototype)
        {
            this.PopulateFunctions();
        }
    }
}