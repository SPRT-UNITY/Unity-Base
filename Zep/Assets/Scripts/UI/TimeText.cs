using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Runtime.Serialization;

public class TimeText : MonoBehaviour
{
    [SerializeField]
    TMP_Text timeText;

    private void Awake()
    {
        StartCoroutine("countTime");
    }
    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator countTime() 
    {
        while (true) 
        {
            refreshTimeText();
            yield return new WaitForSecondsRealtime(60.0f);
        }
    }

    void refreshTimeText() 
    {
        timeText.SetText(DateTime.Now.ToString("hh:mm"));
    }
}
