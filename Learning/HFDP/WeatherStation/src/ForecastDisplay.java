import java.util.Observable;
import java.util.Observer;

public class ForecastDisplay implements DisplayElement, Observer {
	private Observable observable;
	private float currentPressure = 29.92f;
	private float lastPressure;
	
	public ForecastDisplay(Observable observable) {
		this.observable = observable;
		observable.addObserver(this);
	}

	@Override
	public void update(Observable observable, Object arg) {
		if (observable instanceof WeatherData) {
			WeatherData weatherData = (WeatherData)observable;
			
			lastPressure = currentPressure;
			currentPressure = weatherData.getPressure(); 
			display();
		}		
	}

	@Override
	public void display() {
		System.out.println("Last Pressure: " + lastPressure + ". Current Pressure: " + currentPressure);
	}

}
