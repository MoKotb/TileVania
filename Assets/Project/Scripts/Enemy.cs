using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float enemySpeed = 1f;

    private Rigidbody2D rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (transform.localScale.x > 0)
        {
            rigidbody.velocity = new Vector2(enemySpeed, 0f);
        }
        else
        {
            rigidbody.velocity = new Vector2(-enemySpeed, 0f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-1 * Mathf.Sign(rigidbody.velocity.x),1f);
    }
}
