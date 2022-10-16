using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public Animator animator;
    private bool jump = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetInteger("transitionTrampoline", 1);
            jump = true;
            collision.gameObject.GetComponent<Player>().Trampoline();
            
        }

        if (jump)
        {
            animator.SetInteger("transitionTrampoline", 0);
        }
    }
}
