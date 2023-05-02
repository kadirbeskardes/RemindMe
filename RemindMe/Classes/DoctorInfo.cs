using System;
using System.Collections.Generic;
using System.Text;

namespace RemindMe.Classes
{
    public class DoctorInfo
    {
        // [PrimaryKey, AutoIncrement]
        public string TCKNO { get; set; }
        // public int Id { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public string HospitalName { get; set; }
        public string TelNo { get; set; }
        public string Mail { get; set; }

        public string CV { get; set; }

        public string password { get; set; }
    }
}
