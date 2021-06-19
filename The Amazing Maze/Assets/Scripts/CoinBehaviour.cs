using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoinBehaviour : MonoBehaviour
{
    bool goDown;
    // Use this for initialization
    void Start()
    {
        this.transform.eulerAngles = new Vector3(90, 90, 0);

        this.transform.position = new Vector3(this.transform.position.x, 0.1f, this.transform.position.z);
        this.transform.localScale = new Vector3(0.18f, 0.4f, 0.18f);

        goDown = true;

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0, 0, 1, Space.Self);
        if(this.transform.position.y >= 0.1f)
            goDown = true;

        if (this.transform.position.y <= -1)
            goDown = false;

        if(goDown)
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.007f, this.transform.position.z);

        else
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.007f, this.transform.position.z);

    }


    void OnTriggerEnter(Collider collider)
    {
        Properties.points += 10;
        this.gameObject.SetActive(false);
    }

}
