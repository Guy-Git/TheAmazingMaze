using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LoaderHallMaze : MonoBehaviour
{
    public int mazeRows, mazeColumns;
    public GameObject wall;
    private MazeCell[,] mazeCells;
    public float size = 2f;
    public GameObject trophy;
    public Text points;
    private int currentRow = 0;
    private int currentCol = 0;
    public GameObject SpikeFloor;
    public GameObject ChessFloor;
    public GameObject FallWall;
    public GameObject Coin;
    public GameObject QuestionMark;
    public GameObject SlowFloor;
    public GameObject DoublePointes;
    public GameObject AntiGravFloor;
    public GameObject Invincibility;
    public GameObject Stairs;

    void Start()
    {
        InitializeHallMaze();
        int r = mazeRows - 1;
        int c = mazeColumns - 1;
        Properties.pPress = false;
        Properties.points = 0;
        trophy = Instantiate(trophy, new Vector3(r * size, 2f + -(size / 2f), c * size), Quaternion.identity) as GameObject;
        trophy.gameObject.AddComponent<BoxCollider>();
        trophy.gameObject.GetComponent<Collider>().isTrigger = true;
        trophy.gameObject.AddComponent<Win>();

        trophy.name = "Trophy " + r + "," + c;

        if (Properties.mazeType == 0)
        {
            HallCreating();
        }
        else
        {
            Debug.Log("SNAKE");
            SnakeHallCreating();
        }
    }


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

    private void InitializeHallMaze()
    {
        GameObject coin;
        mazeRows = Properties.rows;
        mazeColumns = Properties.cols;
        mazeCells = new MazeCell[mazeRows, mazeColumns];
        GameObject fallWall;
        GameObject lowCeiling;
        GameObject chessFloor;
        GameObject questionMark;
        GameObject doublePointes;
        GameObject invincibility;
        GameObject stairs;

        for (int r = 0; r < mazeRows; r++)
        {
            for (int c = 0; c < mazeColumns; c++)
            {
                int putSpike = Random.Range(0, 10);
                int putChessFloor = Random.Range(0, 10);
                int putLowCeiling = Random.Range(0, 10);
                int putQuestionMark = Random.Range(0, 10);
                int putClosingWalls = Random.Range(0, 10);
                int putSlowFloor = Random.Range(0, 10);
                int putIceFloor = Random.Range(0, 10);
                int putDoublePointes = Random.Range(0, 10);
                int putInvincibility = Random.Range(0, 10);
                int putStairs = Random.Range(0, 10);


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
                else if (!(r == 0 && c == 0) && !(r == mazeRows - 1 && c == mazeColumns - 1) && putSpike != 0 && putLowCeiling != 0 && putChessFloor != 0 && putSlowFloor != 0 && putIceFloor == 0)
                {
                    mazeCells[r, c] = new MazeCell();
                    mazeCells[r, c].floor = Instantiate(AntiGravFloor, new Vector3(r * size, -(size / 2f), c * size), Quaternion.identity);
                    mazeCells[r, c].floor.transform.Rotate(Vector3.right, 90f);
                }
                else if ((!(r == 0 && c == 0) && !(r == mazeRows - 1 && c == mazeColumns - 1) && putSpike != 0 && putLowCeiling != 0 && putChessFloor != 0 && putSlowFloor != 0 && putIceFloor != 0 && putStairs == 0))
                {
                    mazeCells[r, c] = new MazeCell();
                    mazeCells[r, c].floor = Instantiate(Stairs, new Vector3(r * size, -(size / 2f), c * size), Quaternion.identity);
                    mazeCells[r, c].floor.transform.Rotate(Vector3.right, 270f);
                }
                else
                {
                    mazeCells[r, c] = new MazeCell();
                    mazeCells[r, c].floor = Instantiate(wall, new Vector3(r * size, -(size / 2f), c * size), Quaternion.identity) as GameObject;
                    mazeCells[r, c].floor.name = "Floor " + r + "," + c;
                    mazeCells[r, c].floor.transform.Rotate(Vector3.right, 90f);
                }

                if (!(r == 0 && c == 0) && !(r == mazeRows - 1 && c == mazeColumns - 1) && putLowCeiling != 0 && putQuestionMark == 0)
                {
                    questionMark = Instantiate(QuestionMark, new Vector3(r * size, -(size / 2f), c * size), Quaternion.identity);
                    questionMark.gameObject.AddComponent<BoxCollider>();
                    questionMark.gameObject.GetComponent<Collider>().isTrigger = true;
                    questionMark.gameObject.AddComponent<QuestionMarkBehaviour>();
                }

                else if (!(r == 0 && c == 0) && !(r == mazeRows - 1 && c == mazeColumns - 1) && putLowCeiling != 0 && putDoublePointes == 0)
                {
                    doublePointes = Instantiate(DoublePointes, new Vector3(r * size, -(size / 2f), c * size), Quaternion.identity);
                    doublePointes.gameObject.AddComponent<BoxCollider>();
                    doublePointes.gameObject.GetComponent<Collider>().isTrigger = true;
                    doublePointes.gameObject.AddComponent<DoublePointesBehaviour>();
                }
                else if (!(r == 0 && c == 0) && !(r == mazeRows - 1 && c == mazeColumns - 1) && putLowCeiling != 0 && putInvincibility == 0)
                {
                    invincibility = Instantiate(Invincibility, new Vector3(r * size, -(size / 2f), c * size), Quaternion.identity);
                    invincibility.gameObject.AddComponent<BoxCollider>();
                    invincibility.gameObject.GetComponent<Collider>().isTrigger = true;
                    invincibility.gameObject.AddComponent<InvincibleBehaviour>();
                }

                else if (!(r == 0 && c == 0) && !(r == mazeRows - 1 && c == mazeColumns - 1) && putLowCeiling != 0)
                {
                    coin = Instantiate(Coin, new Vector3(r * size, -(size / 2f), c * size), Quaternion.identity);
                    coin.gameObject.AddComponent<BoxCollider>();
                    coin.gameObject.GetComponent<Collider>().isTrigger = true;
                    coin.gameObject.AddComponent<CollectableBehaviour>();
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
                    mazeCells[r, c].westWall.tag = "high wall";

                    mazeCells[r, c].westWall.transform.localScale = new Vector3(mazeCells[r, c].westWall.transform.localScale.x,
                    mazeCells[r, c].westWall.transform.localScale.y * 2,
                    mazeCells[r, c].westWall.transform.localScale.z);
                
                }

                mazeCells[r, c].eastWall = Instantiate(wall, new Vector3(r * size, 0, (c * size) + (size / 2f)), Quaternion.identity) as GameObject;
                mazeCells[r, c].eastWall.name = "East Wall " + r + "," + c;
                mazeCells[r, c].eastWall.tag = "wall";


                if (c == Properties.cols - 1)
                {
                    mazeCells[r, c].eastWall.transform.localScale = new Vector3(mazeCells[r, c].eastWall.transform.localScale.x,
                    mazeCells[r, c].eastWall.transform.localScale.y * 2,
                    mazeCells[r, c].eastWall.transform.localScale.z);
                    mazeCells[r, c].eastWall.tag = "high wall";

                }

                if (r == 0)
                {
                    mazeCells[r, c].northWall = Instantiate(wall, new Vector3((r * size) - (size / 2f), 0, c * size), Quaternion.identity) as GameObject;
                    mazeCells[r, c].northWall.name = "North Wall " + r + "," + c;
                    mazeCells[r, c].northWall.transform.Rotate(Vector3.up * 90f);
                    mazeCells[r, c].northWall.tag = "high wall";

                    mazeCells[r, c].northWall.transform.localScale = new Vector3(mazeCells[r, c].northWall.transform.localScale.x,
                    mazeCells[r, c].northWall.transform.localScale.y * 2,
                    mazeCells[r, c].northWall.transform.localScale.z);
                }

                mazeCells[r, c].southWall = Instantiate(wall, new Vector3((r * size) + (size / 2f), 0, c * size), Quaternion.identity) as GameObject;
                mazeCells[r, c].southWall.name = "South Wall " + r + "," + c;
                mazeCells[r, c].southWall.transform.Rotate(Vector3.up * 90f);
                mazeCells[r, c].southWall.tag = "wall";

                if (r == Properties.rows - 1)
                {
                    mazeCells[r, c].southWall.transform.localScale = new Vector3(mazeCells[r, c].southWall.transform.localScale.x,
                    mazeCells[r, c].southWall.transform.localScale.y * 2,
                    mazeCells[r, c].southWall.transform.localScale.z);
                    mazeCells[r, c].southWall.tag = "high wall";

                }

                if (putClosingWalls == 0)
                {
                    mazeCells[r, c].southWall.gameObject.AddComponent<ClosingWallsBehaviour>();
                    mazeCells[r, c].southWall.tag = "closingWall";
                }
            }
        }
    }

    private void HallCreating()
    {
        int direction = UnityEngine.Random.Range(0, 2);
        if (direction == 0)// halls of the maze going to be horizontal
        {
            while (currentRow < mazeRows && currentCol < mazeColumns)
            {

                if (currentCol == mazeColumns - 1 && currentRow != mazeRows - 1)
                {
                    int entry = UnityEngine.Random.Range(0, mazeRows);
                    DestroyWallIfItExists(mazeCells[currentRow, entry].southWall);
                    currentCol = 0;
                    currentRow++;
                }
                //destroy east wall to create hall
                if (!(currentCol == mazeColumns - 1 && currentRow == mazeRows - 1))
                {
                    DestroyWallIfItExists(mazeCells[currentRow, currentCol].eastWall);
                }
                currentCol++;

            }

        }
        else
        {
            while (currentRow < mazeRows && currentCol < mazeColumns)
            {

                if (currentRow == mazeRows - 1 && currentCol != mazeRows - 1)
                {
                    int entry = UnityEngine.Random.Range(0, mazeRows);
                    DestroyWallIfItExists(mazeCells[entry, currentCol].eastWall);
                    currentRow = 0;
                    currentCol++;
                }

                //destroy south wall to create hall
                if (!(currentCol == mazeColumns - 1 && currentRow == mazeRows - 1))

                    DestroyWallIfItExists(mazeCells[currentRow, currentCol].southWall);

                currentRow++;
            }
        }

    }

    private void SnakeHallCreating()
    {

        while (currentRow < mazeRows && currentCol < mazeColumns)
        {

            if ((currentCol < mazeColumns / 2) && currentRow != mazeRows - 1)
            {

                DestroyWallIfItExists(mazeCells[currentRow, currentCol].southWall);

            }

            else if (currentCol == mazeColumns / 2 && currentRow != mazeRows - 1)
            {
                currentRow++;
                currentCol = -1;

            }

            if (currentCol == mazeColumns / 2 && currentRow == mazeRows - 1)
            {
                for (int i = 0; i < (mazeColumns / 2); i++)
                {
                    if (i % 2 == 0)
                    {
                        DestroyWallIfItExists(mazeCells[mazeColumns - 1, i].eastWall);
                    }
                    else
                    {
                        DestroyWallIfItExists(mazeCells[0, i].eastWall);

                    }
                }
            }
            currentCol++;
        }


        currentCol = mazeColumns / 2;
        currentRow = 0;

        while (currentRow < mazeRows && currentCol < mazeColumns)
        {

            if ((currentCol >= mazeColumns / 2) && currentRow != mazeRows - 1)
            {
                if (currentCol != mazeRows - 1)
                    DestroyWallIfItExists(mazeCells[currentRow, currentCol].eastWall);
            }

            else if (currentRow == mazeRows - 1 && currentCol != mazeRows - 1)
            {
                DestroyWallIfItExists(mazeCells[currentRow, currentCol].eastWall);
                Debug.Log(currentRow);
                currentCol++;
                currentRow = -1;


            }
            if (currentCol == mazeColumns - 1 && currentRow == mazeRows - 1)
            {
                for (int i = mazeColumns / 2; i < mazeColumns - 1; i++)
                {
                    if (i % 2 == 0)
                    {
                        DestroyWallIfItExists(mazeCells[mazeColumns - 1, i].eastWall);
                    }
                    else
                    {
                        DestroyWallIfItExists(mazeCells[0, i].eastWall);

                    }
                }
            }
            currentRow++;
        }

        for (int i = 0; i < mazeRows ; i++)
        { 
            mazeCells[i, 6].eastWall = Instantiate(wall, new Vector3(i * size, 0, (6 * size) - (size / 2f)), Quaternion.identity) as GameObject;
            mazeCells[i, 6].eastWall.name = "east Wall " + i + "," + '6';
            mazeCells[i, 6].eastWall.tag = "wall";
            if (i != mazeRows - 1)
                DestroyWallIfItExists(mazeCells[i, 5].southWall);
        }
        DestroyWallIfItExists(mazeCells[0, 6].eastWall);

        for (int i = 0; i < mazeRows - 1 ; i++)
        {
            if (i % 2 == 0)
            {
                DestroyWallIfItExists(mazeCells[i, mazeColumns -1].southWall);
            }
            else
            {
                DestroyWallIfItExists(mazeCells[i, 6].southWall);

            }
        }
    }

    private void DestroyWallIfItExists(GameObject wall)
    {
        if (wall != null)
        {
            GameObject.Destroy(wall);
        }
    }

}   
