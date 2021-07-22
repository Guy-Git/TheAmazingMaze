using UnityEngine;
using System.Collections;

public class DoublePointesBehaviour : MonoBehaviour
{
    Time time;
    bool timeIsRunning;
    float timeRemaining;
    bool goDown;
    // Use this for initialization
    void Start()
    {
        timeIsRunning = true;
        timeRemaining = 4;
        //this.transform.eulerAngles = new Vector3(90, 90, Random.Range(0, 180));
        this.transform.position = new Vector3(this.transform.position.x, -0.7f, this.transform.position.z);
        this.transform.localScale = new Vector3(0.08f, 0.08f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0, 1, 0, Space.Self);

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
                endDouble();
            }
        }
    }


    void OnTriggerEnter(Collider collider)
    {
        Properties.isDouble = true;
        this.gameObject.SetActive(false);

    }

    public void endDouble()
    {
        Properties.isDouble = false;
        timeIsRunning = false;
    }

}
