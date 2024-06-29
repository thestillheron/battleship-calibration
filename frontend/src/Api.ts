export enum Location {
  Stern = "Stern",
  Bow = "Bow",
}
export interface Settings {
  caliber: number;
  location: Location;
  rotationStartPoint: number;
  rotationEndPoint: number;
  rotations: number;
}
export const applySettings = async (settings: Settings) => {
  const response = await fetch(
    "http://localhost:5242/api/calibration/settings",
    {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(settings),
    }
  );
  const body = await response.json();
  return { ok: response.ok, ...body };
};

export const runCalibration = async () => {
  const response = await fetch("http://localhost:5242/api/calibration/run", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({}),
  });
  const body = await response.json();
  return { ok: response.ok, ...body };
};
