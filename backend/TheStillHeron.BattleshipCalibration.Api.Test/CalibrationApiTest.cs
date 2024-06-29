using System.Net;
using TheStillHeron.BattleshipCalibration.Api.DataTransfer;
using TheStillHeron.BattleshipCalibration.Api.Domain;
using TheStillHeron.BattleshipCalibration.Api.Test.Helpers;

namespace TheStillHeron.BattleshipCalibration.Api.Test;

public class ApiTests : SubcutaneousTest
{
    [Test]
    public async Task AnInvalidCalibrationSettingsRequestReturnsBadRequest()
    {
        var request = new TurretCalibrationSettings
        {
            Caliber = 12,
            Rotations = 0,
            RotationStartPoint = -15,
            RotationEndPoint = 200,
            Location = TurretLocation.Bow,
        };

        // Act
        var (_, statusCode, rawResponse) = await PutRequest<TurretCalibrationSettings, object>(
            "api/calibration/settings",
            request
        );

        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(statusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(
                rawResponse,
                Does.Contain("The field Caliber must be between 102 and 450.")
            );
            Assert.That(rawResponse, Does.Contain("The field Rotations must be between 1 and ∞."));
            Assert.That(
                rawResponse,
                Does.Contain("The field RotationStartPoint must be between 0 and 180.")
            );
            Assert.That(
                rawResponse,
                Does.Contain("The field RotationEndPoint must be between 0 and 180.")
            );
        });
    }

    [Test]
    public async Task AttemptingToRunCalibrationWithoutSettingsReturnsBadRequest()
    {
        // Act
        var (result, statusCode, _) = await PostRequest<object, RunCalibrationResponse>(
            "api/calibration/run",
            new { }
        );

        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(statusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(
                result!.Errors,
                Does.Contain("Cannot perform a calibration until settings have been provided.")
            );
        });
    }

    [Test]
    public async Task AttemptingToSetAnEndpointGreaterThanTheStartPointReturnsBadRequest()
    {
        var request = new TurretCalibrationSettings
        {
            Caliber = 110,
            Rotations = 1,
            RotationStartPoint = 50,
            RotationEndPoint = 40,
            Location = TurretLocation.Bow,
        };

        // Act
        var (_, statusCode, rawResponse) = await PutRequest<TurretCalibrationSettings, object>(
            "api/calibration/settings",
            request
        );

        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(statusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(
                rawResponse,
                Does.Contain("The field RotationEndPoint must be greater than the RotationStartPoint.")
            );
        });
    }
}
