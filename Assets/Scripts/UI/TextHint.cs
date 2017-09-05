using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextHint : MonoBehaviour {

    public GameObject ButtonInfo;
    public GameObject TextPanel;

    public void MouseEnter()
    {
        ButtonInfo.SetActive(true);
        TextPanel.SetActive(true);
    }

    public void MouseExit()
    {
        ButtonInfo.SetActive(false);
        TextPanel.SetActive(false);
    }

}
