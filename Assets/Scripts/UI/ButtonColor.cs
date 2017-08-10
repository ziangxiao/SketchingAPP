using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonColor : MonoBehaviour {

    public Button OppositeButton;

    private Color initColor;
    private Color pressedColor;

	// Use this for initialization
	void Awake () {
        initColor = GetComponent<Button>().colors.normalColor;
        pressedColor = GetComponent<Button>().colors.pressedColor;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Pressed()
    {
        ColorBlock cb = GetComponent<Button>().colors;
        cb.normalColor = pressedColor;
        cb.highlightedColor = pressedColor;
        GetComponent<Button>().colors = cb;

        OppositeButton.GetComponent<ButtonColor>().UnPressed();
    }

    void UnPressed()
    {
        ColorBlock cb = GetComponent<Button>().colors;
        cb.normalColor = initColor;
        cb.highlightedColor = initColor;
        GetComponent<Button>().colors = cb;
    }
}
