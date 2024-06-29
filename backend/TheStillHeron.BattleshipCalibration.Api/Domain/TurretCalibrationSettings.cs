using System.ComponentModel.DataAnnotations;

namespace TheStillHeron.BattleshipCalibration.Api.Domain;

public class TurretCalibrationSettings
{
    public bool Valid {
        get;
        private set;
    }

    public IList<string> Errors { get; set; } = [];

    [Required]
    [Range(102, 450)]
    public required int Caliber { get; set; }

    [Required]
    public required TurretLocation Location { get; set; }

    [Required]
    [Range(0, 180)]
    public required int RotationStartPoint { get; set; }

    private int _rotationEndPoint;
    [Required]
    [Range(0, 180)]
    public required int RotationEndPoint
    {
        get => _rotationEndPoint;
        set
        {
            if (value <= RotationStartPoint)
            {
                Valid = false;
                Errors.Add("The field RotationEndPoint must be greater than the RotationStartPoint.");
            }
            _rotationEndPoint = value;
        }
    }

    [Range(1, double.PositiveInfinity)]
    public required int Rotations { get; set; }

    public int RunCalibration()
    {
        // Move from 0 degree default position to start point
        var total = RotationStartPoint;

        // Move from start point to end point once per rotation
        total += (RotationEndPoint - RotationStartPoint) * Rotations;

        // Move from end point to start point once per rotation -1
        total += (RotationEndPoint - RotationStartPoint) * (Rotations - 1);

        // Move from the end point to the 0 degree default position
        total += RotationEndPoint;

        return total;
    }
}
