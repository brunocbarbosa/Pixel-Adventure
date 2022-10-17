using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melon : MonoBehaviour
{
    public AudioClip melonSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.instance.PlayFX(melonSound);
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().IncreaseScore();
            Destroy(gameObject);
        }
    }
}
