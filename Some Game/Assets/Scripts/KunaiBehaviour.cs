using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class KunaiBehaviour : MonoBehaviour
{
    [HideInInspector]
    public Vector2 direction;
    private float speed = 20f;
    private Rigidbody2D kunaiRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        kunaiRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        kunaiRigidbody.velocity = direction * speed;
        transform.localScale = new Vector3(direction.x, transform.localScale.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Slime"))
        {
            Debug.Log("hit in slime");
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Platform"))
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
