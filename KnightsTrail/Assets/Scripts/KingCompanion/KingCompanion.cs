using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingCompanion : MonoBehaviour
{
    public float speed;
    public float lineOfSight;
    public GameObject target;
    public GameObject Wizard1;
    public GameObject Wizard2;
    public Animator anim;
    public AudioSource swordHit;

    public Transform attackPoint;
    public LayerMask enemyLayer;
    public float distanceToAttack;

    private float time;

    public void playSwordSound()
    {
        swordHit.Play();
    }

    public void stopSwordSound()
    {
        swordHit.Stop();
    }


    private void Start()
    {
        time = Time.time;
    }

    public void TakeDamage()
    {
        anim.SetTrigger("Hit");
    }

    void Update()
    {
        if (Wizard1 != null)
            target = Wizard1;
        else if (Wizard2 != null)
            target = Wizard2;
        else
        {
            Destroy(gameObject);
            return;
        }

        anim.SetBool("Walk", false);

        Vector3 enemySite = target.transform.position - transform.position;
        if (enemySite.x < 0)
            transform.rotation = new Quaternion(0, 180, 0, 0);
        else
            transform.rotation = new Quaternion(0, 0, 0, 0);

        float distancefromTarget = Vector2.Distance(target.transform.position, transform.position);
        if (distancefromTarget < lineOfSight)
        {
            anim.SetBool("Walk", true);
            transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, speed * Time.deltaTime);
        }

        if (distancefromTarget < distanceToAttack)
        {
            if (Time.time >= time + 1.5f)
            {
                Attack();
                time = Time.time;
            }

        }
        else
        {
            anim.SetBool("Attack", false);
            anim.SetBool("Attack2", false);
        }
    }

    void Attack()
    {      
        int rInt = Random.Range(0, 1);

        if(rInt == 1)
        {
            anim.SetBool("Attack", true);
        }
        else
            anim.SetBool("Attack2", true);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, distanceToAttack, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.gameObject.name.Equals("Frog"))
            {
                enemy.gameObject.GetComponent<FrogMovement>().DeathTime();
            }
            else if (enemy.gameObject.name.Equals("Slime"))
            {
                enemy.gameObject.GetComponent<SlimeMovement>().DeathTime();
            }
            else if (enemy.CompareTag("ArcherEnemy"))
            {
                enemy.GetComponent<ArcherController>().TakeDamage();
            }
            else if (enemy.gameObject.name.Equals("Skeleton_collider") || enemy.gameObject.name.Equals("SKeleton_sprites"))
            {
                enemy.gameObject.GetComponent<SkeletonBehaviour>().TakeDamage();
            }
            else if (enemy.gameObject.name.Equals("EnemySkeleton"))
            {
                enemy.gameObject.GetComponent<SkeletonBehaviour>().TakeDamage();
            }
            else if (enemy.gameObject.name.Equals("FlyingEye"))
            {
                enemy.gameObject.GetComponent<EyeFollowPlayer>().TakeDamage();
            }
            else if (enemy.gameObject.name.Equals("EvilWizardBoss"))
            {
                enemy.gameObject.GetComponent<BossScript>().TakeDamage();
            }
            else if (enemy.gameObject.name.Equals("EvilWizardBoss2"))
            {
                enemy.gameObject.GetComponent<Boss2Script>().TakeDamage();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);

        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, distanceToAttack);
    }
}
