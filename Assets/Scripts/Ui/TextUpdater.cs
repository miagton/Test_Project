using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUpdater : MonoBehaviour
{
    GameObject handler;
    Text text;


    private void OnEnable()
    {
        handler = FindObjectOfType<GameHandler>().gameObject;
        text = GetComponent<Text>();
    }
    void Start()// maybe an overkill, but used to ensure text is set before first frame
    {
        SetText();
    }

   
    void Update()
    {
        SetText();
    }
    void SetText()
    {
        int lvl = handler.GetComponent<GameHandler>().GetLVL();
        text.text = "LVL : " + lvl;
    }
}
