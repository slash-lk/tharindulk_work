
public class MiniDuckSimulator {
	public static void main(String[] args) {
		Duck mallard = new MallardDuck();
		
		// This calls the MullardDuck's inherited performQuack() method, which then delegates to the object's
		// QuackBehavior (i.e. calls quack() on the duck's inherited quackBehavior reference) 
		mallard.performQuack();
		mallard.performFly();
		
		// Dynamically changing flying behavior
		Duck model = new ModelDuck();
		model.performFly();
		
		// invoke the model's inherited behavior setter method
		model.setFlyBehavior(new FlyRocketPowered());
		model.performFly();
	}
}
