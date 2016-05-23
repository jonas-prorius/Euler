package euler.assignments;

public class Euler1 {
	private static final int[] validMultiples = new int[] { 3, 5 };
	private static final int LOWERBOUD = 0;
	private static final int UPPERBOUND = 999;
	
	public static void run() {
		System.out.println("Euler1:");
		int sum = 0;
		for (int i = LOWERBOUD; i <= UPPERBOUND; i++)
			if (validMultiple(i))
				sum += i;
		System.out.println(sum);
	}
	
	private static boolean validMultiple(int i) {
		for (int factor : validMultiples)
			if (i % factor == 0)
				return true;
		return false;
	}
}
