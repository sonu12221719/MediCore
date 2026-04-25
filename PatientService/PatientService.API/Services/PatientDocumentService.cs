using System;
using AutoMapper;
using PatientLibrary.Entities;
using PatientLibrary.Exceptions;
using PatientLibrary.Repository;
using PatientService.API.DTOs;

namespace PatientService.API.Services;

public class PatientDocumentService : IPatientDocumentService
{
    private readonly IPatientDocumentRepository _patientDocumentRepository;
    private readonly IMapper _mapper;
    public PatientDocumentService(IPatientDocumentRepository patientDocumentRepository, IMapper mapper)
    {
        _patientDocumentRepository=patientDocumentRepository;
        _mapper=mapper;
    }
    public async Task DeleteDocumentAsync(Guid documentId)
    {
        try
        {
            await _patientDocumentRepository.DeleteDocumentAsync(documentId);
        }
        catch(PatientException ex)
        {
            throw new PatientException(ex.Message);
        }
        catch (System.Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<IEnumerable<ResponsePatientDocumentDto>> GetAllDocumentAsync()
    {
        try
        {
            IEnumerable<PatientDocument> patientDocuments = await _patientDocumentRepository.GetAllDocumentAsync();
            IEnumerable<ResponsePatientDocumentDto> documents = patientDocuments.Select(patientDocument=>_mapper.Map<PatientDocument, ResponsePatientDocumentDto>(patientDocument));
            return documents;
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public async Task<ResponsePatientDocumentDto?> GetDocumentByIdAsync(Guid documentId)
    {
        try
        {
            PatientDocument? patientDocument = await _patientDocumentRepository.GetDocumentByIdAsync(documentId);
            if (patientDocument == null)
            {
                throw new PatientException("Document not found.");
            }
            ResponsePatientDocumentDto result = _mapper.Map<PatientDocument,ResponsePatientDocumentDto>(patientDocument);
            return result;
        }
        catch (PatientException)
        {
            throw;
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public async Task UploadDocumentAsync(string patientId, RequestPatientDocumentDto dto)
    {
        try
        {
            PatientDocument patientDocument = _mapper.Map<RequestPatientDocumentDto, PatientDocument>(dto);
            patientDocument.PatientID=patientId;
            await _patientDocumentRepository.UploadDocumentAsync(patientDocument);
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}
