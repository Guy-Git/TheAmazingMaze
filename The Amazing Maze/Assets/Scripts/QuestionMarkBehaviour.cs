using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionMarkBehaviour : MonoBehaviour
{
    bool goDown;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector3(this.transform.position.x, -1.4f, this.transform.position.z);
        this.transform.localScale = new Vector3(0.05f, 0.05f, 0.02f);
        
        goDown = true;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0, 1, 0, Space.Self);

        if (this.transform.position.y >= -1.4f)
            goDown = true;

        if (this.transform.position.y <= -2)
            goDown = false;

        if (goDown)
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.005f, this.transform.position.z);

        else
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.005f, this.transform.position.z);

    }
    void OnTriggerEnter(Collider collider)
    {
        FirstPersonController fpc = GameObject.Find("FirstPersonController").GetComponent<FirstPersonController>();

        int x = Random.Range(0, Properties.cols);
        int z = Random.Range(0, Properties.rows);

        fpc.isCrouched = true;
        fpc.Crouch();
        fpc.walkSpeed = 5f;

        fpc.gameObject.transform.position = new Vector3(x * 5.5f, fpc.gameObject.transform.position.y, z * 5.5f);

        this.gameObject.SetActive(false);
    }
}
