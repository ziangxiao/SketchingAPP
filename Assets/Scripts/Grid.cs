using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    /// <summary>
    /// The size of the whole grid, the smaller the size, the denser the grid
    /// </summary>
    public float gridSize;

    /// <summary>
    /// The actual size of one node, nodeSize is smaller than gridSize
    /// </summary>
    [HideInInspector]
    public float nodeSize = 0.8f;

    public GameObject CubeContainer;

    float gridDiameter;
    Vector3 topRight;
    Node[,] grid;
    int gridSizeX, gridSizeY;

    float defaultCubeWidth = 1.0f;

	// Use this for initialization
	void Start () {
        topRight = GetComponent<PlaneSize>().topRight;

        gridSizeX = Mathf.RoundToInt(topRight.x * 2 / gridSize);
        gridSizeY = Mathf.RoundToInt(topRight.y * 2 / gridSize);
        gridDiameter = gridSize / 2;
        //CreateSquareGrid();
        CreateIsometricGrid();
        DrawGrid();

        // Adjust the camera to move forward.
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x,
                                            Camera.main.transform.position.y,
                                            Camera.main.transform.position.z * 2 / 3);
	}

    /// <summary>
    /// Generate a square grid.
    /// </summary>
    void CreateSquareGrid() {
        grid = new Node[gridSizeX, gridSizeY];
        for(int i = 0; i < gridSizeX; i++)
        {
            for(int j = 0; j < gridSizeY; j++)
            {
                Vector3 currentPivot = new Vector3((2 * i + 1) * gridDiameter - topRight.x, (2 * j + 1) * gridDiameter - topRight.y, topRight.z);
                grid[i, j] = new Node(currentPivot, gridDiameter);
            }
        }
    }

    /// <summary>
    /// Generate an isometric grid.
    /// </summary>
    void CreateIsometricGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        for (int i = 0; i < gridSizeX; i++)
        {
            if(i % 2 == 0)
            {
                for (int j = 0; j < gridSizeY; j++)
                {
                    Vector3 currentPivot = new Vector3((2 * i + 1) * gridDiameter - topRight.x, (2 * j + 1) * gridDiameter - topRight.y, topRight.z);
                    grid[i, j] = new Node(currentPivot, gridDiameter);
                }
            }
            else
            {
                for (int j = 0; j < gridSizeY; j++)
                {
                    Vector3 currentPivot = new Vector3((2 * i + 1) * gridDiameter - topRight.x, (2 * j) * gridDiameter - topRight.y, topRight.z);
                    grid[i, j] = new Node(currentPivot, gridDiameter);
                }
            }
        }
    }
	
	void Update () {
		
	}

    /// <summary>
    /// Draw the grid.
    /// </summary>
    void DrawGrid()
    {
        foreach (Node node in grid)
        {
            var point = GameObject.CreatePrimitive(PrimitiveType.Cube);
            point.transform.parent = CubeContainer.transform;
            point.tag = "Cube";
            point.transform.position = node.GetPivot();
            point.transform.localScale = new Vector3(nodeSize, nodeSize, nodeSize);
            point.GetComponent<Renderer>().material.color = Color.black;
            point.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            point.GetComponent<BoxCollider>().size = new Vector3(gridSize / (defaultCubeWidth * nodeSize), gridSize / (defaultCubeWidth * nodeSize), gridSize / (defaultCubeWidth * nodeSize));

            point.AddComponent<Glow>();
        }
    }

    public Vector2 Point2Grid(Vector3 point) {
        int i = (int)(((point.x + topRight.x) / gridDiameter - 1) / 2);
        int j = (int)(((point.y + topRight.y) / gridDiameter - 1) / 2);
        return new Vector2(i, j);
    }

    public Vector3 Grid2Point(Vector2 coordinate) {
        return new Vector3((2 * coordinate.x + 1) * gridDiameter - topRight.x, (2 * coordinate.y + 1) * gridDiameter - topRight.y, topRight.z);
    }

    public void ChangeToSquare()
    {
        CreateSquareGrid();
        DrawGrid();
    }

    public void ChangeToIsometric()
    {
        CreateIsometricGrid();
        DrawGrid();
    }
}
