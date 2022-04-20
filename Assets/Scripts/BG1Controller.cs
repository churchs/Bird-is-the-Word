using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG1Controller : MonoBehaviour
{
    public int layer;
    GameController gameController;
    public Sprite[] bgOption;
    public Sprite[] fogOption;

    // 0 = mountain, 1 = fog
    private void Start()
    {
        gameController = GameObject.Find("gameController").GetComponent<GameController>();
        //float yPos = gameObject.transform.localPosition.y;
        if(layer == 0)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        if (layer == 1)
        {
            gameObject.transform.localScale = new Vector3(2, 1 , 1);
        }
    }
    void FixedUpdate()
    {
        if (layer == 0)
        {
            if (gameController.bgSpeed > .1f && gameController.bgSpeed < 100f)
            {
                transform.Translate(Vector3.left * gameController.bgSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.left * 1 * Time.deltaTime);
            }
            if (gameObject.transform.localPosition.x < -30)
            {
                transform.position = new Vector3(20, transform.position.y, 0);
                gameObject.GetComponent<SpriteRenderer>().sprite = bgOption[Random.Range(0, bgOption.Length)];
            }

        }
        if (layer == 1)
        {
            if (gameController.bgSpeed > .1f && gameController.bgSpeed < 100f)
            {
                transform.Translate(Vector3.left * (gameController.bgSpeed/2) * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.left * 1 * Time.deltaTime);
            }
            if (gameObject.transform.localPosition.x < -30)
            {
                transform.position = new Vector3(20, transform.position.y, 0);
                gameObject.GetComponent<SpriteRenderer>().sprite = bgOption[Random.Range(0, bgOption.Length)];
            }

        }
        if (layer == 2)
        {
            if (gameController.bgSpeed > .1f && gameController.bgSpeed < 100f)
            {
                transform.Translate(Vector3.left * (gameController.bgSpeed/3) * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.left * 1 * Time.deltaTime);
            }
            if (gameObject.transform.localPosition.x < -30)
            {
                transform.position = new Vector3(20, transform.position.y, 0);
                gameObject.GetComponent<SpriteRenderer>().sprite = bgOption[Random.Range(0, bgOption.Length)];
            }

        }
        if (layer == 3)
        {
            if (gameController.bgSpeed > .1f && gameController.bgSpeed < 100f)
            {
                transform.Translate(Vector3.left * (gameController.bgSpeed*1.35f) * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.left * 1.35f * Time.deltaTime);
            }
            if (gameObject.transform.localPosition.x < -30)
            {
                transform.position = new Vector3(20, transform.position.y, 0);
                gameObject.GetComponent<SpriteRenderer>().sprite = fogOption[Random.Range(0, bgOption.Length)];
            }
        }
        if (layer == 4)
        {
            if (gameController.bgSpeed > .1f && gameController.bgSpeed < 100f)
            {
                float spMod = (gameController.bgSpeed * 1.35f);
                transform.Translate(Vector3.left * (spMod / 2) * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.left * 1.35f * Time.deltaTime);
            }
            if (gameObject.transform.localPosition.x < -30)
            {
                transform.position = new Vector3(20, transform.position.y, 0);
                gameObject.GetComponent<SpriteRenderer>().sprite = fogOption[Random.Range(0, bgOption.Length)];
            }
        }
        if (layer == 5)
        {
            if (gameController.bgSpeed > .1f && gameController.bgSpeed < 100f)
            {
                float spMod = (gameController.bgSpeed * 1.35f);
                transform.Translate(Vector3.left * (spMod / 3) * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.left * 1.35f * Time.deltaTime);
            }
            if (gameObject.transform.localPosition.x < -30)
            {
                transform.position = new Vector3(20, transform.position.y, 0);
                gameObject.GetComponent<SpriteRenderer>().sprite = fogOption[Random.Range(0, bgOption.Length)];
            }
        }
    }
}
