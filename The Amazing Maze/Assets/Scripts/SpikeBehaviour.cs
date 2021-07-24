using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBehaviour : MonoBehaviour
{
    public GameObject losePanel;
    bool goDown;

    // Start is called before the first frame update
    void Start()
    {
        goDown = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y >= -4f)
            goDown = true;

        if (this.transform.position.y <= -6f)
            goDown = false;

        if (goDown)
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.018f, this.transform.position.z);

        else
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.018f, this.transform.position.z);
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (!Properties.isInvincible)
        {
            losePanel = GameObject.Find("Canvas").transform.Find("losePanel").gameObject;
            losePanel.SetActive(true);
            FirstPersonController fpc = GameObject.Find("FirstPersonController").GetComponent<FirstPersonController>();
            fpc.cameraCanMove = false;
            fpc.playerCanMove = false;
            Cursor.lockState = CursorLockMode.None;
        }

    }

}
