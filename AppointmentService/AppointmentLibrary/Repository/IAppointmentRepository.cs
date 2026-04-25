using System;
using AppointmentLibrary.Entities;

namespace AppointmentLibrary.Repository;

public interface IAppointmentRepository
{
    Task<Appointment?> GetByIdAsync(Guid appointmentId);
    Task<IEnumerable<Appointment>> GetByPatientIdAsync(string patientId);
    Task<IEnumerable<Appointment>> GetByDoctorIdAsync(string doctorId);
    Task AddAsync(Appointment appointment);
    Task UpdateAsync(Appointment appointment);

}
