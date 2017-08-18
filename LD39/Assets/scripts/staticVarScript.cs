public static class StaticVarScript {

	public static string url = "http://localhost/LD39/";

	public static string name = "player";

	public static int icon = 0;

	public static int team = 0; //blue = 0, red = 1

	public static void changeName(string newName)
	{
		name = newName;
	}

	public static void changeIcon(int newIcon)
	{
		icon = newIcon;
	}

	public static void changeTeam(int newTeam)
	{
		team = newTeam;
	}
}
