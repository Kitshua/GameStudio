//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue
{
    [HideInInspector]
    public bool active;
    [HideInInspector]
    public GameObject go;
    [HideInInspector]
    public Text txt;
    [HideInInspector]
    public Vector3 motion;
    [HideInInspector]
    public float dur; //duration
    [HideInInspector]
    public float lastShown;

    public void Show()
    {
        active = true;
        lastShown = Time.time;
        go.SetActive(active);
    }

    public void Hide()
    {
        active = false;
        go.SetActive(active);
    }

    public void UpdateDialogue()
    {
        if(!active)
        {
            return;
        }
        
        if(Time.time - lastShown > dur)
        {
            Hide();
        }

        go.transform.position += motion * Time.deltaTime;
    }
}
