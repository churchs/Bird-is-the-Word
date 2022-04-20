using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CockatielController : MonoBehaviour
{
    public Animator animator;
    public GameController gameController;
    void FixedUpdate()
    {
        if (!gameController.paused)
        {
            if (gameController.cockSpeed > 0)
            {
                animator.SetFloat("flapSpeed", gameController.cockSpeed * .11f);
            }
            else
            {
                animator.SetFloat("flapSpeed", .02f);
            }
        }
        else
        {
            animator.SetFloat("flapSpeed", .06f);

        }

    }
}
