using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {

    public GameObject plane;
    public GameObject LineContainer;

    public GameObject PromptBackground;
    public GameObject ChangeGridPrompt;
    public GameObject ClearAllPrompt;

    public ButtonColor IsometricButtonColor;
    public ButtonColor SquareButtonColor;

    public Material DashedLineMaterial;
    public Material LineMaterial;

    public float scrollspeed = 5.0f;

    List<LineRenderer> AllLines;

    /// <summary>
    /// To check if this is the first click.
    /// </summary>
    bool firstClick = false;

    LineRenderer currentLine;

    /// <summary>
    /// The quantitiy of vertex of LineRenderer.
    /// </summary>
    int vertexCount = 2;

    /// <summary>
    /// The width of the line.
    /// </summary>
    float lineWidth = 0.1f;

    Vector3 firstPoint;
    Vector3 secondPoint;
    Vector3 firstDrawingPoint;
    Vector3 secondDrawingPoint;

    float offset = 0.01f;
    float zoomingbottom = -10.0f;
    float zoomingtop = -2.0f;

    LogTool logTool;

    bool IsometricGridType = false;
    bool DashedLine = false;

    // Use this for initialization
    void Start() {
        firstPoint = new Vector3(0.0f, 0.0f, 0.0f);
        secondPoint = new Vector3(0.0f, 0.0f, 0.0f);
        AllLines = new List<LineRenderer>();
        logTool = transform.GetComponent<LogTool>();
    }

    // Update is called once per frame
    void Update() {
        //Zoom in and zoom out.
        //Debug.Log(Input.mouseScrollDelta);
        float zoomingOffset = Camera.main.transform.position.z + Input.mouseScrollDelta.y * scrollspeed * Time.deltaTime;
        if (zoomingOffset > zoomingbottom && zoomingOffset < zoomingtop)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x,
                                                Camera.main.transform.position.y,
                                                zoomingOffset);
        }

        Vector3 mousePos = Input.mousePosition;

        // For Perspective camera, z is the distance from the camera.
        mousePos.z = -Camera.main.transform.position.z;

        Vector3 mousePositionOnScreen = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 mousePositionOnPlane = new Vector3(mousePositionOnScreen.x, mousePositionOnScreen.y, plane.transform.position.z - offset);
        if (firstClick) {
            //Debug.Log(mousePositionOnScreen);
            currentLine.SetPosition(vertexCount - 1, mousePositionOnPlane);
        }

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            // Here it goes the first click.
            if (!firstClick)
            {
                RaycastHit hitObject = new RaycastHit();
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hitObject) && hitObject.transform.tag == "Cube")
                {
                    firstPoint = hitObject.transform.GetComponent<Glow>().getPosition();
                }

                LineInit(DashedLine);

                firstDrawingPoint = new Vector3(firstPoint.x, firstPoint.y, firstPoint.z - offset);
                currentLine.SetPosition(vertexCount - 2, firstDrawingPoint);
                firstClick = true;
            }
            // Here it goes the second click.
            else
            {
                RaycastHit hitObject = new RaycastHit();
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hitObject) && hitObject.transform.tag == "Cube")
                {
                    secondPoint = hitObject.transform.GetComponent<Glow>().getPosition();
                }

                secondDrawingPoint = new Vector3(secondPoint.x, secondPoint.y, secondPoint.z - offset);
                currentLine.SetPosition(vertexCount - 1, secondDrawingPoint);

                int existIndex = 0;
                if (secondPoint == firstPoint)
                {
                    Destroy(currentLine.gameObject);

                }
                else if ((existIndex = CheckExist(firstDrawingPoint, secondDrawingPoint)) >= 0)
                {
                    // Add log.
                    logTool.LineDel(firstDrawingPoint, secondDrawingPoint);

                    Destroy(currentLine.gameObject);
                    LineRenderer line = AllLines[existIndex];
                    AllLines.RemoveAt(existIndex);
                    Destroy(line.gameObject);
                }
                else {
                    AllLines.Add(currentLine);

                    // Add Log.
                    logTool.LineAdd(firstDrawingPoint, secondDrawingPoint);
                }
                firstClick = false;
            }
        }
    }

    /// <summary>
    /// Check if this line already exists
    /// </summary>
    /// <param name="start">start point</param>
    /// <param name="end">end point</param>
    /// <returns></returns>
    int CheckExist(Vector3 start, Vector3 end)
    {
        for (int i = 0; i < AllLines.ToArray().Length; i++)
        {
            LineRenderer line = AllLines[i];
            if ((line.GetPosition(0) == start && line.GetPosition(1) == end)
                || (line.GetPosition(0) == end && line.GetPosition(1) == start))
                return i;
        }

        return -1;
    }

    /// <summary>
    /// Initiate line object and the parameters of line renderer.
    /// </summary>
    void LineInit(bool _DashedLine)
    {
        GameObject currentLineObj = new GameObject();
        currentLineObj.transform.parent = LineContainer.transform;
        currentLineObj.name = "Line";
        currentLineObj.tag = "Line";
        currentLine = currentLineObj.AddComponent<LineRenderer>();
        currentLine.textureMode = LineTextureMode.Tile;

        if (_DashedLine)
            currentLine.material = DashedLineMaterial;
        else
            currentLine.material = LineMaterial;

        currentLine.positionCount = vertexCount;
        //currentLine.startColor = Color.red;
        //currentLine.endColor = Color.red;
        currentLine.startWidth = lineWidth;
        currentLine.endWidth = lineWidth;
    }

    /// <summary>
    /// Clear all lines with one button.
    /// </summary>
    public void clearAllLines() {
        AllLines.Clear();
        foreach (Object gb in GameObject.FindGameObjectsWithTag("Line"))
        {
            Destroy(gb);
        }
    }

    public void ClearAll()
    {
        // Add log.
        logTool.ClearAll();

        clearAllLines();
    }

    private void changeGridType()
    {
        // logTool.ResetBody();

        AllLines.Clear();
        foreach (Object gb in GameObject.FindGameObjectsWithTag("Line"))
        {
            Destroy(gb);
        }
        foreach (Object gb in GameObject.FindGameObjectsWithTag("Cube"))
        {
            Destroy(gb);
        }
        /*gridType = !gridType;
        if (gridType)
            plane.GetComponent<Grid>().ChangeToSquare();
        else
            plane.GetComponent<Grid>().ChangeToIsometric();*/

    }

    public void PromptChangeToIsometric()
    {
        if (!IsometricGridType)
        {
            PromptBackground.SetActive(true);
            ChangeGridPrompt.SetActive(true);
            IsometricGridType = true;
        }
    }

    public void PromptChangeToSquare()
    {
        if (IsometricGridType)
        {
            PromptBackground.SetActive(true);
            ChangeGridPrompt.SetActive(true);
            IsometricGridType = false;
        }
    }

    public void ConfirmGridType()
    {
        logTool.ChangeGridType(IsometricGridType);

        if (IsometricGridType)
        {
            changeGridType();
            plane.GetComponent<Grid>().ChangeToIsometric();
            CancelPrompt();

            IsometricButtonColor.Pressed();
        }
        else
        {
            changeGridType();
            plane.GetComponent<Grid>().ChangeToSquare();
            CancelPrompt();

            SquareButtonColor.Pressed();
        }
    }

    public void SetGridTypeVariable(bool _isometric)
    {
        IsometricGridType = _isometric;
    }

    public void CancelChangeGridType()
    {
        CancelPrompt();
        IsometricGridType = !IsometricGridType;
    }

    public void PromptClearAll()
    {
        PromptBackground.SetActive(true);
        ClearAllPrompt.SetActive(true);
    }

    public void ConfirmClearAll()
    {
        CancelPrompt();
        ClearAll();
    }

    public void CancelPrompt()
    {
        PromptBackground.SetActive(false);
        ChangeGridPrompt.SetActive(false);
        ClearAllPrompt.SetActive(false);
    }

    
    /// <summary>
    /// Get all lines that are shown on the screen.
    /// </summary>
    /// <returns></returns>
    public List<LineRenderer> getAllLines() {
        return AllLines;
    }

    /// <summary>
    /// Load and draw all lines when clicking load log.
    /// </summary>
    /// <param name="LinesofLog">All lines stored in List</param>
    public void LoadLinesFromLog(List<Vector3> LinesofLog, bool _DashedLine)
    {
        // Clear all lines on the panel. Now we did it in LogTool.
        /*AllLines.Clear();
        foreach (Object gb in GameObject.FindGameObjectsWithTag("Line"))
        {
            Destroy(gb);
        }*/

        // Draw all lines in the log.
        int size = LinesofLog.ToArray().Length;
        for (int i = 0; i < size; i += 2) {
            LineInit(_DashedLine);
            Vector3 startingpoint = LinesofLog[i];
            Vector3 endingpoint = LinesofLog[i + 1];
            firstDrawingPoint = new Vector3(startingpoint.x, startingpoint.y, startingpoint.z - offset);
            currentLine.SetPosition(vertexCount - 2, firstDrawingPoint);
            secondDrawingPoint = new Vector3(endingpoint.x, endingpoint.y, endingpoint.z - offset);
            currentLine.SetPosition(vertexCount - 1, secondDrawingPoint);

            AllLines.Add(currentLine);
        }
    }

    public void SetDashedLine()
    {
        DashedLine = true;
    }

    public void SetActualLine()
    {
        DashedLine = false;
    }

    public bool GetLineType()
    {
        return DashedLine;
    }

    public bool GetGridType()
    {
        return IsometricGridType;
    }
}
