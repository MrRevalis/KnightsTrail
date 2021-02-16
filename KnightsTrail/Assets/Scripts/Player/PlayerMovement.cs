using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D characterController;
    public Animator animator;
    public Rigidbody2D rb;
    public bool CanMove = true;
    [HideInInspector] public bool isDisabled;

    float horizontalMove = 0f;
    float verticalMove = 0f;
    [HideInInspector] public float movementSpeed = 40f;
    bool shouldJump = false;
    bool shouldCrouch = false;
    bool isClimbing = false;

    public bool carryingStuff = false;

    public AudioSource Jump;
    public AudioClip[] AudioJumpClips;
    public AudioSource Land;
    public AudioClip[] AudioLandClips;
    private int i = 0;
    private int j = 0;


    [Header("Dashing")]
    public bool canDash = true;
    public float dashSpeed;
    public float dashingTime;
    public float timeBetweenDashes;

    public void JumpSound()
    {
        if (i == 3)
            i = 0;
        Jump.clip = AudioJumpClips[i++];
        Jump.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * movementSpeed;

            verticalMove = Input.GetAxis("Vertical") * 10;
            animator.SetFloat("PlayerSpeed", Mathf.Abs(horizontalMove));

            /*if (horizontalMove != 0)
            {
                //Debug.Log("Graj");
                if(!Walk.isPlaying)
                    Walk.Play();
            }
            else
            {
                //Debug.Log("Koniec gry");
                Walk.Stop();
            }*/


            if(isDisabled == false)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    animator.SetBool("ShouldJump", true);
                    shouldJump = true;
                }

                if (Input.GetButtonDown("Dash"))
                {
                    DashAbility();
                }
            }

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) isClimbing = true;
            else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)) isClimbing = false;
        }
        else horizontalMove = 0;
    }

    public void OnLanding()
    {
        animator.SetBool("ShouldJump", false);
        animator.SetBool("PlayerHurt", false);
    }

    public void PlayLandingSound()
    {
        if (j == 2)
            j = 0;
        Land.clip = AudioLandClips[j++];
        Land.Play();
    }

    void FixedUpdate()
    {
        characterController.Move(horizontalMove * Time.fixedDeltaTime, verticalMove, shouldCrouch, shouldJump, isClimbing);
        shouldJump = false;  
    }

    private void DashAbility()
    {
        if (canDash)
        {
            StartCoroutine(Dash());
        }
    }
    IEnumerator Dash()
    {
        canDash = false;
        movementSpeed = dashSpeed;

        yield return new WaitForSeconds(dashingTime);
        movementSpeed = 40f;
        yield return new WaitForSeconds(timeBetweenDashes);
        canDash = true;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("MovingPlatform"))
            this.transform.parent = collision.transform;

        if (collision.gameObject.name.Equals("Frog"))
        {
            FindObjectOfType<HealthController>().TakeDamage();
            //Zmienic żeby bohater bardziej się odbijał
            if(transform.position.x < collision.transform.position.x)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(-7 * 5f, transform.up.y * 5f), ForceMode2D.Impulse);
            }
            else
                GetComponent<Rigidbody2D>().AddForce(new Vector2(7 * 5f,transform.up.y * 5f), ForceMode2D.Impulse);
            //animator.SetBool("PlayerHurt", true);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("MovingPlatform"))
            this.transform.parent = null;

        if (collision.gameObject.name.Equals("Frog"))
        {
            animator.SetBool("PlayerHurt", false);
        }

        if (collision.gameObject.name.Equals("EnemySkeleton") || collision.gameObject.name.Equals("hitBox"))
        {
            animator.SetBool("PlayerHurt", false);
        }
    }

    /*public void ChangeMusicSource(string location)
    {
        int arrayPosition = MusicPosition(location);
        Walk.clip = AudioClips[arrayPosition];
        Debug.Log(Walk.clip.name);
    }

    private int MusicPosition(string location)
    {
        switch (location)
        {
            case "ForestInfo": return 0;
            case "MineInfo": return 1;
            case "VillageInfo": return 2;
            default: return 0;
        }
    }*/
}
