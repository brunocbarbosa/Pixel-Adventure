using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [Header("Cut Initial")]
    public Transform point;

    [Header("Sound Config")]
    private AudioSource fx;
    
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<Player>().transform.position = point.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        instance = this;
        fx = GetComponent<AudioSource>();
    }

    public void PlayFX(AudioClip clip)
    {
        fx.PlayOneShot(clip);
    }
}
