
public abstract class Duck {
	
	// interface type variables
	FlyBehavior flyBehavior;
	QuackBehavior quackBehavior;

	void swim() {
		System.out.println("All ducks float, even decoys!");
	}
	
	void performQuack() {
		// delegate to the behavior class
		quackBehavior.quack();
	}
	
	void performFly() {
		// delegate to the behavior class		
		flyBehavior.fly();
	}	
	
	// abstract since all duck subtypes look different
	abstract void display();
	
	public void setFlyBehavior(FlyBehavior fb) {
		flyBehavior = fb;
	}
	
	public void setQuackBehavior(QuackBehavior qb) {
		quackBehavior = qb;
	}
}
