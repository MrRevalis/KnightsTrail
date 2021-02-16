using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcherController : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform bowPosition;
    public float shootPeriod = 1f;
    public int lives = 3;
    public Collider2D OwnCollider;

    public Transform spawnArrowPosition;
    private float lastShoot;
    private float lastDamage;
    private Transform target;

    private bool hitted = false;
    private bool facingRight = true;
    private Animator anim;
    [HideInInspector] public Vector2 direction;

    public AudioSource hit;
    public AudioSource spawnSound;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 ownPosition = transform.position;
        Vector2 targetPosition = target.transform.position;
        Vector2 bowFixedPosition = bowPosition.position;

        direction = targetPosition - ownPosition;
        Vector2 directionOfBow = targetPosition - bowFixedPosition;
        bowPosition.right = directionOfBow;

        if (lives > 0)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            if (direction.x < 0 && facingRight)
            {
                transform.Rotate(0f, 180f, 0f);
                facingRight = false;
            }
            if (direction.x > 0 && !facingRight)
            {
                transform.Rotate(0f, 180f, 0f);
                facingRight = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision.collider, OwnCollider);
        }
    }

    public void StartAttacking()
    {
        if (Time.time - lastShoot > shootPeriod && !hitted)
        {
            lastShoot = Time.time;
            anim.SetBool("Attacking", true);
        }
    }

    public void StopAttacking()
    {
        anim.SetBool("Attacking", false);
    }

    public void ShootArrow()
    {
        spawnSound.Play();
        Instantiate(arrowPrefab, spawnArrowPosition.position, spawnArrowPosition.rotation);
    }

    public void ShootEnd()
    {
        anim.SetBool("Attacking", false);
    }

    public void TakeDamage()
    {
        if(Time.time - lastDamage > 0.5 && lives > 0)
        {
            hit.Play();
            hitted = true;
            anim.SetBool("Attacking", false);
            anim.SetBool("Hit", true);
            lastDamage = Time.time;
        }
    }

    public void StopHitAnim()
    {
        Debug.Log("stop anim");
        anim.SetBool("Hit", false);
        if (--lives == 0)
        {
            anim.SetTrigger("Death");
        } else
        {
            hitted = false;
        }
    }

    public void StopDieAnim()
    {
        FindObjectOfType<MoneySystem>().SpawnCoin(transform.position);
        Destroy(gameObject);
    }
}
