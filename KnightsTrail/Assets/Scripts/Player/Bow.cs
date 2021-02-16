using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public Transform firePoint;
    public Transform bowPosition;
    public GameObject arrowPrefab;
    public Animator animator;
    public float launchForce;
    public CharacterController2D characterController;
    public Texture2D cursorArrow;
    public bool intro;
    [HideInInspector] public bool InputDisabled;
    [HideInInspector] public Vector2 direction;

    public AudioSource shotSound;

    private void Start()
    {
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
    }

    void Update()
    {
        if (InputDisabled == false)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                animator.SetTrigger("Shoot");
            }
        }  
    }

    private void FixedUpdate()
    {
        if (intro == false)
        {
            Vector2 playerPosition = transform.position;
            Vector2 bowFixedPosition = bowPosition.position;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = mousePosition - playerPosition;
            Vector2 directionOfBow = mousePosition - bowFixedPosition;
            bowPosition.right = directionOfBow;
        }

        if (direction.x < 0 && characterController.m_FacingRight == true)
        {
            if (characterController.moving == false)
                characterController.Flip();
        }
        else if (direction.x >= 0 && characterController.m_FacingRight == false)
        {
            if (characterController.moving == false)
                characterController.Flip();
        }
    }

    void Shoot()
    {
        shotSound.Play();
        GameObject newArrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        newArrow.GetComponent<Rigidbody2D>().velocity = bowPosition.right * launchForce;
    }
}
