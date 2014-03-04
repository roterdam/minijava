class MainClass
{
    public static void main(String[] args)
	{
		System.out.println(new SubClass().ComputeFac());
    }
}

class SubClass
{
	public int ComputeFac()
	{
		int a;
		int b; 
		int c;
		a = 5;
		b = 2;
		c = a * b;
		return c;
	}
}