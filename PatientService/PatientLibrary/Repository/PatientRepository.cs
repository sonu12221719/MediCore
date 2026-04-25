using System;
using Microsoft.EntityFrameworkCore;
using PatientLibrary.Data;
using PatientLibrary.Entities;
using PatientLibrary.Exceptions;

namespace PatientLibrary.Repository;

public class PatientRepository : IPatientRepository
{
    private readonly PatientDbContext _context;
    public PatientRepository(PatientDbContext context)
    {
        _context=context;
    }
    public async Task AddAsync(Patient patient)
    {
        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string patientId)
    {
        var patient = await GetByIdAsync(patientId);
        if (patient == null)
        {
            throw new PatientException("Patient not found");
        }
        patient.Status=false;
        _context.Patients.Update(patient);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Patient>> GetAllAsync()
    {
        return await _context.Patients.ToListAsync();
    }

    public async Task<Patient?> GetByIdAsync(string patientId)
    {
        // return await _context.Patients.FirstOrDefaultAsync(p=>p.PatientID==patientId);
        return await _context.Patients.FirstOrDefaultAsync(p=>p.PatientID==patientId && p.Status==true);
    }

    public async Task UpdateAsync(Patient patient)
    {
        _context.Patients.Update(patient);
        await _context.SaveChangesAsync();
    }
}
