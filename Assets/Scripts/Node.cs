using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {

    Vector3 pivotPoint;
    float nodeDiameter;

    public Node (Vector3 pivot, float diameter) {
        pivotPoint = pivot;
        nodeDiameter = diameter;
    }

    public Vector3 GetPivot()
    {
        return pivotPoint;
    }
}
