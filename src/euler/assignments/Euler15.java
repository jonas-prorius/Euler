package euler.assignments;

import java.util.HashSet;
import java.util.Random;

public class Euler15 {
	private static Random r = new Random();
	private static HashSet<String> routes = new HashSet<String>();
	private static String s;
	private static int saved = 0, notSaved = 0;
	
	public static void run() {
		System.out.println("Euler15:");
		while (true) {
			s = new String("");
			for (int i = 0; i < 19; i++)
				s += String.valueOf(r.nextInt(2));
			routes.add(new String(s));
			if (routes.size() > saved) {
				saved++;
				notSaved = 0;
			} else {
				notSaved++;
				System.out.println("not saved " + notSaved);
				if (notSaved == 1000000)
					break;
			}
		}
		System.out.println("Final: " + routes.size());
	}
}