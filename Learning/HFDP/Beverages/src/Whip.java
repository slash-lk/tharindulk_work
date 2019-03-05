
public class Whip extends CondimentDecorator {
	// an instance variable to hold the beverage we are wrapping.
	Beverage beverage;
	
	// a way to set this instance variable to the object we are wrapping.
	public Whip(Beverage beverage) {
		this.beverage = beverage;
	}

	@Override
	public String getDescription() {
		return beverage.getDescription() + ", Whip";
	}

	@Override
	public double cost() {
		return .10 + beverage.cost();
	}

}
