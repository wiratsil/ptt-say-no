using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 8f;
    float radius;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveMentManagement();
    }

    void moveMentManagement() {
        float moveHorizontal = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        if(transform.position.x + radius >= GameController.topRight.x && moveHorizontal > 0){
            moveHorizontal = 0;
        }

        if(transform.position.x - radius <= GameController.bottomLeft.x && moveHorizontal < 0){
            moveHorizontal = 0;
        }

        if(moveHorizontal != 0){
            transform.Translate (moveHorizontal * Vector2.right);
        }

        if(Input.GetMouseButton(0)){
            Vector3 mosuePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if(mosuePos.x > 0.5 && transform.position.x <= GameController.topRight.x){
                transform.Translate (Time.deltaTime * speed, 0, 0);
            }else if(mosuePos.x < -0.5 && transform.position.x >= GameController.bottomLeft.x){
                transform.Translate (Time.deltaTime * -speed, 0, 0);
            }
        }
    }
}
