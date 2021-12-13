using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : MonoBehaviour
{
    public float speed = 9f;
    void Update()
    {
        transform.Translate(speed * Vector2.down * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.tag == "Player"){
            GameController.Instance.maxLife--;
            Destroy(gameObject);
        }
    }
}
