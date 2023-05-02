using RemindMe.Classes;
using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemindMe
{
    internal class Client
    {
        FirebaseClient firebase =
            new FirebaseClient("------------your Firebase Database source link------------");
        public async Task<bool> Save(DoctorInfo doctor)
        {
            await firebase.Child(nameof(DoctorInfo) + "/" + doctor.TCKNO).PutAsync(JsonConvert.SerializeObject(doctor));
            return true;
        }
        public async Task<bool> SavePatient(PatientInfo patient)
        {
            await firebase.Child(nameof(PatientInfo) + "/" + patient.TCKNO).PutAsync(JsonConvert.SerializeObject(patient));
            return true;
        }
        internal async Task<bool> isThereDoctor(string tckno, string email)
        {
            return (await firebase.Child(nameof(DoctorInfo)).OnceAsync<DoctorInfo>())
                .Any(x => x.Object.TCKNO == tckno || x.Object.Mail == email);
        }
        internal async Task<bool> isTherePatient(string tckno, string email)
        {
            return (await firebase.Child(nameof(PatientInfo)).OnceAsync<PatientInfo>())
                .Any(x => x.Object.TCKNO == tckno || x.Object.Mail == email);
        }
        internal async Task<bool> isThereNewPatient(string tckno)
        {
            return (await firebase.Child(nameof(PatientInfo)).OnceAsync<PatientInfo>())
                .Any(x => x.Object.TCKNO == tckno || !string.IsNullOrEmpty(x.Object.DoctorTCKNO));
        }
        internal async Task<bool> isLogDoctor(string tckno, string password)
        {
            return (await firebase.Child(nameof(DoctorInfo)).OnceAsync<DoctorInfo>())
                .Any(x => x.Object.TCKNO == tckno && x.Object.password == password);
        }
        internal async Task<bool> isLogPatient(string tckno, string password)
        {
            return (await firebase.Child(nameof(PatientInfo)).OnceAsync<PatientInfo>())
                .Any(x => x.Object.TCKNO == tckno && x.Object.password == password);
        }
        internal async Task<DoctorInfo> DoctorGetMe(string mytckno)
        {
            return (await firebase.Child(nameof(DoctorInfo) + "/" + mytckno).OnceSingleAsync<DoctorInfo>());
        }
        internal async Task<List<PatientInfo>> GetAllPatient(string tckno)
        {
            return (await firebase.Child(nameof(PatientInfo)).OnceAsync<PatientInfo>())
                .Where(x => x.Object.DoctorTCKNO == tckno).Select(item => new PatientInfo
                {
                    Ad_Soyad = item.Object.Ad_Soyad,
                    TCKNO = item.Object.TCKNO,
                    ImageURL = item.Object.ImageURL,
                    TelNo = item.Object.TelNo,
                    Mail = item.Object.Mail,
                    password = item.Object.password,
                    BirthDate = item.Object.BirthDate,
                    CV = item.Object.CV,
                    Medicine = item.Object.Medicine,
                    MedicineTime = item.Object.MedicineTime,
                    Status = item.Object.Status,
                }).ToList();
        }
        public async Task<PatientInfo> GetPatient(string mytckno)
        {
            return (await firebase.Child(nameof(PatientInfo) + "/" + mytckno).OnceSingleAsync<PatientInfo>());
        }
        internal async Task<bool> addNewPatient_1(PatientInfo patient, string tckno)
        {
            patient.DoctorTCKNO = tckno;
            await firebase.Child(nameof(PatientInfo) + "/" + patient.TCKNO)
                .PutAsync(JsonConvert.SerializeObject(patient));
            return true;
        }
        internal async Task<PatientInfo> addNewPatient(string tckno)
        {
            return (await firebase.Child(nameof(PatientInfo)).OnceAsync<PatientInfo>())
                .Where(x => x.Object.TCKNO == tckno).Select(item => new PatientInfo
                {
                    Ad_Soyad = item.Object.Ad_Soyad,
                    TCKNO = item.Object.TCKNO,
                    ImageURL = item.Object.ImageURL,
                    TelNo = item.Object.TelNo,
                    Mail = item.Object.Mail,
                    password = item.Object.password,
                    BirthDate = item.Object.BirthDate,
                    CV = item.Object.CV,
                    Medicine = item.Object.Medicine,
                    MedicineTime = item.Object.MedicineTime,
                    Status = item.Object.Status,
                }).ToList()[0];

        }
        internal async Task<bool> DeletePatient(PatientInfo patient)
        {
            patient.DoctorTCKNO = null;
            await firebase.Child(nameof(PatientInfo) + "/" + patient.TCKNO)
                .PutAsync(JsonConvert.SerializeObject(patient));
            return true;
        }
    }
}
