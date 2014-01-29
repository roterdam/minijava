class MainClass
{
    public static void main(String[] a)
	{
		System.out.println(new test().t1(26));
    }
}

class Vehicle
{
	int DoorCount;
}

class Car extends Vehicle
{
	public int SetDoorCount(int count)
	{
		DoorCount = count;
		return DoorCount;
	}
}

class test
{
	Car c;

	public int t1(int count)
	{
		int t;
		c = new Car();
		//t = c.SetDoorCount(count);
		c.DoorCount = count;
		return c.DoorCount;
	}
}

