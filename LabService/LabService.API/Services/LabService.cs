using System;
using System.Security.Claims;
using AutoMapper;
using LabService.API.DTOs;
using LabServiceLibrary.Entities;
using LabServiceLibrary.Enums;
using LabServiceLibrary.Exceptions;
using LabServiceLibrary.Repository;

namespace LabService.API.Services;

public class LabService:ILabService
{
    private readonly ILabTestRepository _labTestRepository;
    private readonly ILabReportRepository _labReportRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LabService(
        ILabTestRepository labTestRepository,
        ILabReportRepository labReportRepository,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor)
    {
        _labTestRepository    = labTestRepository;
        _labReportRepository  = labReportRepository;
        _mapper               = mapper;
        _httpContextAccessor  = httpContextAccessor;
    }

    private string GetUserIdFromToken()
        => _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    // CREATE TEST — Doctor only
    public async Task<LabTestResponseDto> CreateTestAsync(CreateLabTestRequestDto dto)
    {
        var labTest          = _mapper.Map<LabTest>(dto);
        labTest.DoctorID     = GetUserIdFromToken();

        await _labTestRepository.AddAsync(labTest);
        return _mapper.Map<LabTestResponseDto>(labTest);
    }

    // GET ALL TESTS — Admin / Doctor
    public async Task<IEnumerable<LabTestResponseDto>> GetAllTestsAsync()
    {
        var tests = await _labTestRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<LabTestResponseDto>>(tests);
    }

    // GET TEST BY ID
    public async Task<LabTestResponseDto> GetTestByIdAsync(Guid testId)
    {
        var test = await _labTestRepository.GetByIdAsync(testId)
            ?? throw new LabTestNotFoundException(testId);

        return _mapper.Map<LabTestResponseDto>(test);
    }

    // GET TESTS BY PATIENT
    public async Task<IEnumerable<LabTestResponseDto>> GetTestsByPatientAsync(string patientId)
    {
        var tests = await _labTestRepository.GetByPatientIdAsync(patientId);
        return _mapper.Map<IEnumerable<LabTestResponseDto>>(tests);
    }

    // ASSIGN TECHNICIAN — Admin only
    public async Task<LabTestResponseDto> AssignTechnicianAsync(Guid testId, AssignTechnicianRequestDto dto)
    {
        var test = await _labTestRepository.GetByIdAsync(testId)
            ?? throw new LabTestNotFoundException(testId);

        if (test.Status == LabTestStatus.Cancelled)
            throw new LabServiceException("Cannot assign technician to a cancelled test.");

        test.TechnicianID = dto.TechnicianID;
        test.Status       = LabTestStatus.InProgress;

        await _labTestRepository.UpdateAsync(test);
        return _mapper.Map<LabTestResponseDto>(test);
    }

    // UPDATE TEST STATUS — Technician only
    public async Task<LabTestResponseDto> UpdateTestStatusAsync(Guid testId, UpdateLabTestStatusRequestDto dto)
    {
        var test = await _labTestRepository.GetByIdAsync(testId)
            ?? throw new LabTestNotFoundException(testId);

        if (test.TechnicianID != GetUserIdFromToken())
            throw new UnauthorizedLabAccessException();

        if (test.Status == LabTestStatus.Cancelled)
            throw new LabServiceException("Cannot update status of a cancelled test.");

        test.Status = dto.Status;
        await _labTestRepository.UpdateAsync(test);
        return _mapper.Map<LabTestResponseDto>(test);
    }

    // UPLOAD REPORT — Technician only
    public async Task<LabReportResponseDto> UploadReportAsync(Guid testId, UploadLabReportRequestDto dto)
    {
        var test = await _labTestRepository.GetByIdAsync(testId)
            ?? throw new LabTestNotFoundException(testId);

        if (test.TechnicianID != GetUserIdFromToken())
            throw new UnauthorizedLabAccessException();

        if (test.Status != LabTestStatus.Completed)
            throw new LabServiceException("Report can only be uploaded for a completed test.");

        // Check report does not already exist
        var existing = await _labReportRepository.GetByTestIdAsync(testId);
        if (existing is not null)
            throw new LabReportAlreadyExistsException(testId);

        var report = new LabReport
        {
            ReportID  = Guid.NewGuid(),
            TestID    = testId,
            FileName  = dto.File.FileName,
            FileURI   = $"/uploads/lab/{testId}/{dto.File.FileName}",
            Notes     = dto.Notes,
            Status    = LabReportStatus.Uploaded,
            Date      = DateTime.UtcNow
        };

        await _labReportRepository.AddAsync(report);
        return _mapper.Map<LabReportResponseDto>(report);
    }

    // GET REPORT
    public async Task<LabReportResponseDto> GetReportAsync(Guid testId)
    {
        var test = await _labTestRepository.GetByIdAsync(testId)
            ?? throw new LabTestNotFoundException(testId);

        var report = await _labReportRepository.GetByTestIdAsync(testId)
            ?? throw new LabReportNotFoundException(testId);

        return _mapper.Map<LabReportResponseDto>(report);
    }
}
