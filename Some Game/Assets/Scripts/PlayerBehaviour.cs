using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Range(1, 10)]
    public int playerHealth;
    public int kunaiAmount;
    [SerializeField] 
    private float jumpSpeed;
    [SerializeField] 
    private float playerSpeed;
    [SerializeField]
    private Vector2 impact;
    [SerializeField] 
    private LayerMask platformsLayerMask;
    [SerializeField] 
    private GameObject throwingKunai;

    public GameObject body;

    private Rigidbody2D playerRigidbody;
    private PolygonCollider2D playerCollider;
    private Animator playerAnimator;
    private Vector2 playerDirection;

    private bool movementState = true;

    // Start is called before the first frame update
    void Start()
    {
        //kunaiAmount = 0;

        playerRigidbody = transform.GetComponent<Rigidbody2D>();
        playerCollider = transform.GetComponent<PolygonCollider2D>();
        playerAnimator = body.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movementState)
        {
            playerAnimator.SetTrigger("NoDamage");
            PlayerMovement();
        }

        if (IsGrounded())
        {
            playerAnimator.SetFloat("jumpspeed", 0);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerRigidbody.velocity = Vector2.up * jumpSpeed;
            }
        }
        else
        {
            playerAnimator.SetTrigger("NoDamage");
            playerAnimator.SetFloat("jumpspeed", jumpSpeed);
        }

        if (Input.GetKeyDown(KeyCode.E) && kunaiAmount > 0)
        {
            Attack();
        }

        if (playerHealth == 0)
        {
            Debug.Log("Death by lost hp");
            UIManager.LoadSceneFromGame(1);
        }

        if (playerRigidbody.velocity.y < -50f)
        {
            UIManager.LoadSceneFromGame(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CollectableKunai"))
        {
            kunaiAmount++;
            Debug.Log("kunai raised\n" + "kunai = " + kunaiAmount);
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Slime"))
        {
            playerHealth--;
            Debug.Log("lost 1 hp\n" + "hp = " + playerHealth);
            try
            {
                Destroy(UIManager.healthImage.Dequeue().gameObject);
            }
            catch { }
            movementState = false;
            playerRigidbody.AddForce(new Vector2(-playerDirection.x * impact.x, impact.y), ForceMode2D.Impulse);
            StartCoroutine("Impact");
        }
    }

    private IEnumerator Impact()
    {
        playerAnimator.SetTrigger("TakeDamage");
        yield return new WaitForSeconds(1.5f);
        movementState = true;
    }

    private void Attack()
    {
        kunaiAmount--;
        throwingKunai.GetComponent<KunaiBehaviour>().direction = playerDirection;
        Instantiate(throwingKunai, new Vector3(transform.position.x, transform.position.y - 0.8f), Quaternion.identity);
    }

    private void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            playerDirection = Vector2.left;
            playerRigidbody.velocity = new Vector2(-playerSpeed, playerRigidbody.velocity.y);
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            playerAnimator.SetFloat("speed", playerSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            playerDirection = Vector2.right;
            playerRigidbody.velocity = new Vector2(playerSpeed, playerRigidbody.velocity.y);
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            playerAnimator.SetFloat("speed", playerSpeed);
        }
        else
        {
            playerRigidbody.velocity = new Vector2(0, playerRigidbody.velocity.y);
            playerAnimator.SetFloat("speed", 0);
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(playerCollider.bounds.center, Vector2.down, 2f, platformsLayerMask);
        //Debug.Log(raycastHit2D.collider);
        return raycastHit2D.collider != null;
    }
}
