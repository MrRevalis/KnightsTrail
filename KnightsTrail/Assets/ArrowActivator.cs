using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowActivator : MonoBehaviour
{
    public GameObject gate;
    public AudioSource open;
    public AudioSource impact;
    private Vector3 movingDirection;
    private bool wasMoved = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.name.Equals("ArrowPlayer(Clone)"))
        {
            impact.Play();
            if (!wasMoved)
            {
                Debug.Log("TESTETSM");
                open.Play();
                StartCoroutine(RotateObjectUp());
            }
        }
    }
    void Start()
    {
        movingDirection = new Vector3(0, 6, 0);
    }

    IEnumerator RotateObjectUp()
    {
        wasMoved = true;
        float timer = 0f;
        while (timer <= 1.0)
        {
            gate.transform.position += movingDirection * Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(gate.gameObject);
    }
}
