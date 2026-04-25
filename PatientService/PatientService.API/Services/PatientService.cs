using System;
using AutoMapper;
using PatientLibrary.Entities;
using PatientLibrary.Exceptions;
using PatientLibrary.Repository;
using PatientService.API.DTOs;

namespace PatientService.API.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;
    private readonly IMapper _mapper;
    public PatientService(IPatientRepository patientRepository, IMapper mapper)
    {
        _patientRepository=patientRepository;
        _mapper=mapper;
    }
    public async Task AddPatientAsync(RequestPatientDto dto)
    {
        try
        {
            var patientExist = await _patientRepository.GetByIdAsync(dto.PatientID);
            if (patientExist != null)
            {
                throw new PatientException("Patient already exist.");
            }
            var patient = _mapper.Map<Patient>(dto);
            await _patientRepository.AddAsync(patient);
        }
        catch (PatientException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task DeletePatientAsync(string patientId)
    {
        try
        {
            await _patientRepository.DeleteAsync(patientId);
        }
        catch(PatientException ex)
        {
            throw new PatientException(ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<IEnumerable<ResponsePatientDto>> GetAllPatientAsync()
    {
        IEnumerable<Patient> patients = await _patientRepository.GetAllAsync();
        IEnumerable<ResponsePatientDto> result = _mapper.Map<IEnumerable<ResponsePatientDto>>(patients);
        return result;
    }

    public async Task<ResponsePatientDto?> GetByIdAsync(string patientId)
    {
        var patient = await _patientRepository.GetByIdAsync(patientId);
        if (patient == null)
        {
            throw new PatientException("Patient not found");
        }
        var result = _mapper.Map<ResponsePatientDto>(patient);
        return result;
    }

    public async Task UpdateAsync(UpdatePatientDto dto)
    {
        try
        {
            var existingPatient = await _patientRepository.GetByIdAsync(dto.PatientID);
            if (existingPatient == null)
            {
                throw new PatientException("Patient not found");
            }
            var patient = _mapper.Map<UpdatePatientDto, Patient>(dto, existingPatient);
            await _patientRepository.UpdateAsync(patient);
        }
        catch (System.Exception)
        {
            throw;
        }
        
    }
}
