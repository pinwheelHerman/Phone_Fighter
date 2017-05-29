using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour {

	public Transform tilePrefab;
	public Vector2 mapSize;
	public ArrayList map = new ArrayList();
	public Color color = Color.black ;
    private int tileAdjust;
    private Vector3 startPosition;
    private GameObject mHolder;

	[Range(0,10)]
	public float outlinePercent;

	void Start()
	{
		GenerateMap ();
        tileAdjust = Screen.height/3;
    }

	public void GenerateMap() 
	{	
		string holderName = "Generated Map";

		if (transform.Find (holderName))
        {
			Destroy(transform.Find(holderName).gameObject);
		}

		Transform mapHolder = new GameObject (holderName).transform;
		mapHolder.parent = transform;

		for (int y = 0; y < mapSize.y; y ++) 
		{
			for (int x = 0; x < mapSize.x; x ++)
			{
			    Vector3 tilePosition = new Vector3(mapSize.x/2 - 2.5f + x, mapSize.y/2 - 1.5f - y);

				Transform newTile = Instantiate(tilePrefab, tilePosition, Quaternion.Euler(Vector3.right)) as Transform;
				newTile.localScale = Vector3.one * (1-outlinePercent);
			    newTile.transform.parent = GameObject.Find("MapHolder").transform;
				Debug.Log (tilePosition);
				map.Add (tilePosition);

			}
		}
		var test = ReturnTile (2);
    }

	public Vector3 ReturnTile(int tileNumber)
	{
		return (Vector3)map[tileNumber];
	}
}