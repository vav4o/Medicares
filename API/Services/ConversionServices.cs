using API.DTOs;
using Data.Entities;
using Humanizer;

namespace API.Services
{
    public class ConversionServices
    {
        public static Doctor DoctorToEntity(DoctorDTO dto) 
        { 
            return new Doctor()
            {
                Id = dto.Id,
                FName = dto.FName,
                LName = dto.LName,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                Salary = dto.Salary,
                HireDate = dto.HireDate,
                Username = dto.Username,
                Password = dto.Password
            }; 
        }
        public static Medication MedicationToEntity(MedicationDTO dto)
        {
            return new Medication()
            {
                Id = dto.Id,
                Name = dto.Name,
                Manufacturer = dto.Manufacturer,
                Type = dto.Type,
                Dosage = dto.Dosage,
                Price = dto.Price,
                ExpirationDate = dto.ExpirationDate
            };
        }
        public static Patient PatientToEntity(PatientDTO dto)
        {
            return new Patient()
            {
                Id = dto.Id,
                FName = dto.FName,
                LName = dto.LName,
                Gender = dto.Gender,
                BirthDate = dto.BirthDate,
                DoctorId = dto.DoctorId,
                MedicationId = dto.MedicationId
            };
        }
        public static DoctorDTO DoctorToDTO (Doctor doctor)
        {
            return new DoctorDTO()
            {
                Id = doctor.Id,
                FName = doctor.FName,
                LName = doctor.LName,
                PhoneNumber = doctor.PhoneNumber,
                Email = doctor.Email,
                Salary = doctor.Salary,
                HireDate = doctor.HireDate,
                Username = doctor.Username,
                Password = doctor.Password
            };
        }
        public static List<DoctorDTO> DoctorToDTO(List<Doctor> doctors)
        {
            List<DoctorDTO> doctorDTOs = new List<DoctorDTO>();
            foreach (Doctor doctor in doctors)
            {
                doctorDTOs.Add(DoctorToDTO(doctor));
            }
            return doctorDTOs;
        }
        public static List<MedicationDTO> MedicationToDTO(List<Medication> medications)
        {
            List<MedicationDTO> medicationDTOs = new List<MedicationDTO>();
            foreach (Medication medication in medications)
            {
                medicationDTOs.Add(MedicationToDTO(medication));
            }
            return medicationDTOs;
        }
        public static List<PatientDTO> PatientToDTO(List<Patient> patients)
        {
            List<PatientDTO> patientDTOs = new List<PatientDTO>();
            foreach (Patient patient in patients)
            {
                patientDTOs.Add(PatientToDTO(patient));
            }
            return patientDTOs;
        }
        public static MedicationDTO MedicationToDTO(Medication medication)
        {
            return new MedicationDTO()
            {
                Id = medication.Id,
                Name = medication.Name,
                Manufacturer = medication.Manufacturer,
                Type = medication.Type,
                Dosage = medication.Dosage,
                Price = medication.Price,
                ExpirationDate = medication.ExpirationDate
            };
        }
        public static PatientDTO PatientToDTO(Patient patient)
        {
            return new PatientDTO()
            {
                Id = patient.Id,
                FName = patient.FName,
                LName = patient.LName,
                Gender = patient.Gender,
                BirthDate = patient.BirthDate,
                DoctorId = patient.DoctorId,
                MedicationId = patient.MedicationId
            };
        }
    }
}
