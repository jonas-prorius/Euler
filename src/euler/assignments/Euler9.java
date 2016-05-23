package euler.assignments;

import java.util.Random;

public class Euler9 {
	private static int a, b, c;
	private static Random r;
	
	public static void run() {
		System.out.println("Euler9:");
		r = new Random();
		while (true) {
			a = r.nextInt(333);
			b = r.nextInt(1000 - a);
			c = 1000 - a - b;
			if (a * a + b * b == c * c && a < b && b < c)
				break;
		}
		System.out.println(a + " " + b + " " + c);
		System.out.println(a * b * c);
	}
}