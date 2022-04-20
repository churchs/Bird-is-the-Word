using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required when Using UI elements.
using TMPro;

public class ValueChange : MonoBehaviour
{
    public TMP_InputField mainInputField;

    public void Start()
    {
        //Adds a listener to the main input field and invokes a method when the value changes.
        mainInputField.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    // Invoked when the value of the text field changes.
    public void ValueChangeCheck()
    {
       // Debug.Log("Value Changed");
    }
}

