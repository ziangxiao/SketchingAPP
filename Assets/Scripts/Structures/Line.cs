using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line {

    Vector3 startPoint;
    Vector3 endPoint;

    public Line(Vector3 start, Vector3 end) {
        startPoint = start;
        endPoint = end;
    }

    public Vector3 getStartPoint() {
        return startPoint;
    }

    public Vector3 getEndPoint() {
        return endPoint;
    }
}
