using System;
using Microsoft.EntityFrameworkCore;
using PatientLibrary.Data;
using PatientLibrary.Entities;
using PatientLibrary.Exceptions;

namespace PatientLibrary.Repository;

public class PatientDocumentRepository : IPatientDocumentRepository
{
    private readonly PatientDbContext _context;
    public PatientDocumentRepository(PatientDbContext context)
    {
        _context=context;
    }
    public async Task DeleteDocumentAsync(Guid documentId)
    {
        var document = await GetDocumentByIdAsync(documentId);
        if (document == null)
        {
            throw new PatientException("Document not found.");
        }
        _context.PatientDocuments.Remove(document);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<PatientDocument>> GetAllDocumentAsync()
    {
        return await _context.PatientDocuments.ToListAsync();
    }

    public async Task<PatientDocument?> GetDocumentByIdAsync(Guid documentId)
    {
        return await _context.PatientDocuments.FirstOrDefaultAsync(p=>p.DocumentID==documentId);
    }

    public async Task UploadDocumentAsync(PatientDocument document)
    {
        _context.PatientDocuments.Update(document);
        await _context.SaveChangesAsync();
    }
}
