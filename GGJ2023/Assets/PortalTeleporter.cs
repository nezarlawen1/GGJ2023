using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour
{
    public Transform player;
    public Transform enterPoint;
    public Transform exitPoint;
    

    // Update is called once per frame
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("PLAYER TELEPORTED");
            player.transform.position = exitPoint.transform.position;


        }
    }


}
