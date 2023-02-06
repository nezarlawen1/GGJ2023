using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public enum SpawnArea
{
    FirstQuarter,
    SecondQuarter,
    ThirdQuarter,
    FourthQuarter,
}

public class MazeGenerator : MonoBehaviour
{
    [Header("Base Generation")]
    [SerializeField] private MazeNode _mazeNodePrefab;
    [SerializeField] private Transform _nodesHolder, InteractablesHolder;
    [SerializeField] private List<MazeNode> _createdMazeNodes;
    [SerializeField] private MazeNode _roofNode;
    [SerializeField] private Vector2Int _mazeSize = new Vector2Int(5, 5);
    [SerializeField] private Vector2Int _startCord;
    [SerializeField] private Vector2Int _endCord;
    [SerializeField] private SpawnArea _startSpawnArea;
    [SerializeField] private SpawnArea _endSpawnArea;
    [SerializeField] private bool _randStart;
    [SerializeField] private bool _randEnd;
    [SerializeField] private bool _showDrawing;
    [SerializeField] private bool _showQuarters;
    [SerializeField] private bool _withRoof = true;
    [SerializeField] private bool _createMaze = true;
    private bool _creatingMaze;
    private bool _finishedMaze;

    [Header("Additional Generation")]
    [SerializeField] private CoreType _coreType;
    [SerializeField] private GameObject _portalPrefab;
    [SerializeField] private GameObject _corePrefab;
    [SerializeField] public GameObject _portalRef, _coreRef;
    [SerializeField] private bool _darkVision;
    [SerializeField] private bool _changedVision;
    private BoxCollider _mazeCollider;

    public bool IsPlayerInMaze;
    public Light PlayerSpotLight;
    public Material _skyboxMaterial;

    private void Awake()
    {
        _mazeCollider = GetComponent<BoxCollider>();
        _skyboxMaterial = RenderSettings.skybox;
    }

    private void Start()
    {
        DrawMaze();
    }

    private void Update()
    {
        DrawMaze();
        if (_changedVision)
        {
            SetNodeColors();
        }
    }

    private void DrawMaze()
    {
        if (_showDrawing && _createMaze && !_creatingMaze)
        {
            _createMaze = false;
            _creatingMaze = true;
            for (int i = 0; i < _nodesHolder.childCount; i++)
            {
                Destroy(_nodesHolder.GetChild(i).gameObject);
            }
            for (int i = 0; i < InteractablesHolder.childCount; i++)
            {
                Destroy(InteractablesHolder.GetChild(i).gameObject);
            }
            _createdMazeNodes.Clear();
            StartCoroutine(GenerateMaze(_mazeSize));
        }
        else if (!_showDrawing && _createMaze && !_creatingMaze)
        {
            _createMaze = false;
            _creatingMaze = true;
            for (int i = 0; i < _nodesHolder.childCount; i++)
            {
                Destroy(_nodesHolder.GetChild(i).gameObject);
            }
            for (int i = 0; i < InteractablesHolder.childCount; i++)
            {
                Destroy(InteractablesHolder.GetChild(i).gameObject);
            }
            _createdMazeNodes.Clear();
            GenerateMazeInstantaneously(_mazeSize);
            GenerateAdditions();
        }

        _mazeCollider.center = new Vector3(_mazeSize.x * 2, _mazeNodePrefab.transform.localScale.y / 2, _mazeSize.y * 2);
        _mazeCollider.size = new Vector3(_mazeSize.x * _mazeSize.x, _mazeNodePrefab.transform.localScale.y, _mazeSize.y * _mazeSize.y);
    }

    public void SwitchVision(bool isDark)
    {
        if (!_finishedMaze)
        {
            _darkVision = isDark;
            _changedVision = true;
            if (PlayerSpotLight != null)
            {
                PlayerSpotLight.enabled = isDark;
            }
            if (isDark)
            {
                RenderSettings.skybox = null;
            }
            else
            {
                RenderSettings.skybox = _skyboxMaterial;
            }
        }
    }

