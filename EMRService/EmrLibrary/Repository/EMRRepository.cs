using System;
using EmrLibrary.Data;
using EmrLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmrLibrary.Repository;

public class EMRRepository:IEMRRepository
{
    private readonly EmrDBContext _context;

    public EMRRepository(EmrDBContext context)
    {
        _context = context;
    }

    public async Task<EMR?> GetByIdAsync(Guid emrId)
    {
        return await _context.EMRs
            .Include(e => e.Prescriptions)
            .Include(e => e.TreatmentLogs)
            .FirstOrDefaultAsync(e => e.EMRID == emrId);
    }

    public async Task<IEnumerable<EMR>> GetByPatientIdAsync(string patientId)
    {
        return await _context.EMRs
            .Include(e => e.Prescriptions)
            .Include(e => e.TreatmentLogs)
            .Where(e => e.PatientID == patientId)
            .OrderByDescending(e => e.Date)
            .ToListAsync();
    }

    public async Task<IEnumerable<EMR>> GetByDoctorIdAsync(string doctorId)
    {
        return await _context.EMRs
            .Include(e => e.Prescriptions)
            .Include(e => e.TreatmentLogs)
            .Where(e => e.DoctorID == doctorId)
            .OrderByDescending(e => e.Date)
            .ToListAsync();
    }

    public async Task AddAsync(EMR emr)
    {
        await _context.EMRs.AddAsync(emr);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(EMR emr)
    {
        emr.UpdatedAt = DateTime.UtcNow;
        _context.EMRs.Update(emr);
        await _context.SaveChangesAsync();
    }
}
