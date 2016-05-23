package euler.assignments;

public class Euler4 {
	private static final int UPPERBOUND = 4000000;
	private static int maxPalindrome = 0;
	
	public static void run() {
		System.out.println("Euler4:");
		for (int factor1 = 999; factor1 >= 100; factor1--)
			for (int factor2 = 999; factor2 >= 100; factor2--)
				if (isPalindrome(factor1 * factor2))
					maxPalindrome = Math.max(factor1 * factor2, maxPalindrome);
		System.out.println(maxPalindrome);
	}
	
	private static boolean isPalindrome(int i) {
		String s = String.valueOf(i);
		int len = s.length();
		int index = 0;
		while (index < len / 2 + 1) {
			if (s.charAt(index) != s.charAt(len - index - 1))
				return false;
			index++;
		}
		return true;
	}
}
