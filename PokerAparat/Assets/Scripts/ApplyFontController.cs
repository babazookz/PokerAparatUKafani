using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplyFontController : MonoBehaviour
{
    private void Start()
    {
        Text[] txtComponents = FindObjectsOfType<Text>();
        Debug.Log("Found text components: " + txtComponents.Length);
        foreach (Text txt in txtComponents)
        {
            txt.font = UIManager.Instance.AppFont;
        }
    }
}