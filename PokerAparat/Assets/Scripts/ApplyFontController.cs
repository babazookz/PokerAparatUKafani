using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplyFontController : MonoBehaviour
{
    private void Start()
    {
        Text[] txtComponents = FindObjectsOfType<Text>();
        foreach (Text txt in txtComponents)
        {
            txt.font = UIManager.Instance.AppFont;
        }
    }
}