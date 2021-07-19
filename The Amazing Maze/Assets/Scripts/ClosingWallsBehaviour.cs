using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosingWallsBehaviour : MonoBehaviour
{
    bool close;
    // Start is called before the first frame update
    void Start()
    {        
        close = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.localScale.z >= 5.5f)
            close = true;

        if (this.transform.localScale.z <= 0.6f)
            close = false;

        if (close)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z - 0.02f);
            this.transform.position = new Vector3(this.transform.position.x - 0.01f, this.transform.position.y, this.transform.position.z);
        }

        else
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z + 0.02f);
            this.transform.position = new Vector3(this.transform.position.x + 0.01f, this.transform.position.y, this.transform.position.z);
        }
    }
}
