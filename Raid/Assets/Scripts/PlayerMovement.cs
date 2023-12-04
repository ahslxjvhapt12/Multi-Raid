using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float speed = 5f;
    PlayerAnimation playerAnimation;
    Rigidbody2D _rigid;

    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        _rigid.velocity = new Vector3(h, v, 0).normalized * speed;

        playerAnimation.SetAnimation(h, v);
    }
}
