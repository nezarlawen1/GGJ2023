using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour
{
    //public Transform enterPoint;
    public Transform exitPoint;
    public MazeGenerator mazeGenerator;
    public bool open = false;

    private GameObject player;

    private void Update()
    {
        if (exitPoint == null)
        {
            GameManager.Instance.CurrentMaze = mazeGenerator;
            exitPoint = mazeGenerator._portalRef.transform;
        }
        if (ControllersSwapManager.Instance.PlayerCanTeleport && player != null)
        {
            Debug.Log("Player TELEPORTED");

            Teleport();
        }
    }

    private void Teleport()
    {
        player.transform.position = exitPoint.transform.position;
        ControllersSwapManager.Instance.PlayerCanTeleport = false;
        player = null;
        
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
            player = other.gameObject;
            mazeGenerator.PlayerSpotLight = other.GetComponentInChildren<Light>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            ControllersSwapManager.Instance.PlayerInPortalCollider = false;
            //ControllersSwapManager.Instance.CurrentPortalPos = null;
            
        }
    }

}
