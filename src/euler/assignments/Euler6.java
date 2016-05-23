package euler.assignments;

public class Euler6 {
	private static long squareSum = 0, sumSquared = 0;
	
	public static void run() {
		System.out.println("Euler6:");
		for (int i = 0; i <= 100; i++) {
			squareSum += (i * i);
			sumSquared += i;
		}
		sumSquared *= sumSquared;
		System.out.println(sumSquared - squareSum);
	}
}