using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class testScriptCharCount : MonoBehaviour
{
    public int charCount;
    public int noRichCharCount;
    public TextMeshProUGUI textL;
    public GameController gameController;

    void Update()
    {
        charCount = textL.text.Length;
      //  noRichCharCount = gameController.TextLength(textL.ToString());
    }
}
