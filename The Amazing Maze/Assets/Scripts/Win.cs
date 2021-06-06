using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    public GameObject winPanel;

    // Start is called before the first frame update
    void Start()
    {
        winPanel = GameObject.Find("Canvas").transform.Find("winPanel").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        winPanel.SetActive(true);
        FirstPersonController fpc = GameObject.Find("FirstPersonController").GetComponent<FirstPersonController>();
        fpc.cameraCanMove = false;
        fpc.playerCanMove = false;
        Cursor.lockState = CursorLockMode.None;

        Debug.Log("Win");
    }
}
