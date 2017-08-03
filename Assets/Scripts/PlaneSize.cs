using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSize : MonoBehaviour {

    /// <summary>
    /// The topright point of the screen in world space.
    /// </summary>
    [HideInInspector]
    public Vector3 topRight;

    // The origin plane size is 10x10 units
    float originPlaneWidth = 5.0f;
    float originPlaneHeight = 5.0f;

	void Start () {
        Vector3 screenSize = new Vector3(Screen.width, Screen.height, transform.position.z - Camera.main.transform.position.z);
        Vector3 planeSize = Camera.main.ScreenToWorldPoint(screenSize);
        topRight = new Vector3(planeSize.x, planeSize.y, transform.position.z);
        transform.localScale = new Vector3(topRight.x / originPlaneWidth, 1.0f, topRight.y / originPlaneHeight);
    }

    void Update() {

    }
}
