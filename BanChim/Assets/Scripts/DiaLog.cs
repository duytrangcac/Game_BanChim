using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DiaLog : MonoBehaviour
{
    public Text titleText;
    public Text contenText;

    public void Show(bool isShow)
    {
        gameObject.SetActive(isShow);
    }
    public void UpdateDiaLog(string title, string content)
    {
        if (titleText)
        {
            titleText.text = title;
        }
        if (contenText)
        {
            contenText.text = content;
        }
    }
}
