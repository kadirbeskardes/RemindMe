using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace RemindMe.Classes
{
    internal class PatientInfo
    {
        // public int Id { get; set; }
        public string Ad_Soyad { get; set; }
        public string TCKNO { get; set; }
        // public List<string> ilac { get; set; }
        public string ImageURL { get; set; }
        public string TelNo { get; set; }
        public string Mail { get; set; }
        public string Status { get; set; }
        public string DoctorTCKNO { get; set; }
        public DateTime BirthDate { get; set; }
        public string password { get; set; }
        public string CV { get; set; }

        public ObservableCollection<string> Medicine { get; set; }
        public ObservableCollection<string> MedicineTime { get; set; }
    }
}
