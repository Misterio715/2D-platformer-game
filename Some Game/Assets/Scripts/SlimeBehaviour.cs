using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehaviour : MonoBehaviour
{
    private Animator slimeAnimator;

    // Start is called before the first frame update
    void Start()
    {
        slimeAnimator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        slimeAnimator.SetFloat("Impact", 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MyPlayer"))
        {
            slimeAnimator.SetFloat("Impact", 1);
        }
    }
}
