using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        HallCreating();
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
        mazeRows = Properties.rows;
        mazeColumns = Properties.cols;
        mazeCells = new MazeCell[mazeRows, mazeColumns];

        for (int r = 0; r < mazeRows; r++)
        {
            for (int c = 0; c < mazeColumns; c++)
            {
           
                mazeCells[r, c] = new MazeCell();
                mazeCells[r, c].floor = Instantiate(wall, new Vector3(r * size, -(size / 2f), c * size), Quaternion.identity) as GameObject;
                mazeCells[r, c].floor.name = "Floor " + r + "," + c;
                mazeCells[r, c].floor.transform.Rotate(Vector3.right, 90f);
              
                if (c == 0)
                {
                    mazeCells[r, c].westWall = Instantiate(wall, new Vector3(r * size, 0, (c * size) - (size / 2f)), Quaternion.identity) as GameObject;
                    mazeCells[r, c].westWall.name = "West Wall " + r + "," + c;
                }

                mazeCells[r, c].eastWall = Instantiate(wall, new Vector3(r * size, 0, (c * size) + (size / 2f)), Quaternion.identity) as GameObject;
                mazeCells[r, c].eastWall.name = "East Wall " + r + "," + c;

                if (r == 0)
                {
                    mazeCells[r, c].northWall = Instantiate(wall, new Vector3((r * size) - (size / 2f), 0, c * size), Quaternion.identity) as GameObject;
                    mazeCells[r, c].northWall.name = "North Wall " + r + "," + c;
                    mazeCells[r, c].northWall.transform.Rotate(Vector3.up * 90f);
                }

                mazeCells[r, c].southWall = Instantiate(wall, new Vector3((r * size) + (size / 2f), 0, c * size), Quaternion.identity) as GameObject;
                mazeCells[r, c].southWall.name = "South Wall " + r + "," + c;
                mazeCells[r, c].southWall.transform.Rotate(Vector3.up * 90f);
                
        }
        }
    }
    private void HallCreating()
    {
        int direction = UnityEngine.Random.Range(0, 2);
        Debug.Log(direction);
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


    private void DestroyWallIfItExists(GameObject wall)
    {
        if (wall != null)
        {
            GameObject.Destroy(wall);
        }
    }

}   
