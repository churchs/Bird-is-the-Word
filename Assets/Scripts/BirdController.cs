using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    public Vector3 birdEndLoc;
    public bool birdDone = false;
    public float lifespan;

    // Update is called once per frame


    public void BirdDone(float yLoc)
    {
        birdEndLoc = new Vector3(gameObject.transform.localPosition.x + 2000, yLoc, 0f);
        birdDone = true;
        StartCoroutine(DestroyBird(9));
    }
    private IEnumerator DestroyBird(int timer) 
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
    void FixedUpdate()
    {
        
        if (birdDone == true)
        {
        gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, birdEndLoc, (850f * Time.deltaTime));
        }
        else
        {
            return;
        }
    }
}
