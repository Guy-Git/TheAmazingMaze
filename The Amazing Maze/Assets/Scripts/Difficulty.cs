using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Difficulty : MonoBehaviour
{


    public void Easy()
    {
        Debug.Log("Kaki");
        SceneManager.LoadScene("SampleScene");
        //properties = new Properties();
        if(Properties.chosenMode == 0)
        {
            Properties.rows = 5;
            Properties.cols = 5;
        }
        else
        {
            Properties.rows = 10;
            Properties.cols = 10;
            Properties.time = 600;
        }


        Debug.Log(Properties.cols);
    }

    public void Medium()
    {
        SceneManager.LoadScene("SampleScene");
        //properties = new Properties();

        Properties.rows = 10;
        Properties.cols = 10;

        if (Properties.chosenMode == 1)
        {
            Properties.time = 300;
        }
    }

    public void Hard()
    {
        Debug.Log("Kaki");
        SceneManager.LoadScene("SampleScene");
        //properties = new Properties();

        Properties.rows = 20;
        Properties.cols = 20;

        if (Properties.chosenMode == 1)
        {
            Properties.time = 120;
            Properties.rows = 10;
            Properties.cols = 10;
        }
    }
}
