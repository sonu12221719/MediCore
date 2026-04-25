using System;
using System.Security.Claims;
using AutoMapper;
using Emr.API.DTOs;
using EmrLibrary.Entities;
using EmrLibrary.Repository;
using EmrLibrary.Exceptions;
using EmrLibrary.Enums;

namespace Emr.API.Services;

public class EMRService:IEMRService
{
    private readonly IEMRRepository _emrRepository;
    private readonly IPrescriptionRepository _prescriptionRepository;
    private readonly ITreatmentLogRepository _treatmentLogRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EMRService(
        IEMRRepository emrRepository,
        IPrescriptionRepository prescriptionRepository,
        ITreatmentLogRepository treatmentLogRepository,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor)
    {
        _emrRepository            = emrRepository;
        _prescriptionRepository   = prescriptionRepository;
        _treatmentLogRepository   = treatmentLogRepository;
        _mapper                   = mapper;
        _httpContextAccessor      = httpContextAccessor;
    }

    private string GetUserIdFromToken()
        => _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    // CREATE EMR — Doctor only
    public async Task<EMRResponseDto> CreateAsync(CreateEMRRequestDto dto)
    {
        var emr          = _mapper.Map<EMR>(dto);
        emr.DoctorID     = GetUserIdFromToken();

        await _emrRepository.AddAsync(emr);
        return _mapper.Map<EMRResponseDto>(emr);
    }

    // GET EMR BY ID
    public async Task<EMRResponseDto> GetByIdAsync(Guid emrId)
    {
        var emr = await _emrRepository.GetByIdAsync(emrId)
            ?? throw new EMRNotFoundException(emrId);

        return _mapper.Map<EMRResponseDto>(emr);
    }

    // GET ALL EMRs BY PATIENT
    public async Task<IEnumerable<EMRResponseDto>> GetByPatientIdAsync(string patientId)
    {
        var emrs = await _emrRepository.GetByPatientIdAsync(patientId);
        return _mapper.Map<IEnumerable<EMRResponseDto>>(emrs);
    }

    // UPDATE EMR — only the doctor who created it
    public async Task<EMRResponseDto> UpdateAsync(Guid emrId, UpdateEMRRequestDto dto)
    {
        var emr = await _emrRepository.GetByIdAsync(emrId)
            ?? throw new EMRNotFoundException(emrId);

        if (emr.DoctorID != GetUserIdFromToken())
            throw new UnauthorizedEMRAccessException();

        emr.Diagnosis     = dto.Diagnosis;
        emr.TreatmentPlan = dto.TreatmentPlan;
        emr.Status        = dto.Status;

        await _emrRepository.UpdateAsync(emr);
        return _mapper.Map<EMRResponseDto>(emr);
    }

    // ADD PRESCRIPTION — only the doctor who created the EMR
    public async Task<PrescriptionResponseDto> AddPrescriptionAsync(Guid emrId, AddPrescriptionRequestDto dto)
    {
        var emr = await _emrRepository.GetByIdAsync(emrId)
            ?? throw new EMRNotFoundException(emrId);

        if (emr.Status == EMRStatus.Closed || emr.Status == EMRStatus.Archived)
            throw new EmrServiceException("Cannot add prescription to a closed or archived EMR.");

        var prescription      = _mapper.Map<Prescription>(dto);
        prescription.EMRID    = emrId;
        prescription.DoctorID = GetUserIdFromToken();

        await _prescriptionRepository.AddAsync(prescription);
        return _mapper.Map<PrescriptionResponseDto>(prescription);
    }

    // GET PRESCRIPTIONS OF EMR
    public async Task<IEnumerable<PrescriptionResponseDto>> GetPrescriptionsAsync(Guid emrId)
    {
        var emr = await _emrRepository.GetByIdAsync(emrId)
            ?? throw new EMRNotFoundException(emrId);

        var prescriptions = await _prescriptionRepository.GetByEMRIdAsync(emrId);
        return _mapper.Map<IEnumerable<PrescriptionResponseDto>>(prescriptions);
    }

    // ADD TREATMENT LOG — Nurse only
    public async Task<TreatmentLogResponseDto> AddTreatmentLogAsync(Guid emrId, AddTreatmentLogRequestDto dto)
    {
        var emr = await _emrRepository.GetByIdAsync(emrId)
            ?? throw new EMRNotFoundException(emrId);

        if (emr.Status == EMRStatus.Archived)
            throw new EmrServiceException("Cannot add treatment log to an archived EMR.");

        var log      = _mapper.Map<TreatmentLog>(dto);
        log.EMRID    = emrId;
        log.NurseID  = GetUserIdFromToken();

        await _treatmentLogRepository.AddAsync(log);
        return _mapper.Map<TreatmentLogResponseDto>(log);
    }

    // GET TREATMENT LOGS OF EMR
    public async Task<IEnumerable<TreatmentLogResponseDto>> GetTreatmentLogsAsync(Guid emrId)
    {
        var emr = await _emrRepository.GetByIdAsync(emrId)
            ?? throw new EMRNotFoundException(emrId);

        var logs = await _treatmentLogRepository.GetByEMRIdAsync(emrId);
        return _mapper.Map<IEnumerable<TreatmentLogResponseDto>>(logs);
    }
}
