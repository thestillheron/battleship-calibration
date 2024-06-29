namespace TheStillHeron.BattleshipCalibration.Api.DataTransfer;

public class PutCalibrationSettingsResponse
{
    public bool Success { get; set; }
    public IEnumerable<string> Errors { get; set; } = [];
}
