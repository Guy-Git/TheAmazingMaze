using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimedScoreboard : MonoBehaviour
{
    public List<Text> scores;
    // Start is called before the first frame update
    void Start()
    {
        ScoresContainer highscores = JsonUtility.FromJson<ScoresContainer>(PlayerPrefs.GetString("TimedHighscores"));

        try
        {
            List<string> easyScores = highscores.easyScores;
            List<string> mediumScores = highscores.mediumScores;
            List<string> hardScores = highscores.hardScores;

            easyScores = SortScores(easyScores);
            easyScores.Reverse();

            mediumScores = SortScores(mediumScores);
            mediumScores.Reverse();

            hardScores = SortScores(hardScores);
            hardScores.Reverse();

            for (int i = 0; i < 5; i++)
            {
                try
                {
                    float minutes = Mathf.FloorToInt(float.Parse(easyScores[i].Split()[1]) / 60);
                    float seconds = Mathf.FloorToInt(float.Parse(easyScores[i].Split()[1]) % 60);

                    easyScores[i] = easyScores[i].Split()[0] + " " + string.Format("{0:00}:{1:00}", minutes, seconds);
                }

                catch
                {
                    easyScores.Add("");
                }

                try
                {
                    float minutes = Mathf.FloorToInt(float.Parse(mediumScores[i].Split()[1]) / 60);
                    float seconds = Mathf.FloorToInt(float.Parse(mediumScores[i].Split()[1]) % 60);

                    mediumScores[i] = mediumScores[i].Split()[0] + " " + string.Format("{0:00}:{1:00}", minutes, seconds);
                }

                catch
                {
                    mediumScores.Add("");
                }

                try
                {
                    float minutes = Mathf.FloorToInt(float.Parse(hardScores[i].Split()[1]) / 60);
                    float seconds = Mathf.FloorToInt(float.Parse(hardScores[i].Split()[1]) % 60);

                    hardScores[i] = hardScores[i].Split()[0] + " " + string.Format("{0:00}:{1:00}", minutes, seconds);
                }

                catch
                {
                    hardScores.Add("");
                }
            }

            scores[0].text = easyScores[0];
            scores[1].text = easyScores[1];
            scores[2].text = easyScores[2];
            scores[3].text = easyScores[3];
            scores[4].text = easyScores[4];

            scores[5].text = mediumScores[0];
            scores[6].text = mediumScores[1];
            scores[7].text = mediumScores[2];
            scores[8].text = mediumScores[3];
            scores[9].text = mediumScores[4];

            scores[10].text = hardScores[0];
            scores[11].text = hardScores[1];
            scores[12].text = hardScores[2];
            scores[13].text = hardScores[3];
            scores[14].text = hardScores[4];
        }

        catch
        {

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private List<string> SortScores(List<string> scores)
    {
        float globalMin;
        int sortIndex = 0;
        for (int i = 0; i < scores.Count; i++)
        {
            globalMin = 100000;

            for (int j = i; j < scores.Count; j++)
            {
                float jElement = float.Parse(scores[j].Split()[1]);

                if (jElement < globalMin)
                {
                    globalMin = jElement;
                    sortIndex = j;
                }
            }

            Swap<string>(scores, i, sortIndex);

        }

        return scores;
    }

    private static void Swap<T>(List<T> list, int indexA, int indexB)
    {
        T tmp = list[indexA];
        list[indexA] = list[indexB];
        list[indexB] = tmp;
    }

}
