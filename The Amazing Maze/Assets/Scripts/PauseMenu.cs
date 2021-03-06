using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    Camera playerCamera;
    public Camera clueCam;
    public GameObject sphere;
    public Light light;
    public GameObject fpc;

    Quaternion lightBackup;

    Time time;
    bool timeIsRunning;
    float timeRemaining;

    private void Start()
    {
        timeIsRunning = false;
    }
    public void Clue()
    {
        playerCamera = GameObject.Find("PlayerCamera").GetComponent<Camera>();
        lightBackup = new Quaternion();
        lightBackup = light.transform.rotation;

        light.transform.eulerAngles = new Vector3(90, 0, 0);
        fpc.transform.position = new Vector3(fpc.transform.position.x, fpc.transform.position.y + 6f, fpc.transform.position.z);
        fpc.GetComponent<Rigidbody>().useGravity = false;

        clueCam.enabled = true;
        playerCamera.enabled = false;
        //playerCameraBackup = playerCamera;

        float mid = (Properties.rows - 1) * 3;
        int height = 0;

        if (Properties.rows == 5)
            height = 30;

        else if (Properties.rows == 10)
            height = 60;

        else if (Properties.rows == 20)
            height = 120;

        sphere.SetActive(true);
        clueCam.transform.eulerAngles = new Vector3(90, 0, 0);
        clueCam.transform.position = new Vector3(mid, height, mid);

        timeIsRunning = true;
        timeRemaining = 4;
        GameObject pausePanel = GameObject.Find("Canvas").transform.Find("PauseMenu").gameObject;
        GameObject canvas = GameObject.Find("Canvas");
        canvas.transform.Find("PauseMenu").SetParent(null);

        //pausePanel.GetComponent<Renderer>().enabled = false;
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Update()
    {


        if (timeIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;

            }
            else
            {
                timeRemaining = 0;
                timeIsRunning = false;
                endClue();
            }
        }
    }

    public void endClue()
    {
        FirstPersonController fpc = GameObject.Find("FirstPersonController").GetComponent<FirstPersonController>();
        playerCamera.enabled = true;
        clueCam.enabled = false;

        light.transform.rotation = lightBackup;
        
        GameObject pausePanel = GameObject.Find("PauseMenu").gameObject;

        pausePanel.transform.SetParent(GameObject.Find("Canvas").transform);

        pausePanel.SetActive(false);
        fpc.cameraCanMove = true;
        fpc.playerCanMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Properties.pPress = false;
        fpc.transform.position = new Vector3(fpc.transform.position.x, fpc.transform.position.y - 6f, fpc.transform.position.z);
        fpc.GetComponent<Rigidbody>().useGravity = true;
        sphere.SetActive(false);
    }
}
