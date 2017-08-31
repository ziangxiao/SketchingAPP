using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextHint : MonoBehaviour {

    public GameObject ButtonInfo;

    public void MouseEnter()
    {
        ButtonInfo.SetActive(true);
    }

    public void MouseExit()
    {
        ButtonInfo.SetActive(false);
    }

}
