using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public float speed = 5f;
    public float itemPoint;
    void Update()
    {
        transform.Translate(speed * Vector2.down * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.tag == "Player"){
            if(gameObject.tag == "Point"){
                GameController.Instance.points++;
            }else if(gameObject.tag == "Special Point"){
                GameController.Instance.points+= 5;
            }
            
            Destroy(gameObject);
        }
    }
}
