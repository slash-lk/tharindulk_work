
public class MallardDuck extends Duck {

	
	public MallardDuck() {
		// MallardDuck inherits the quackBehavior and flyBehavior instance variables from class Duck.
		
		// A MallardDuck uses the Quack class to handle its quack, so when performQuack is called,
		// the responsibility for the quack is delegated to the Quack object and we get a real quack.
		quackBehavior = new Quack();
		
		flyBehavior = new FlyWithWings();
	}

	@Override
	void display() {
		System.out.println("I'm a Mallard Duck!");

	}

}
