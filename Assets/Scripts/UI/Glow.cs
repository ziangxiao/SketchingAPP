using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glow : MonoBehaviour {

    void OnMouseEnter()
    {
        transform.GetComponent<Renderer>().material.color = new Color(1f, 1f, 0f);
    }

    /*void OnMouseOver()
    {
        transform.GetComponent<Renderer>().material.color -= new Color(0.1f, 0f, 0f) * Time.deltaTime;
    }*/

    void OnMouseExit()
    {
        transform.GetComponent<Renderer>().material.color = Color.black;
    }

    /// <summary>
    /// Get the position of current Cube.
    /// </summary>
    /// <returns>transform.position</returns>
    public Vector3 getPosition()
    {
        return transform.position;
    }
}
