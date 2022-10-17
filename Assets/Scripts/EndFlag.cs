using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndFlag : MonoBehaviour
{
    public int stagenumber;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (stagenumber == 3)
            {
                collision.transform.GetComponent<Player>().FinishGame();
            }
            else
            {
                NextStage(stagenumber);
            }
           
        }
    }

    void NextStage(int stage)
    {
        SceneManager.LoadScene(stage);
    }
}
