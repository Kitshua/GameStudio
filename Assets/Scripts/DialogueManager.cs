using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject txtBox;
    public GameObject txtPrefab;       
    private List<Dialogue> lines = new List<Dialogue>();

    private void Update()
    {
        foreach (Dialogue line in lines)
        {
            line.UpdateDialogue();
        }
    }

    //from gameManager
    public void Show(string msg, int size, Color c, Vector3 pos, Vector3 motion, float dur)
    {
        Dialogue dialogue = GetDialogue();

        dialogue.txt.text = msg;
        dialogue.txt.fontSize = size;
        dialogue.txt.color = c;

        dialogue.go.transform.position = Camera.main.WorldToScreenPoint(pos);
        dialogue.motion = motion;
        dialogue.dur = dur;

        dialogue.Show();
    }

    private Dialogue GetDialogue()
    {
        Dialogue txt = lines.Find(t => !t.active);

        if (txt == null)
        {
            txt = new Dialogue();
            txt.go = Instantiate(txtPrefab);
            txt.go.transform.SetParent(txtBox.transform);
            txt.txt = txt.go.GetComponent<Text>();

            lines.Add(txt);
        }

        return txt;
    }
}
