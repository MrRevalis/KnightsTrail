using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    public float diameter;
    public float RotateSpeed;
    public Transform target;
    void FixedUpdate()
    {
        transform.position = target.position;
        transform.Translate(diameter, 0, 0);
        transform.Rotate(0, 0, RotateSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Debug.Log("SIEMASDMASDMASDMSAD");
            float strength = 5f;
            Vector3 direction = (this.transform.position - collision.transform.position) / (this.transform.position - collision.transform.position).magnitude;
            Debug.Log(direction * strength);
            if(direction.x < 0)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-7, collision.transform.up.y) * strength, ForceMode2D.Impulse);
            }
            else
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(7, collision.transform.up.y) * strength, ForceMode2D.Impulse);
            }
            //collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * strength, ForceMode2D.Force);
            FindObjectOfType<HealthController>().TakeDamage();
        }
    }
}
