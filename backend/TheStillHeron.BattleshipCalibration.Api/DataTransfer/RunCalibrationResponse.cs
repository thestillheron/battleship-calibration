namespace TheStillHeron.BattleshipCalibration.Api.DataTransfer;

public class RunCalibrationResponse
{
    public int? TotalRotation { get; set; }
    public bool Success { get; set; }
    public IEnumerable<string> Errors { get; set; } = [];
}
