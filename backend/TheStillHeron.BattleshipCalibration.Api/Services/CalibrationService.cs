using TheStillHeron.BattleshipCalibration.Api.DataTransfer;
using TheStillHeron.BattleshipCalibration.Api.Domain;

namespace TheStillHeron.BattleshipCalibration.Api.Services;

public class CalibrationService(ILogger<CalibrationService> logger)
{
    private readonly ILogger<CalibrationService> _logger = logger;

    private TurretCalibrationSettings? _calibrationSettings;

    /// <summary>
    /// Calibrates the turret.
    /// </summary>
    /// <param name="request"></param>
    /// <returns>The result of calibration, including success / failure and a message indicating the result.</returns>
    public PutCalibrationSettingsResponse UpdateTurretCalibrationSettings(TurretCalibrationSettings request)
    {
        if (!request.Valid)
        {
            return new PutCalibrationSettingsResponse
            {
                Success = false,
                Errors = request.Errors
            };
        }

        _calibrationSettings = request;
        _logger.LogInformation("Calibration settings have been updated. New settings: {_calibrationSettings}", _calibrationSettings);

        return new PutCalibrationSettingsResponse
        {
            Success = true
        };
    }

    public RunCalibrationResponse RunCalibration()
    {
        if (_calibrationSettings == null)
        {
            _logger.LogError("User attempted to perform a calibration without providing settings.");
            return new RunCalibrationResponse
            {
                Success = false,
                Errors = ["Cannot perform a calibration until settings have been provided."]
            };
        }

        var totalRotation = _calibrationSettings.RunCalibration();
        _logger.LogError("Calibration run performed successfuly. Result: {totalRotation}", totalRotation);

        return new RunCalibrationResponse
        {
            Success = true,
            TotalRotation = _calibrationSettings.RunCalibration()
        };
    }
}
