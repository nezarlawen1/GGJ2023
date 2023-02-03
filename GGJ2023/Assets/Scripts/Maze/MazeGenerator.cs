using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MazeGenerator : MonoBehaviour
{
    [Header("Base Generation")]
    [SerializeField] private MazeNode _mazeNodePrefab;
    [SerializeField] private List<MazeNode> _createdMazeNodes;
    [SerializeField] private Vector2Int _mazeSize = new Vector2Int(5, 5);
    [SerializeField] private Vector2Int _startCord;
    [SerializeField] private Vector2Int _endCord;
    [SerializeField] private bool _randStart;
    [SerializeField] private bool _randEnd;
    [SerializeField] private bool _showDrawing;
    [SerializeField] private bool _createMaze = true;
    private bool _creatingMaze;

    [Header("Additional Generation")]
    [SerializeField] private GameObject _portalPrefab;
    [SerializeField] private GameObject _corePrefab;


    private void Start()
    {
        DrawMaze();
    }

    private void Update()
    {
        DrawMaze();
    }

    private void DrawMaze()
    {
        if (_showDrawing && _createMaze && !_creatingMaze)
        {
            _createMaze = false;
            _creatingMaze = true;
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
            _createdMazeNodes.Clear();
            StartCoroutine(GenerateMaze(_mazeSize));
        }
        else if (!_showDrawing && _createMaze && !_creatingMaze)
        {
            _createMaze = false;
            _creatingMaze = true;
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
            _createdMazeNodes.Clear();
            GenerateMazeInstantaneously(_mazeSize);
            GenerateAdditions();
        }
    }

    private void GenerateMazeInstantaneously(Vector2Int size)
    {
        List<MazeNode> nodes = new List<MazeNode>();

        // Create Nodes
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector3 nodePos = new Vector3(x/* - (size.x / 2)*/, 0, y/* - (size.x / 2)*/);
                MazeNode newNode = Instantiate(_mazeNodePrefab, nodePos, Quaternion.identity, transform);
                newNode.SetPosOnMatrix(x, y);
                nodes.Add(newNode);
                _createdMazeNodes.Add(newNode);
            }
        }

        List<MazeNode> currentPath = new List<MazeNode>();
        List<MazeNode> completedNodes = new List<MazeNode>();

        // Choose Starting Node
        if (_randStart)
        {
            MazeNode tempStartNode = nodes[Random.Range(0, nodes.Count)];
            currentPath.Add(tempStartNode);
            _startCord.Set(tempStartNode.PosCords.x, tempStartNode.PosCords.y);
        }
        else
        {

            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].PosCords.x == _startCord.x && nodes[i].PosCords.y == _startCord.y)
                {
                    currentPath.Add(nodes[i]);
                }
            }
        }

        while (completedNodes.Count < nodes.Count)
        {
            // Check Nodes next to the Current Node
            List<int> possibleNextNode = new List<int>();
            List<int> possibleDirections = new List<int>();

            int currentNodeIndex = nodes.IndexOf(currentPath[currentPath.Count - 1]);
            int currentNodeX = currentNodeIndex / size.y;
            int currentNodeY = currentNodeIndex % size.y;

            // Check Node East of the Current Node
            if (currentNodeX < size.x - 1)
            {
                if (!completedNodes.Contains(nodes[currentNodeIndex + size.y]) && !currentPath.Contains(nodes[currentNodeIndex + size.y]))
                {
                    possibleDirections.Add(0);
                    possibleNextNode.Add(currentNodeIndex + size.y);
                }
            }

            // Check Node West of the Current Node
            if (currentNodeX > 0)
            {
                if (!completedNodes.Contains(nodes[currentNodeIndex - size.y]) && !currentPath.Contains(nodes[currentNodeIndex - size.y]))
                {
                    possibleDirections.Add(1);
                    possibleNextNode.Add(currentNodeIndex - size.y);
                }
            }

            // Check Node North of the Current Node
            if (currentNodeY < size.y - 1)
            {
                if (!completedNodes.Contains(nodes[currentNodeIndex + 1]) && !currentPath.Contains(nodes[currentNodeIndex + 1]))
                {
                    possibleDirections.Add(2);
                    possibleNextNode.Add(currentNodeIndex + 1);
                }
            }

            // Check Node South of the Current Node
            if (currentNodeY > 0)
            {
                if (!completedNodes.Contains(nodes[currentNodeIndex - 1]) && !currentPath.Contains(nodes[currentNodeIndex - 1]))
                {
                    possibleDirections.Add(3);
                    possibleNextNode.Add(currentNodeIndex - 1);
                }
            }

            // Choose Next Node
            if (possibleDirections.Count > 0)
            {
                int chosenDirection = Random.Range(0, possibleDirections.Count);
                MazeNode chosenNode = nodes[possibleNextNode[chosenDirection]];

                switch (possibleDirections[chosenDirection])
                {
                    // East
                    case 0:
                        chosenNode.RemoveWall(1);
                        currentPath[currentPath.Count - 1].RemoveWall(0);
                        break;
                    // West
                    case 1:
                        chosenNode.RemoveWall(0);
                        currentPath[currentPath.Count - 1].RemoveWall(1);
                        break;
                    // North
                    case 2:
                        chosenNode.RemoveWall(3);
                        currentPath[currentPath.Count - 1].RemoveWall(2);
                        break;
                    // South
                    case 3:
                        chosenNode.RemoveWall(2);
                        currentPath[currentPath.Count - 1].RemoveWall(3);
                        break;
                    default:
                        break;
                }

                currentPath.Add(chosenNode);
            }
            else
            {
                completedNodes.Add(currentPath[currentPath.Count - 1]);

                currentPath.RemoveAt(currentPath.Count - 1);
            }
        }

        _creatingMaze = false;
    }

    IEnumerator GenerateMaze(Vector2Int size)
    {
        List<MazeNode> nodes = new List<MazeNode>();

        // Create Nodes
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector3 nodePos = new Vector3(x/* - (size.x / 2)*/, 0, y/* - (size.x / 2)*/);
                MazeNode newNode = Instantiate(_mazeNodePrefab, nodePos, Quaternion.identity, transform);
                newNode.SetPosOnMatrix(x, y);
                nodes.Add(newNode);
                _createdMazeNodes.Add(newNode);

                yield return null;
            }
        }

        List<MazeNode> currentPath = new List<MazeNode>();
        List<MazeNode> completedNodes = new List<MazeNode>();

        // Choose Starting Node
        if (_randStart)
        {
            currentPath.Add(nodes[Random.Range(0, nodes.Count)]);
        }
        else
        {

            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].PosCords.x == _startCord.x && nodes[i].PosCords.y == _startCord.y)
                {
                    currentPath.Add(nodes[i]);
                }
            }
        }
        currentPath[0].SetState(NodeState.Current);

        while (completedNodes.Count < nodes.Count)
        {
            // Check Nodes next to the Current Node
            List<int> possibleNextNode = new List<int>();
            List<int> possibleDirections = new List<int>();

            int currentNodeIndex = nodes.IndexOf(currentPath[currentPath.Count - 1]);
            int currentNodeX = currentNodeIndex / size.y;
            int currentNodeY = currentNodeIndex % size.y;

            // Check Node East of the Current Node
            if (currentNodeX < size.x - 1)
            {
                if (!completedNodes.Contains(nodes[currentNodeIndex + size.y]) && !currentPath.Contains(nodes[currentNodeIndex + size.y]))
                {
                    possibleDirections.Add(0);
                    possibleNextNode.Add(currentNodeIndex + size.y);
                }
            }

            // Check Node West of the Current Node
            if (currentNodeX > 0)
            {
                if (!completedNodes.Contains(nodes[currentNodeIndex - size.y]) && !currentPath.Contains(nodes[currentNodeIndex - size.y]))
                {
                    possibleDirections.Add(1);
                    possibleNextNode.Add(currentNodeIndex - size.y);
                }
            }

            // Check Node North of the Current Node
            if (currentNodeY < size.y - 1)
            {
                if (!completedNodes.Contains(nodes[currentNodeIndex + 1]) && !currentPath.Contains(nodes[currentNodeIndex + 1]))
                {
                    possibleDirections.Add(2);
                    possibleNextNode.Add(currentNodeIndex + 1);
                }
            }

            // Check Node South of the Current Node
            if (currentNodeY > 0)
            {
                if (!completedNodes.Contains(nodes[currentNodeIndex - 1]) && !currentPath.Contains(nodes[currentNodeIndex - 1]))
                {
                    possibleDirections.Add(3);
                    possibleNextNode.Add(currentNodeIndex - 1);
                }
            }

            // Choose Next Node
            if (possibleDirections.Count > 0)
            {
                int chosenDirection = Random.Range(0, possibleDirections.Count);
                MazeNode chosenNode = nodes[possibleNextNode[chosenDirection]];

                switch (possibleDirections[chosenDirection])
                {
                    // East
                    case 0:
                        chosenNode.RemoveWall(1);
                        currentPath[currentPath.Count - 1].RemoveWall(0);
                        break;
                    // West
                    case 1:
                        chosenNode.RemoveWall(0);
                        currentPath[currentPath.Count - 1].RemoveWall(1);
                        break;
                    // North
                    case 2:
                        chosenNode.RemoveWall(3);
                        currentPath[currentPath.Count - 1].RemoveWall(2);
                        break;
                    // South
                    case 3:
                        chosenNode.RemoveWall(2);
                        currentPath[currentPath.Count - 1].RemoveWall(3);
                        break;
                    default:
                        break;
                }

                currentPath.Add(chosenNode);
                chosenNode.SetState(NodeState.Current);
            }
            else
            {
                completedNodes.Add(currentPath[currentPath.Count - 1]);

                currentPath[currentPath.Count - 1].SetState(NodeState.Completed);
                currentPath.RemoveAt(currentPath.Count - 1);
            }

            //yield return null;
            yield return new WaitForSeconds(0.05f);
        }

        GenerateAdditions();
        _creatingMaze = false;
    }

    private void GenerateAdditions()
    {
        if (_portalPrefab && _corePrefab)
        {
            if (_randEnd)
            {
                MazeNode tempStartNode = _createdMazeNodes[Random.Range(0, _createdMazeNodes.Count)];
                _endCord.Set(tempStartNode.PosCords.x, tempStartNode.PosCords.y);
            }

            for (int i = 0; i < _createdMazeNodes.Count; i++)
            {
                if (_createdMazeNodes[i].PosCords.x == _startCord.x && _createdMazeNodes[i].PosCords.y == _startCord.y)
                {
                    GameObject entrance = Instantiate(_portalPrefab, _createdMazeNodes[i].transform.position, Quaternion.identity, _createdMazeNodes[i].transform);
                }

                if (_createdMazeNodes[i].PosCords.x == _endCord.x && _createdMazeNodes[i].PosCords.y == _endCord.y)
                {
                    GameObject endpoint = Instantiate(_corePrefab, _createdMazeNodes[i].transform.position, Quaternion.identity, _createdMazeNodes[i].transform);
                }
            }
        }

    }
}
