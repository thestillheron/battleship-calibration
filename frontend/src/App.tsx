import { useEffect, useState } from "react";
import "./App.css";

function App() {
  const [forecastValues, setForecastValues] = useState<any[]>([]);

  useEffect(() => {
    async function getForecast() {
      let result: Response;
      try {
        result = await fetch("http://localhost:5242/api/calibration");
      } catch (error) {
        console.log(error);
        throw error;
      }
      setForecastValues(await result!.json());
    }
    getForecast();
  }, []);

  return (
    <div className="App">
      <header className="App-header">{forecastValues.length} retrieved</header>
    </div>
  );
}

export default App;
