using UnityEngine;
using System.Collections;

public class WreckWallBehaviour : MonoBehaviour
{
    RaycastHit hit;
    private void Start()
    {
        hit = new RaycastHit();
    }
    // Update is called once per frame
    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Q) && Properties.points >= 50)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, 50))
            {
                if (hit.transform.tag.Equals("wall"))
                {
                    Properties.points -= 50;
                    Destroy(hit.transform.gameObject);
                }
            }
        }

    }
}
