using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public Rigidbody2D rigidbody2;
    public Vector2 velocity;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //rigidbody2.MovePosition(rigidbody2.position - velocity * Time.deltaTime);
            rigidbody2.velocity = new Vector2(-speed, velocity.y);
        } else if (Input.GetKey(KeyCode.RightArrow))
        {
            //rigidbody2.MovePosition(rigidbody2.position + velocity * Time.deltaTime);
            rigidbody2.velocity = new Vector2(+speed, velocity.y);
        }
    }
}
