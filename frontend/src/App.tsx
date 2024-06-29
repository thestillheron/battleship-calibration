import { useCallback, useState } from "react";
import "./App.css";
import { applySettings, runCalibration, Location } from "./Api";
import { Errors } from "./Errors";

function App() {
  const [caliber, setCaliber] = useState(102);
  const [location, setLocation] = useState<Location>(Location.Stern);
  const [rotationStartPoint, setRotationStartPoint] = useState(0);
  const [rotationEndPoint, setRotationEndPoint] = useState(0);
  const [rotations, setRotations] = useState(1);
  const [canRun, setCanRun] = useState(false);
  const [totalRotation, setTotalRotation] = useState();
  const [errors, setErrors] = useState([]);

  const handleSubmitSettings = useCallback(async () => {
    const response = await applySettings({
      caliber,
      location,
      rotationStartPoint,
      rotationEndPoint,
      rotations,
    });
    if (response.ok) {
      setCanRun(true);
      setErrors([]);
    } else {
      setCanRun(false);
      if (response.errors && response.errors.length > 0) {
        setErrors(response.errors);
      }
    }
  }, [
    caliber,
    location,
    rotationStartPoint,
    rotationEndPoint,
    rotations,
    setCanRun,
  ]);

  const handleRunCalibration = useCallback(async () => {
    const response = await runCalibration();
    if (response.ok) {
      setTotalRotation(response.totalRotation);
      setErrors([]);
    } else {
      if (response.errors && response.errors.length > 0) {
        setErrors(response.errors);
      }
    }
  }, [setTotalRotation]);

  return (
    <div className="layout">
      <h1 style={{ textAlign: "center" }}>Turret Configuration</h1>
      <div className="top">
        <div className="topLeft">
          <h1>Settings</h1>
          <h3>Caliber</h3>
          <p>
            <input
              type="range"
              min={102}
              max={450}
              value={caliber}
              onChange={(e) => setCaliber(+e.target.value)}
            />
          </p>
          <p>{caliber}mm</p>

          <h3>Location</h3>
          <p>
            {Location.Stern}
            <input
              type="radio"
              name="location"
              value="Stern"
              checked={location === Location.Stern}
              onChange={() => setLocation(Location.Stern)}
            />
            {Location.Bow}
            <input
              style={{ marginRight: "16px" }}
              type="radio"
              name="location"
              value="Bow"
              checked={location === Location.Bow}
              onChange={() => setLocation(Location.Bow)}
            />
          </p>

          <h3>Rotation Start Point</h3>
          <p>
            <input
              type="range"
              min={0}
              max={180}
              value={rotationStartPoint}
              onChange={(e) => setRotationStartPoint(+e.target.value)}
            />
          </p>
          <p>{rotationStartPoint} degrees</p>

          <h3>Rotation End Point</h3>
          <p>
            <input
              type="range"
              min={0}
              max={180}
              value={rotationEndPoint}
              onChange={(e) => setRotationEndPoint(+e.target.value)}
            />
          </p>
          <p>{rotationEndPoint} degrees</p>

          <h3>Rotations</h3>
          <input
            style={{ maxWidth: "60px", marginBottom: "16px" }}
            type="number"
            min={1}
            value={rotations}
            onChange={(e) => setRotations(+e.target.value)}
          />

          <p>
            <button onClick={handleSubmitSettings}>Submit Settings</button>
          </p>
        </div>

        {(canRun || totalRotation !== undefined) && (
          <div className="topRight">
            <h1>Calibration</h1>
            <p>
              <button onClick={handleRunCalibration}>Run Calibration</button>
            </p>

            {totalRotation && (
              <p>Total Rotations during calibration: {totalRotation} degress</p>
            )}
          </div>
        )}
      </div>
      <div className="bottom">
        <Errors errors={errors} />
      </div>
    </div>
  );
}

export default App;
