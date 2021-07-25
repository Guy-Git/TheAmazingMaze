using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleBehaviour : MonoBehaviour
{
    Time time;
    static bool timeIsRunning = false;
    static float timeRemaining = 20;
    bool goDown;

    // Use this for initialization
    void Start()
    {
        goDown = true;

        this.transform.eulerAngles = new Vector3(270, 0, 0);
        this.transform.position = new Vector3(this.transform.position.x, -1.2f, this.transform.position.z);
        this.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);


    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0, 0, 1, Space.Self);

        if (this.transform.position.y >= -1.2f)
            goDown = true;

        if (this.transform.position.y <= -1.8f)
            goDown = false;

        if (goDown)
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.005f, this.transform.position.z);

        else
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.005f, this.transform.position.z);

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
                Properties.isInvincible = false;
            }
        }

    }
    void OnTriggerEnter(Collider collider)
    {
        timeRemaining = 20;
        timeIsRunning = true;
        Properties.isInvincible = true;
        this.gameObject.SetActive(false);
    }
}