    public void SetNodeColors()
    {
        if (!_finishedMaze)
        {
            foreach (var node in _createdMazeNodes)
            {
                node.SetMaterial(_darkVision);
            }
            if (_roofNode != null)
            {
                _roofNode.SetMaterial(_darkVision);
            }
            _changedVision = false;
            if (_coreRef.GetComponent<Core>().Purified)
            {
                _finishedMaze = true;
            }
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
                Vector3 nodePos = new Vector3(x * _mazeNodePrefab.transform.localScale.x + transform.position.x /* - (size.x / 2)*/, transform.position.y, y * _mazeNodePrefab.transform.localScale.y + transform.position.z /* - (size.x / 2)*/);
                MazeNode newNode = Instantiate(_mazeNodePrefab, nodePos, Quaternion.identity, _nodesHolder);
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
            int nodeIndex = Random.Range(0, nodes.Count);
            MazeNode tempStartNode = nodes[nodeIndex];
            currentPath.Add(tempStartNode);
            _startCord.Set(tempStartNode.PosCords.x, tempStartNode.PosCords.y);

            if (nodeIndex >= 0 && nodeIndex < nodes.Count / 4)
            {
                _startSpawnArea = SpawnArea.FirstQuarter;
            }
            else if (nodeIndex >= nodes.Count / 4 && nodeIndex < nodes.Count / 4 * 2)
            {
                _startSpawnArea = SpawnArea.SecondQuarter;
            }
            else if (nodeIndex >= nodes.Count / 4 * 2 && nodeIndex < nodes.Count / 4 * 3)
            {
                _startSpawnArea = SpawnArea.ThirdQuarter;
            }
            else if (nodeIndex >= nodes.Count / 4 * 3 && nodeIndex <= nodes.Count)
            {
                _startSpawnArea = SpawnArea.FourthQuarter;
            }
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
                Vector3 nodePos = new Vector3(x * _mazeNodePrefab.transform.localScale.x /* - (size.x / 2)*/, transform.position.y, y * _mazeNodePrefab.transform.localScale.y /* - (size.x / 2)*/);
                MazeNode newNode = Instantiate(_mazeNodePrefab, nodePos, Quaternion.identity, _nodesHolder);
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
            int nodeIndex = Random.Range(0, nodes.Count);
            currentPath.Add(nodes[nodeIndex]);

            if (nodeIndex >= 0 && nodeIndex < nodes.Count / 4)
            {
                _startSpawnArea = SpawnArea.FirstQuarter;
            }
            else if (nodeIndex >= nodes.Count / 4 && nodeIndex < nodes.Count / 4 * 2)
            {
                _startSpawnArea = SpawnArea.SecondQuarter;
            }
            else if (nodeIndex >= nodes.Count / 4 * 2 && nodeIndex < nodes.Count / 4 * 3)
            {
                _startSpawnArea = SpawnArea.ThirdQuarter;
            }
            else if (nodeIndex >= nodes.Count / 4 * 3 && nodeIndex <= nodes.Count)
            {
                _startSpawnArea = SpawnArea.FourthQuarter;
            }
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
                int nodeIndex = Random.Range(0, _createdMazeNodes.Count);
                while (CheckArea(nodeIndex) == _startSpawnArea || CheckArea(nodeIndex) == _startSpawnArea + 1 || CheckArea(nodeIndex) == _startSpawnArea - 1)
                {
                    nodeIndex = Random.Range(0, _createdMazeNodes.Count);
                }
                _endSpawnArea = CheckArea(nodeIndex);
                MazeNode tempStartNode = _createdMazeNodes[nodeIndex];
                _endCord.Set(tempStartNode.PosCords.x, tempStartNode.PosCords.y);

            }

            for (int i = 0; i < _createdMazeNodes.Count; i++)
            {
                if (_createdMazeNodes[i].PosCords.x == _startCord.x && _createdMazeNodes[i].PosCords.y == _startCord.y)
                {
                    GameObject entrance = Instantiate(_portalPrefab, _createdMazeNodes[i].transform.position + Vector3.up, Quaternion.identity, InteractablesHolder);
                    _portalRef = entrance;
                }

                if (_createdMazeNodes[i].PosCords.x == _endCord.x && _createdMazeNodes[i].PosCords.y == _endCord.y)
                {
                    GameObject endpoint = Instantiate(_corePrefab, _createdMazeNodes[i].transform.position + Vector3.up, Quaternion.identity, InteractablesHolder);
                    _coreRef = endpoint;
                    int typeChoice = Random.Range(0, Enum.GetNames(typeof(CoreType)).Length);
                    _coreType = (CoreType)typeChoice;
                    _coreRef.GetComponent<Core>().SetCoreType(_coreType);
                }
            }
        }

        if (_withRoof)
        {
            Vector3 tempPos = new Vector3(_mazeSize.x * 2 + transform.position.x, _mazeSize.x + transform.position.y, _mazeSize.y * 2 + transform.position.z);
            _roofNode = Instantiate(_mazeNodePrefab, tempPos, Quaternion.identity, _nodesHolder);
            _roofNode.transform.localScale = new Vector3(_mazeSize.x * _mazeSize.x, _mazeNodePrefab.transform.localScale.y, _mazeSize.y * _mazeSize.y);
        }

        if (_showQuarters)
        {
            for (int i = 0; i < _createdMazeNodes.Count; i++)
            {
                _createdMazeNodes[i].SetQuarter(CheckArea(i));
            }
        }
    }

    private SpawnArea CheckArea(int nodeIndex)
    {
        if (nodeIndex >= 0 && nodeIndex < _createdMazeNodes.Count / 4)
        {
            return SpawnArea.FirstQuarter;
        }
        else if (nodeIndex >= _createdMazeNodes.Count / 4 && nodeIndex < _createdMazeNodes.Count / 4 * 2)
        {
            return SpawnArea.SecondQuarter;
        }
        else if (nodeIndex >= _createdMazeNodes.Count / 4 * 2 && nodeIndex < _createdMazeNodes.Count / 4 * 3)
        {
            return SpawnArea.ThirdQuarter;
        }
        else if (nodeIndex >= _createdMazeNodes.Count / 4 * 3 && nodeIndex <= _createdMazeNodes.Count)
        {
            return SpawnArea.FourthQuarter;
        }
        return SpawnArea.FourthQuarter;
    }

    public void RerollType()
    {
        int typeChoice = Random.Range(0, Enum.GetNames(typeof(CoreType)).Length);
        _coreType = (CoreType)typeChoice;
        _coreRef.GetComponent<Core>().SetCoreType(_coreType);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsPlayerInMaze = true;
            //PlayerSpotLight = other.GetComponentInChildren<Light>();
            SwitchVision(true);
        }
        else if (other.CompareTag("Scout"))
        {
            IsPlayerInMaze = false;
            SwitchVision(false);
        }
    }
    /*private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //IsPlayerInMaze = false;
            SwitchVision(false);
        }
    }*/
}
