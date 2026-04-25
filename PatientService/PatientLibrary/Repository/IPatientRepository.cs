using System;
using PatientLibrary.Entities;

namespace PatientLibrary.Repository;

public interface IPatientRepository
{
    /// <summary>
    /// Adding new Patient in database
    /// </summary>
    /// <param name="patient">An object of Patient details</param>
    /// <returns></returns>
    Task AddAsync(Patient patient);

    /// <summary>
    /// Retrieve Patient from database
    /// </summary>
    /// <param name="patientId">A Unique identitfied of string type</param>
    /// <returns>Return Patient object if found, else return null</returns>
    Task<Patient?> GetByIdAsync(string patientId);

    /// <summary>
    /// Retrieve a collection of Patient from database
    /// </summary>
    /// <returns>A collection of available Patient, else empty collection</returns>
    Task<IEnumerable<Patient>> GetAllAsync();

    /// <summary>
    /// Update record of patient in database
    /// </summary>
    /// <param name="patient">An object of patient</param>
    /// <returns></returns>
    Task UpdateAsync(Patient patient);

    /// <summary>
    /// Soft delete of patient data from database (changing the status only)
    /// </summary>
    /// <param name="patientId">A unique identifier of patient</param>
    /// <returns></returns>
    Task DeleteAsync(string patientId);
}
