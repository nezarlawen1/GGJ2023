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
    [SerializeField] private Vector2Int _posCords;

    public Vector2Int PosCords { get => _posCords; }

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

    public void SetPosOnMatrix(int x, int y)
    {
        _posCords.x = x;
        _posCords.y = y;
    }

    public void RemoveWall(int wallToRemove)
    {
        _walls[wallToRemove].SetActive(false);
    }
}