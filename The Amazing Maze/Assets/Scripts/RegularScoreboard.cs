using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RegularScoreboard : MonoBehaviour
{
    public List<Text> scores; 
    // Start is called before the first frame update
    void Start()
    {
        ScoresContainer highscores = JsonUtility.FromJson<ScoresContainer>(PlayerPrefs.GetString("RegularHighscores"));
        
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
                string x = easyScores[i];
            }

            catch
            {
                easyScores.Add("");
            }

            try
            {
                string x = mediumScores[i];
            }

            catch
            {
                mediumScores.Add("");
            }

            try
            {
                string x = hardScores[i];
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

    // Update is called once per frame
    void Update()
    {
        
    }

    private List<string> SortScores(List<string> scores)
    {
        int globalMin;
        int sortIndex = 0;
        for (int i = 0; i < scores.Count; i++)
        {
            globalMin = 100000;

            for (int j = i; j < scores.Count; j++)
            {
                int jElement = Int32.Parse(scores[j].Split()[1]);

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
