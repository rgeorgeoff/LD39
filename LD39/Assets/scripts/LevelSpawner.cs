using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LevelSpawner : MonoBehaviour{

	public GameObject[] ourBlocks;
	public GameObject fire;
	public GameObject pixel;
	float width = 4;
	float depth = 4;
	float pieceWidth = 3.5f;
	float pieceHeightGap = 5;
	float widthQuadrent;
	float heightQuadrent;
	float chanceOfPixelPerQuadrent = .4f;

	public GameObject downloadingDisplay;
	private WWW www;

	public bool readyToPlay = false;

	public List<DeathLocation> deathLocations = new List<DeathLocation> ();
	private GameObject levelObjectParent;

	private GameObject fireObjectParent;

	public List<Vector2> spawnedLocations = new List<Vector2>();
	private GameObject player;

	private PlayerControl pc;

	// Use this for initialization of our level
	void Start () {
		levelObjectParent = new GameObject();
		widthQuadrent = pieceWidth * width;
		heightQuadrent = pieceHeightGap * depth;
		DownloadAndSpawnFires();
		SpawnPlatforms (new Vector3 (0, 0, 0));
		/*
		Make the initial cell the current cell and mark it as visited
		While there are unvisited cells
		If the current cell has any neighbours which have not been visited
		Choose randomly one of the unvisited neighbours
		Push the current cell to the stack
		Remove the wall between the current cell and the chosen cell
		Make the chosen cell the current cell and mark it as visited
		Else if stack is not empty
			Pop a cell from the stack
			Make it the current cell
		*/

	}

	void SpawnPlatforms (Vector3 pos)
	{
		int a = (int)pos.x;
		int b = (int)pos.y;
		Random.InitState( a*45 + b * 23 + (a-b*3)*30 );
		if(levelObjectParent == null)
			levelObjectParent = new GameObject();
		for (int y = 0; y < depth; y++) {
			int removeMe = Random.Range (0, (int)width);
			for (int x = 0; x < width; x++) {
				if (x != removeMe) {
					GameObject o = Instantiate (ourBlocks [0], new Vector3 ((x - width / 2) * pieceWidth + pos.x * widthQuadrent, -y * pieceHeightGap + pos.y * heightQuadrent, 0), Quaternion.identity) as GameObject;
					o.transform.parent = levelObjectParent.transform;
				}
			}
		}
	}

	void SpawnPixel (Vector3 pos)
	{
		if (Random.value < chanceOfPixelPerQuadrent) {
			float x = Random.Range (0, width);
			float y = Random.Range (0, depth);
			GameObject o = Instantiate (pixel, new Vector3 ((x - width / 2) * pieceWidth + pos.x * widthQuadrent, -y * pieceHeightGap + pos.y * heightQuadrent + 1.2f, 0), Quaternion.identity);
			o.GetComponent<PixelPickup> ().posx = (int)pos.x;
			o.GetComponent<PixelPickup> ().posy = (int)pos.y;
			NetworkServer.Spawn (o);

		}
	}

	float TimeToRefreshFire;
	float refreshTime = 10;
	public void DownloadAndSpawnFires()
	{
		TimeToRefreshFire = Time.time + refreshTime;
		www = new WWW(StaticVarScript.url + "getData.php");
		DisplayDownloadMessage(true);
		StartCoroutine("DownLoader");
	}

	IEnumerator DownLoader()
	{
		yield return www;
		DisplayDownloadMessage(false);
		ParseData (www.text);
		SpawnDeathLocations ();
		readyToPlay = true;
	}


	
	// Update is called once per frame
	void Update () {
		
		if (!player) {
			player = GameObject.FindGameObjectWithTag ("Player");
			if(player)
				pc = player.GetComponent<PlayerControl> ();
		}
		else {
			float posx = player.transform.position.x;
			float posy = player.transform.position.y;
			for (int i = 0; i < 3; i++) {
				for (int j = 0; j < 3; j++) {
					Vector2 v = new Vector2 (Mathf.Floor (posx / widthQuadrent) - j + 1, Mathf.Floor (posy / heightQuadrent) - i + 1);
					if (!spawnedLocations.Contains (v)) {
						spawnedLocations.Add (v);
						SpawnPlatforms (v);
						if(pc.isServer)
							SpawnPixel (v);
					}
				}
			}
		}
		if (TimeToRefreshFire < Time.time) {
			DownloadAndSpawnFires ();
		}

	}

	void DisplayDownloadMessage (bool onOff)
	{
		//downloadingDisplay.SetActive (onOff);
	}

	void SpawnDeathLocations ()
	{
		if (fireObjectParent) {
			GameObject temp = fireObjectParent;
			Destroy (temp);
		}
		fireObjectParent = new GameObject ();
		foreach (DeathLocation d in deathLocations)
		{
			GameObject f = Instantiate (fire, new Vector3 (d.x, d.y), Quaternion.identity);
			f.transform.SetParent (fireObjectParent.transform);
			f.GetComponent<Fire> ().InitMe (d);
		}
	}

	void ParseData(string wwwtext)
	{
		deathLocations = new List<DeathLocation> ();
		string[] entries = wwwtext.Split('\n');
		foreach (string s in entries) {
			string[] entryvals = s.Split (',');
			if (entryvals.Length > 1) {
				if (entryvals [3] == "") //icon
					entryvals [3] = "0";
				int team = System.Int32.Parse (entryvals [5]);
				deathLocations.Add(new DeathLocation(float.Parse(entryvals[0]), float.Parse(entryvals[1]), entryvals[2], System.Int32.Parse(entryvals[3]), team, entryvals[4]));
			}
		}
	}
}

