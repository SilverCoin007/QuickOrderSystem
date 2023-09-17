﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickOrderSystemAdminApp.Data
{
    internal class QrCode
    {
        public int ID { get; set; }
        public int TableNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime Created { get; set; }
        public byte[] ImageData { get; set; }
    }
}
