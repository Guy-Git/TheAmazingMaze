using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{ 

    public void RegularGame()
    {
        SceneManager.LoadScene("ChooseDifficulty");
        Properties.chosenMode = 0;
    }

    public void TimedGame()
    {
        SceneManager.LoadScene("ChooseDifficulty");
        Properties.chosenMode = 1;
    }
}
