public class DeathLocation {

	public float x;
	public float y;
	public string name;
	public int icon;
	public string message;
	public int team;

	public DeathLocation(float posx, float posy, string name, int icon, int team, string message = "")
	{
		x = posx;
		y = posy;
		this.name = name;
		this.icon = icon;
		this.message = message;
		this.team = team;
	}
}
