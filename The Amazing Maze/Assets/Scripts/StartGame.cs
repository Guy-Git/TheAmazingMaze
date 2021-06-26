using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    //public GameObject wall;

    public void RegularGame()
    {
        SceneManager.LoadScene("ChooseDifficulty");
        Properties.chosenMode = 0;
        // Debug.Log(wall.GetComponent<Collider>().bounds.size);

    }

    public void TimedGame()
    {
        SceneManager.LoadScene("ChooseDifficulty");
        Properties.chosenMode = 1;
    }

    public void RegularScoreboard()
    {
        SceneManager.LoadScene("RegularScoreboard");
    }

    public void TimedScoreboard()
    {
        SceneManager.LoadScene("TimedScoreboard");
    }

    public void Controls()
    {
        SceneManager.LoadScene("Controls");
    }

    public void Exit()
    {
        Application.Quit();
    }

}
