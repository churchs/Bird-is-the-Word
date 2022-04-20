using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSound : MonoBehaviour
{
    [SerializeField] Sprite[] icon;
    [SerializeField] bool soundON = true;
    public void SwitchIcon()
    {
        if (soundON)
        {
            gameObject.GetComponent<Image>().sprite = icon[1];
            soundON = false;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = icon[0];
            soundON = true;
        }
    }
}
