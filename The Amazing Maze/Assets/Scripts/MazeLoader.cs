using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MazeLoader : MonoBehaviour {
	public int mazeRows, mazeColumns;
	public GameObject wall;
	public GameObject champions;
	public GameObject SpikeFloor;
	public float size = 2f;
	public GameObject Coin;
	public Text points;

	private MazeCell[,] mazeCells;

	// Use this for initialization
	void Start () {
		InitializeMaze ();
		Properties.pPress = false;
		Properties.points = 0;

		int r = mazeRows - 1;
		int c = mazeColumns - 1;
		mazeCells[r, c].floor = Instantiate(champions, new Vector3(r * size, 2f + -(size / 2f), c * size), Quaternion.identity) as GameObject;
		mazeCells[r, c].floor.gameObject.AddComponent<BoxCollider>();
		mazeCells[r, c].floor.gameObject.GetComponent<Collider>().isTrigger = true;
		mazeCells[r, c].floor.gameObject.AddComponent<Win>();
	
		mazeCells[r, c].floor.name = "Trophy " + r + "," + c;

		MazeAlgorithm ma = new HuntAndKillMazeAlgorithm (mazeCells);
		ma.CreateMaze ();
		
		
		//champions = new GameObject();
		//Properties properties = new Properties();
		
	}
	
	// Update is called once per frame
	void Update () {
		mazeCells[mazeRows - 1, mazeColumns - 1].floor.gameObject.transform.Rotate(0, 1, 0, Space.Self);

		points.text = Properties.points.ToString();
		if(Input.GetKeyDown(KeyCode.P))
        {
			GameObject pausePanel = GameObject.Find("Canvas").transform.Find("PauseMenu").gameObject;
			FirstPersonController fpc = GameObject.Find("FirstPersonController").GetComponent<FirstPersonController>();
			if(!Properties.pPress)
            {
				Properties.pPress = true;			
				pausePanel.SetActive(true);
				fpc.cameraCanMove = false;
				fpc.playerCanMove = false;
				Cursor.lockState = CursorLockMode.None;	
            }
            else
            {
				Properties.pPress = false;
				pausePanel.SetActive(false);
				fpc.cameraCanMove = true;
				fpc.playerCanMove = true;
				Cursor.lockState = CursorLockMode.Locked;
			}

        }
	
	}

	private void InitializeMaze() {

		mazeRows = Properties.rows;
		Debug.Log(mazeRows);
		mazeColumns = Properties.cols;
		GameObject coin;
		mazeCells = new MazeCell[mazeRows,mazeColumns];
		//Random rnd = new Random();

		for (int r = 0; r < mazeRows; r++) {
			for (int c = 0; c < mazeColumns; c++)
			{

				int putSpike = Random.Range(0, 7);

				if (!(r == 0 && c == 0) && !(r == mazeRows - 1 && c == mazeColumns - 1) && putSpike == 0)
				{
					mazeCells[r, c] = new MazeCell();
					mazeCells[r, c].floor = Instantiate(SpikeFloor, new Vector3(r * size, -(size / 2f), c * size), Quaternion.identity) as GameObject;
					mazeCells[r, c].floor.name = "Spike Floor " + r + "," + c;
					mazeCells[r, c].floor.transform.Rotate(Vector3.right, 90f);
				}

				else
				{
					mazeCells[r, c] = new MazeCell();
					mazeCells[r, c].floor = Instantiate(wall, new Vector3(r * size, -(size / 2f), c * size), Quaternion.identity) as GameObject;
					mazeCells[r, c].floor.name = "Floor " + r + "," + c;
					mazeCells[r, c].floor.transform.Rotate(Vector3.right, 90f);
				}

				if (!(r == 0 && c == 0) && !(r == mazeRows - 1 && c == mazeColumns - 1))
				{
					coin = Instantiate(Coin, new Vector3(r * size, -(size / 2f), c * size), Quaternion.identity);
					coin.gameObject.AddComponent<BoxCollider>();
					coin.gameObject.GetComponent<Collider>().isTrigger = true;
					coin.gameObject.AddComponent<CoinBehaviour>();
				}

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
