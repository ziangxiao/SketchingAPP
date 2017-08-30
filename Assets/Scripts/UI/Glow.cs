using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glow : MonoBehaviour {

    void OnMouseEnter()
    {
        transform.GetComponent<Renderer>().material.color = new Color(249.0f/255.0f,116.0f/255.0f, 7.0f/255.0f);
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
