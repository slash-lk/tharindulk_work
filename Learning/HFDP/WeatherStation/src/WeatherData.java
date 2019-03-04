import java.util.Observable;

// Subject class
public class WeatherData extends Observable {

	private float temperature;
	private float humidity;
	private float pressure;
	
	public float getTemperature() {
		return temperature;
	}

	public float getHumidity() {
		return humidity;
	}

	public float getPressure() {
		return pressure;
	}
	
	public void measurementsChanged() {
		setChanged();
		// object not passed - PULL model
		notifyObservers();
	}
	
	public void setMeasurements(float temperature, float humidity, float pressure) {
		this.temperature = temperature;
		this.humidity = humidity;
		this.pressure = pressure;
		
		measurementsChanged();
	}

}
