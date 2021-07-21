using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CollectableBehaviour : MonoBehaviour
{
    bool goDown;
    // Use this for initialization
    void Start()
    {
        if (Properties.chosenMode == 0)
        {
            this.transform.eulerAngles = new Vector3(90, 90, Random.Range(0, 180));
            this.transform.position = new Vector3(this.transform.position.x, -0.7f, this.transform.position.z);
            this.transform.localScale = new Vector3(0.18f, 0.4f, 0.18f);
        }

        else
        {
            this.transform.eulerAngles = new Vector3(0, 0, 0);
            this.transform.position = new Vector3(this.transform.position.x, -0.7f, this.transform.position.z);
            this.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        }

        goDown = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (Properties.chosenMode == 0)
            this.transform.Rotate(0, 0, 1, Space.Self);

        else
            this.transform.Rotate(0, 1, 0, Space.Self);


        if(this.transform.position.y >= -0.7f)
            goDown = true;

        if (this.transform.position.y <= -1.3)
            goDown = false;

        if(goDown)
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.005f, this.transform.position.z);

        else
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.005f, this.transform.position.z);

    }

    void OnTriggerEnter(Collider collider)
    {
        if (Properties.chosenMode == 0)
            if (Properties.isDouble == false)
                Properties.points += 10;
            else
                Properties.points += 20;

        else
            Timer.timeRemaining += 10;

        this.gameObject.SetActive(false);
    }

}
