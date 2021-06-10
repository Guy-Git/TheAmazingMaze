using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Difficulty : MonoBehaviour
{
    Properties properties;

    public void Easy()
    {
        Debug.Log("Kaki");
        SceneManager.LoadScene("SampleScene");
        //properties = new Properties();

        Properties.rows = 5;
        Properties.cols = 5;

        Debug.Log(Properties.cols);
    }

    public void Medium()
    {
        SceneManager.LoadScene("SampleScene");
        //properties = new Properties();

        Properties.rows = 10;
        Properties.cols = 10;
    }

    public void Hard()
    {
        Debug.Log("Kaki");
        SceneManager.LoadScene("SampleScene");
        //properties = new Properties();

        Properties.rows = 20;
        Properties.cols = 20;
    }
}
