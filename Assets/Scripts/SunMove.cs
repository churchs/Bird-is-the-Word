using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMove : MonoBehaviour
{
    public float movespeed = .075f;
    void FixedUpdate()
    {
        transform.Translate(Vector3.left * .075f * Time.deltaTime);

        if (gameObject.transform.localPosition.x < -30)
        {
            transform.position = new Vector3(20, transform.position.y, 0);
        }
    }
}
