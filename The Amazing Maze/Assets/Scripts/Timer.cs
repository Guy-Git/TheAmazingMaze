using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timeText;
    public static float timeRemaining;
    public static bool timeIsRunning = false;
    public GameObject losePanel;

    // Start is called before the first frame update
    void Start()
    {

        timeRemaining = Properties.time;

        if(Properties.chosenMode == 0)
        {
            timeText.gameObject.SetActive(false);
            timeIsRunning = false;
        }
        else
        {
            timeIsRunning = true;
        }
            
    }

    // Update is called once per frame
    void Update()
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
                timeEnded();
            }

            DisplayTime(timeRemaining);
        }
    }
    public void timeEnded()
    {
        losePanel = GameObject.Find("Canvas").transform.Find("losePanel").gameObject;
        losePanel.SetActive(true);
        FirstPersonController fpc = GameObject.Find("FirstPersonController").GetComponent<FirstPersonController>();
        fpc.cameraCanMove = false;
        fpc.playerCanMove = false;
        Cursor.lockState = CursorLockMode.None;
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

