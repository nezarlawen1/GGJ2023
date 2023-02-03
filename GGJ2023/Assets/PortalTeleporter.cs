using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour
{
    //public Transform enterPoint;
    public Transform exitPoint;
    public MazeGenerator mazeGenerator;
    public bool open = false;

    private void Update()
    {
        if (exitPoint == null)
        {
            GameManager.Instance.CurrentMaze = mazeGenerator;
            exitPoint = mazeGenerator._portalRef.transform;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Scout")
        {
            Debug.Log("Scout TELEPORTED");
            other.transform.position = exitPoint.transform.position;
        }
        if (other.tag == "Player")
        {
            ControllersSwapManager.Instance.CurrentPortalPos = gameObject.transform;
            ControllersSwapManager.Instance.PlayerInPortalCollider = true;
            if (ControllersSwapManager.Instance.PlayerCanTeleport)
            {
                Debug.Log("Player TELEPORTED");
                other.transform.position = exitPoint.transform.position;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            ControllersSwapManager.Instance.PlayerInPortalCollider = false;
        }
    }

}
