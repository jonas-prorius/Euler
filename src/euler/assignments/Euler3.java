package euler.assignments;

import euler.helpers.PrimeFinder;

public class Euler3 {
	private static final long TEST = 600851475143L;
	private static final double eps = 0.00001;
	private static long newnumm = TEST;
	private static long largestFact = 0;
	private static int counter = 2;
	
	public static void run() {
		System.out.println("Euler3:");
		getFactor2();
		System.out.println(largestFact);
		System.out.println(getFactor(TEST));
	}
	
	private static long getFactor(long l) {
		long divisor = (Math.round(Math.sqrt(l) + 1));
		while (divisor > 0) {
			if (l % divisor == 0 && PrimeFinder.isPrime(divisor))
				return divisor;
			return divisor;
		}
		return divisor;
	}
	
	public static void getFactor2() {
		while (counter * counter <= newnumm) {
			if (newnumm % counter == 0) {
				newnumm = newnumm / counter;
				largestFact = counter;
			} else {
				counter++;
			}
		}
		if (newnumm > largestFact) { // the remainder is a prime number
			largestFact = newnumm;
		}
	}
}