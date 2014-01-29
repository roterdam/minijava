class MainClass
{
	public static void main(String[] args)
	{
		System.out.println((new Bug()).foo(1,2,3).m1(true));
	}
}

class Vehicle
{
	boolean flag;
	public int m1(boolean b)
	{
		return 1;
	}
}

class Car extends Vehicle
{
}
class Bug extends Vehicle
{
int[] a;
int b;
int c;
Vehicle v;

public Vehicle foo(int x, int y, int z)
{
	return new Car();
}
}
