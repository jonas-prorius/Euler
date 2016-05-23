package euler.assignments;

import java.util.Random;

import euler.helpers.PrimeFinder;

public class Euler10 {
	private static long sum = 2;
	private static Random r;
	
	public static void run() {
		System.out.println("Euler10:");
		for (int i = 3; i < 2000000; i += 2)
			if (PrimeFinder.isPrime(i))
				sum += i;
		System.out.println(sum);
	}
}