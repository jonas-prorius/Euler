package euler.assignments;

public class Euler2 {
	private static final int UPPERBOUND = 4000000;
	private static int[] fibonacci = new int[] { 0, 1, 1 };
	
	public static void run() {
		System.out.println("Euler2:");
		int sum = 0;
		while (fibonacci[2] < UPPERBOUND)
			if (isEven(nextFibonacci()))
				sum += fibonacci[2];
		System.out.println(sum);
	}
	
	private static int nextFibonacci() {
		fibonacci[0] = fibonacci[1];
		fibonacci[1] = fibonacci[2];
		fibonacci[2] = fibonacci[0] + fibonacci[1];
		return fibonacci[2];
	}
	
	private static boolean isEven(int i) {
		if (i % 2 == 0)
			return true;
		return false;
	}
}
