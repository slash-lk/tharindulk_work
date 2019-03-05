
public class Soy extends CondimentDecorator {
	// an instance variable to hold the beverage we are wrapping.
	Beverage beverage;
	
	// a way to set this instance variable to the object we are wrapping.
	public Soy(Beverage beverage) {
		this.beverage = beverage;
	}

	@Override
	public String getDescription() {
		return beverage.getDescription() + ", Soy";
	}

	@Override
	public double cost() {
		return .15 + beverage.cost();
	}

}
