using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose : MonoBehaviour
{
    public GameObject losePanel;

    // Start is called before the first frame update
    void Start()
    {
        losePanel = GameObject.Find("Canvas").transform.Find("losePanel").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnCollisionEnter(Collision collision)
    {
        losePanel.SetActive(true);
        FirstPersonController fpc = GameObject.Find("FirstPersonController").GetComponent<FirstPersonController>();
        fpc.cameraCanMove = false;
        fpc.playerCanMove = false;
        Cursor.lockState = CursorLockMode.None;

        Debug.Log("LOOSER!");
    }
}
