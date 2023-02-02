using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeState
{
    Available,
    Current,
    Completed
}

public class MazeNode : MonoBehaviour
{
    [SerializeField] private GameObject[] _walls;
    [SerializeField] private MeshRenderer _floormesh;

    public void SetState(NodeState state)
    {
        switch (state)
        {
            case NodeState.Available:
                _floormesh.material.color = Color.white;
                break;
            case NodeState.Current:
                _floormesh.material.color = Color.yellow;
                break;
            case NodeState.Completed:
                _floormesh.material.color = Color.blue;
                break;
            default:
                break;
        }
    }
}
