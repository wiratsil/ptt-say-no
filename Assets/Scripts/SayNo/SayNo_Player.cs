using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SayNo_Player : MonoBehaviour
{
    public SayNo_GameManager sayNo_GameManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        sayNo_GameManager.AddScore(collision.tag);
        if (collision.tag != "Untagged")
        {
            collision.gameObject.SetActive(false);
        }
    }

}
