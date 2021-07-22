using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class backToMenu : MonoBehaviour
{
    public Text name;
    // Start is called before the first frame update
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void WinMainMenu()
    {
        ScoresContainer regularHighscores;
        ScoresContainer timedHighscores;

        if (Properties.chosenMode == 0)
        {
            //Debug.Log(PlayerPrefs.GetString("Highscores"));
            if (!PlayerPrefs.GetString("RegularHighscores").Equals(""))
            {
                regularHighscores = JsonUtility.FromJson<ScoresContainer>(PlayerPrefs.GetString("RegularHighscores"));
                PlayerPrefs.DeleteAll();
            }

            else
            {
                regularHighscores = new ScoresContainer();
                regularHighscores.easyScores = new List<string>();
                regularHighscores.mediumScores = new List<string>();
                regularHighscores.hardScores = new List<string>();
            }

            if (Properties.rows == 5)
                regularHighscores.easyScores.Add(name.text + " " + Properties.points.ToString());

            if (Properties.rows == 10)
                regularHighscores.mediumScores.Add(name.text + " " + Properties.points.ToString());

            if (Properties.rows == 20)
                regularHighscores.hardScores.Add(name.text + " " + Properties.points.ToString());

            string scores = JsonUtility.ToJson(regularHighscores);

            PlayerPrefs.SetString("RegularHighscores", scores);
            SceneManager.LoadScene("RegularScoreboard");
        }

        else
        {
            //Debug.Log(PlayerPrefs.GetString("Highscores"));
            if (!PlayerPrefs.GetString("TimedHighscores").Equals(""))
            {
                timedHighscores = JsonUtility.FromJson<ScoresContainer>(PlayerPrefs.GetString("TimedHighscores"));
                PlayerPrefs.DeleteAll();
            }

            else
            {
                timedHighscores = new ScoresContainer();
                timedHighscores.easyScores = new List<string>();
                timedHighscores.mediumScores = new List<string>();
                timedHighscores.hardScores = new List<string>();
            }

            if (Properties.time == 300)
                timedHighscores.easyScores.Add(name.text + " " + Timer.timeRemaining.ToString());

            if (Properties.time == 180)
                timedHighscores.mediumScores.Add(name.text + " " + Timer.timeRemaining.ToString());

            if (Properties.time == 90)
                timedHighscores.hardScores.Add(name.text + " " + Timer.timeRemaining.ToString());

            string scores = JsonUtility.ToJson(timedHighscores);
            //Debug.Log("KAKI");
            //Debug.Log(scores);

            PlayerPrefs.SetString("TimedHighscores", scores);
            SceneManager.LoadScene("TimedScoreboard");
        }
    }
}
