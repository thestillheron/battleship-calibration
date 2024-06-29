using TheStillHeron.BattleshipCalibration.Api.Domain;

namespace TheStillHeron.BattleshipCalibration.Api.Test.Domain;

public class TurretCalibrationSettingsTests
{
    [Test]
    public void SumsTheRotationalMovementsOfTheTurret()
    {
        // Arrange
        var settings = new TurretCalibrationSettings
        {
            Caliber = 120,
            Rotations = 2,
            RotationStartPoint = 0,
            RotationEndPoint = 180,
            Location = TurretLocation.Bow
        };

        // Act
        var result = settings.RunCalibration();

        // Assert
        Assert.That(result, Is.EqualTo(720));
    }

    [Test]
    public void CorrectlyAccountsForTravelTimeToAndFromDefaultPosition()
    {
        // Arrange
        var settings = new TurretCalibrationSettings
        {
            Caliber = 120,
            Rotations = 3,
            RotationStartPoint = 40,
            RotationEndPoint = 80,
            Location = TurretLocation.Bow
        };

        // Act
        var result = settings.RunCalibration();

        // Assert
        Assert.That(result, Is.EqualTo(320));
    }
}
