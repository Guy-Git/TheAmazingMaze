using UnityEngine;
using System.Collections;

public class MazeLoader : MonoBehaviour {
	public int mazeRows, mazeColumns;
	public GameObject wall;
	public GameObject champions;
	public float size = 2f;

	private MazeCell[,] mazeCells;

	// Use this for initialization
	void Start () {
		InitializeMaze ();

		MazeAlgorithm ma = new HuntAndKillMazeAlgorithm (mazeCells);
		ma.CreateMaze ();

		//champions = new GameObject();
		//Properties properties = new Properties();
	}
	
	// Update is called once per frame
	void Update () {
	}

	private void InitializeMaze() {

		mazeRows = Properties.rows;
		Debug.Log(mazeRows);
		mazeColumns = Properties.cols;

		mazeCells = new MazeCell[mazeRows,mazeColumns];

		for (int r = 0; r < mazeRows; r++) {
			for (int c = 0; c < mazeColumns; c++)
			{
				mazeCells[r, c] = new MazeCell();

				// For now, use the same wall object for the floor!
				if (r == mazeRows - 1 && c == mazeColumns - 1)
				{
					mazeCells[r, c].floor = Instantiate(champions, new Vector3(r * size, 0.25f + -(size / 2f), c * size), Quaternion.identity) as GameObject;
					mazeCells[r, c].floor.gameObject.AddComponent<BoxCollider>();
					mazeCells[r, c].floor.gameObject.GetComponent<Collider>().isTrigger = true;
					Win win = mazeCells[r, c].floor.gameObject.AddComponent<Win>();


					mazeCells[r, c].floor.name = "Trophy " + r + "," + c;
					//mazeCells[r, c].floor.transform.Rotate(Vector3.right, 90f);
				}

				mazeCells[r, c].floor = Instantiate(wall, new Vector3(r * size, -(size / 2f), c * size), Quaternion.identity) as GameObject;
				mazeCells[r, c].floor.name = "Floor " + r + "," + c;
				mazeCells[r, c].floor.transform.Rotate(Vector3.right, 90f);


				if (c == 0) {
					mazeCells[r,c].westWall = Instantiate (wall, new Vector3 (r*size, 0, (c*size) - (size/2f)), Quaternion.identity) as GameObject;
					mazeCells [r, c].westWall.name = "West Wall " + r + "," + c;
				}

				mazeCells [r, c].eastWall = Instantiate (wall, new Vector3 (r*size, 0, (c*size) + (size/2f)), Quaternion.identity) as GameObject;
				mazeCells [r, c].eastWall.name = "East Wall " + r + "," + c;

				if (r == 0) {
					mazeCells [r, c].northWall = Instantiate (wall, new Vector3 ((r*size) - (size/2f), 0, c*size), Quaternion.identity) as GameObject;
					mazeCells [r, c].northWall.name = "North Wall " + r + "," + c;
					mazeCells [r, c].northWall.transform.Rotate (Vector3.up * 90f);
				}

				mazeCells[r,c].southWall = Instantiate (wall, new Vector3 ((r*size) + (size/2f), 0, c*size), Quaternion.identity) as GameObject;
				mazeCells [r, c].southWall.name = "South Wall " + r + "," + c;
				mazeCells [r, c].southWall.transform.Rotate (Vector3.up * 90f);
			}
		}
	}
}
