using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MazeLoader : MonoBehaviour
{
    public int mazeRows, mazeColumns;
    public GameObject wall;
    public GameObject SpikeFloor;
    public GameObject ChessFloor;
    public GameObject FallWall;
    public float size = 2f;
    public GameObject Coin;
    public GameObject Clock;
    public Text points;
    public GameObject trophy;
    public GameObject QuestionMark;
    public GameObject SlowFloor;

    private MazeCell[,] mazeCells;

    // Use this for initialization
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        if (Properties.chosenMode == 1)
            points.gameObject.SetActive(false);

        InitializeMaze();
        Properties.pPress = false;
        Properties.points = 0;

        int r = mazeRows - 1;
        int c = mazeColumns - 1;
        trophy = Instantiate(trophy, new Vector3(r * size, 2f + -(size / 2f), c * size), Quaternion.identity) as GameObject;
        trophy.gameObject.AddComponent<BoxCollider>();
        trophy.gameObject.GetComponent<Collider>().isTrigger = true;
        trophy.gameObject.AddComponent<Win>();

        trophy.name = "Trophy " + r + "," + c;

        MazeAlgorithm ma = new HuntAndKillMazeAlgorithm(mazeCells);
        ma.CreateMaze();


        //champions = new GameObject();
        //Properties properties = new Properties();

    }

    // Update is called once per frame
    void Update()
    {
        trophy.gameObject.transform.Rotate(0, 1, 0, Space.Self);

        points.text = Properties.points.ToString();
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameObject pausePanel = GameObject.Find("Canvas").transform.Find("PauseMenu").gameObject;
            FirstPersonController fpc = GameObject.Find("FirstPersonController").GetComponent<FirstPersonController>();
            if (!Properties.pPress)
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

    private void InitializeMaze()
    {

        mazeRows = Properties.rows;
        mazeColumns = Properties.cols;
        GameObject coin;
        GameObject questionMark;
        GameObject collectableClock;
        mazeCells = new MazeCell[mazeRows, mazeColumns];
        //Random rnd = new Random();
        GameObject lowCeiling;
        GameObject chessFloor;
        GameObject fallWall;


        for (int r = 0; r < mazeRows; r++)
        {
            for (int c = 0; c < mazeColumns; c++)
            {

                int putSpike = Random.Range(0, 8);
                int putChessFloor = Random.Range(0, 7);
                int putLowCeiling = Random.Range(0, 7);
                int putClock = Random.Range(0, 8);
                int putQuestionMark = Random.Range(0, 9);
                int putClosingWalls = Random.Range(0, 9);
                int putSlowFloor = Random.Range(0, 10);

                if (!(r == 0 && c == 0) && !(r == mazeRows - 1 && c == mazeColumns - 1) && putSpike == 0)
                {
                    mazeCells[r, c] = new MazeCell();
                    mazeCells[r, c].floor = Instantiate(SpikeFloor, new Vector3(r * size, -(size / 2f), c * size), Quaternion.identity) as GameObject;
                    mazeCells[r, c].floor.name = "Spike Floor " + r + "," + c;
                    mazeCells[r, c].floor.transform.Rotate(Vector3.right, 90f);
                }

                else if (!(r == 0 && c == 0) && !(r == mazeRows - 1 && c == mazeColumns - 1) && putSpike != 0 && putLowCeiling != 0 && putChessFloor == 0)
                {
                    // Chess Floor:
                    mazeCells[r, c] = new MazeCell();
                    mazeCells[r, c].floor = Instantiate(ChessFloor, new Vector3(r * size - 1.375f, -(size / 2f), c * size - 1.375f), Quaternion.identity);
                    mazeCells[r, c].floor.name = "Chess Floor " + r + "," + c;
                    mazeCells[r, c].floor.transform.Rotate(Vector3.right, 90f);

                    // Fall Wall:
                    // Floor - 
                    fallWall = Instantiate(FallWall, new Vector3(r * size, -(size / 2f) - size, c * size), Quaternion.identity);
                    fallWall.transform.Rotate(Vector3.right, 90f);
                    // West - 
                    fallWall = Instantiate(FallWall, new Vector3(r * size, -size, (c * size) - (size / 2f)), Quaternion.identity);
                    // East - 
                    fallWall = Instantiate(FallWall, new Vector3(r * size, -size, (c * size) + (size / 2f)), Quaternion.identity);
                    // North - 
                    fallWall = Instantiate(FallWall, new Vector3((r * size) - (size / 2f), -size, c * size), Quaternion.identity);
                    fallWall.transform.Rotate(Vector3.up * 90f);
                    // South - 
                    fallWall = Instantiate(FallWall, new Vector3((r * size) + (size / 2f), -size, c * size), Quaternion.identity);
                    fallWall.transform.Rotate(Vector3.up * 90f);
                }


                else if (!(r == 0 && c == 0) && !(r == mazeRows - 1 && c == mazeColumns - 1) && putSpike != 0 && putLowCeiling != 0 && putChessFloor != 0 && putSlowFloor == 0)
                {
                    mazeCells[r, c] = new MazeCell();
                    mazeCells[r, c].floor = Instantiate(SlowFloor, new Vector3(r * size, -(size / 2f), c * size), Quaternion.identity);
                    mazeCells[r, c].floor.transform.Rotate(Vector3.right, 90f);
                    mazeCells[r, c].floor.tag = "SlowFloor";
                }

                else
                {
                    mazeCells[r, c] = new MazeCell();
                    mazeCells[r, c].floor = Instantiate(wall, new Vector3(r * size, -(size / 2f), c * size), Quaternion.identity) as GameObject;
                    mazeCells[r, c].floor.name = "Floor " + r + "," + c;
                    mazeCells[r, c].floor.transform.Rotate(Vector3.right, 90f);
                }

                if (!(r == 0 && c == 0) && !(r == mazeRows - 1 && c == mazeColumns - 1) && Properties.chosenMode == 0 && putLowCeiling != 0 && putQuestionMark == 0)
                {
                    questionMark = Instantiate(QuestionMark, new Vector3(r * size, -(size / 2f), c * size), Quaternion.identity);
                    questionMark.gameObject.AddComponent<BoxCollider>();
                    questionMark.gameObject.GetComponent<Collider>().isTrigger = true;
                    questionMark.gameObject.AddComponent<QuestionMarkBehaviour>();
                }

                else if (!(r == 0 && c == 0) && !(r == mazeRows - 1 && c == mazeColumns - 1) && Properties.chosenMode == 0 && putLowCeiling != 0)
                {
                    coin = Instantiate(Coin, new Vector3(r * size, -(size / 2f), c * size), Quaternion.identity);
                    coin.gameObject.AddComponent<BoxCollider>();
                    coin.gameObject.GetComponent<Collider>().isTrigger = true;
                    coin.gameObject.AddComponent<CollectableBehaviour>();
                }

                if (!(r == 0 && c == 0) && !(r == mazeRows - 1 && c == mazeColumns - 1) && Properties.chosenMode == 1 && putClock == 0 && putLowCeiling != 0)
                {
                    collectableClock = Instantiate(Clock, new Vector3(r * size, -(size / 2f), c * size), Quaternion.identity);
                    collectableClock.gameObject.AddComponent<BoxCollider>();
                    collectableClock.gameObject.GetComponent<Collider>().isTrigger = true;
                    collectableClock.gameObject.AddComponent<CollectableBehaviour>();
                }

                if (!(r == 0 && c == 0) && !(r == mazeRows - 1 && c == mazeColumns - 1) && putSpike != 0 && putLowCeiling == 0)
                {
                    lowCeiling = Instantiate(wall, new Vector3(r * size, -0.6f, c * size), Quaternion.identity);
                    lowCeiling.transform.Rotate(Vector3.right, 90f);
                    lowCeiling = Instantiate(wall, new Vector3(r * size, 0f, c * size), Quaternion.identity);
                    lowCeiling.transform.Rotate(Vector3.right, 90f);
                    lowCeiling = Instantiate(wall, new Vector3(r * size, 0.6f, c * size), Quaternion.identity);
                    lowCeiling.transform.Rotate(Vector3.right, 90f);
                    lowCeiling = Instantiate(wall, new Vector3(r * size, 1.2f, c * size), Quaternion.identity);
                    lowCeiling.transform.Rotate(Vector3.right, 90f);
                    lowCeiling = Instantiate(wall, new Vector3(r * size, 1.8f, c * size), Quaternion.identity);
                    lowCeiling.transform.Rotate(Vector3.right, 90f);
                    lowCeiling = Instantiate(wall, new Vector3(r * size, 2.4f, c * size), Quaternion.identity);
                    lowCeiling.transform.Rotate(Vector3.right, 90f);
                }

                if (c == 0)
                {
                    mazeCells[r, c].westWall = Instantiate(wall, new Vector3(r * size, 0, (c * size) - (size / 2f)), Quaternion.identity) as GameObject;
                    mazeCells[r, c].westWall.name = "West Wall " + r + "," + c;
                    mazeCells[r, c].westWall.tag = "wall";
                }

                mazeCells[r, c].eastWall = Instantiate(wall, new Vector3(r * size, 0, (c * size) + (size / 2f)), Quaternion.identity) as GameObject;
                mazeCells[r, c].eastWall.name = "East Wall " + r + "," + c;
                mazeCells[r, c].eastWall.tag = "wall";

                if (r == 0)
                {
                    mazeCells[r, c].northWall = Instantiate(wall, new Vector3((r * size) - (size / 2f), 0, c * size), Quaternion.identity) as GameObject;
                    mazeCells[r, c].northWall.name = "North Wall " + r + "," + c;
                    mazeCells[r, c].northWall.transform.Rotate(Vector3.up * 90f);
                    mazeCells[r, c].northWall.tag = "wall";

                }

                mazeCells[r, c].southWall = Instantiate(wall, new Vector3((r * size) + (size / 2f), 0, c * size), Quaternion.identity) as GameObject;
                mazeCells[r, c].southWall.name = "South Wall " + r + "," + c;
                mazeCells[r, c].southWall.transform.Rotate(Vector3.up * 90f);
                mazeCells[r, c].southWall.tag = "wall";

                if (putClosingWalls == 0)
                {
                    mazeCells[r, c].southWall.gameObject.AddComponent<ClosingWallsBehaviour>();
                    mazeCells[r, c].southWall.tag = "closingWall";
                }
                
            }
        }
    }
}
