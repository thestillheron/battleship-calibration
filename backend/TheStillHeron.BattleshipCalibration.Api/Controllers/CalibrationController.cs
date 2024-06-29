using Microsoft.AspNetCore.Mvc;
using TheStillHeron.BattleshipCalibration.Api.DataTransfer;
using TheStillHeron.BattleshipCalibration.Api.Domain;
using TheStillHeron.BattleshipCalibration.Api.Services;

namespace TheStillHeron.BattleshipCalibration.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CalibrationController(
    ILogger<CalibrationController> logger,
    CalibrationService calibrationService
) : ControllerBase
{
    private readonly ILogger<CalibrationController> _logger = logger;
    private readonly CalibrationService _calibrationService = calibrationService;

    [Route("settings")]
    [HttpPut(Name = "PutCalibrationSettings")]
    public ActionResult<PutCalibrationSettingsResponse> PutCalibrationSettings(
        TurretCalibrationSettings request
    )
    {
        var result = _calibrationService.UpdateTurretCalibrationSettings(request);
        if (result.Success) return result;
        return BadRequest(result);
    }

    [Route("run")]
    [HttpPost(Name = "PerformCalibration")]
    public ActionResult<RunCalibrationResponse> RunCalibration()
    {
        var result = _calibrationService.RunCalibration();
        if (result.Success) return result;
        return BadRequest(result);
    }
}
