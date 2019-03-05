
public class Mocha extends CondimentDecorator {
	// an instance variable to hold the beverage we are wrapping.
	Beverage beverage;
	
	// a way to set this instance variable to the object we are wrapping.
	public Mocha(Beverage beverage) {
		this.beverage = beverage;
	}

	@Override
	public String getDescription() {
		return beverage.getDescription() + ", Mocha";
	}

	@Override
	public double cost() {
		return .20 + beverage.cost();
	}

}
