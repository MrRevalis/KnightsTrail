using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public static PlayerCombat Instance;

    public Animator animator;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public bool canReceiveInput = true;
    public bool inputReceived = false;

    public int damage = 40;

    public AudioSource attack1;
    public AudioSource attack2;

    [HideInInspector] public bool InputDisabled;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if(InputDisabled == false)
                Attack();
        }
    }

    public void PlaySoundFirstAttack()
    {
        attack1.Play();
    }

    public void StopSoundFirstAttack()
    {
        attack1.Stop();
    }

    public void PlaySoundSecondAttack()
    {
        attack2.Play();
    }

    public void StopSoundSecondAttack()
    {
        attack2.Stop();
    }

    public void Attack()
    {

        inputReceived = true;

        if (canReceiveInput)
        {
            inputReceived = true;
            canReceiveInput = false;

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.gameObject.name.Equals("Frog"))
                {
                    enemy.gameObject.GetComponent<FrogMovement>().TakeDamage();
                }
                else if (enemy.gameObject.name.Equals("Slime"))
                {
                    enemy.gameObject.GetComponent<SlimeMovement>().TakeDamage();
                }
                else if (enemy.CompareTag("ArcherEnemy"))
                {
                    enemy.GetComponent<ArcherController>().TakeDamage();
                }
                else if (enemy.gameObject.name.Equals("ArcherSkeleton"))
                {
                    enemy.GetComponent<ArcherController>().TakeDamage();
                }
                else if (enemy.gameObject.name.Equals("Skeleton_collider") || enemy.gameObject.name.Equals("SKeleton_sprites"))
                {
                    enemy.gameObject.GetComponent<SkeletonBehaviour>().TakeDamage();
                }
                else if(enemy.gameObject.name.Equals("EnemySkeleton"))
                {
                    enemy.gameObject.GetComponent<SkeletonBehaviour>().TakeDamage();
                }
                else if (enemy.gameObject.name.Equals("FlyingEye"))
                {
                    enemy.gameObject.GetComponent<EyeFollowPlayer>().TakeDamage();
                }
                else if (enemy.gameObject.name.Equals("Goblin"))
                {
                    enemy.gameObject.GetComponent<GoblinController>().TakeDamage();
                }
                else if (enemy.gameObject.name.Equals("MonsterSkeleton"))
                {
                    enemy.gameObject.GetComponent<Guardian>().TakeDamage();
                }
                else if (enemy.gameObject.name.Equals("EvilWizardBoss"))
                {
                    enemy.gameObject.GetComponent<BossScript>().TakeDamage();
                }
                else if (enemy.gameObject.name.Equals("EvilWizardBoss2"))
                {                   
                    enemy.gameObject.GetComponent<Boss2Script>().TakeDamage();
                }
                else if (enemy.gameObject.name.Equals("Mushroom"))
                {
                    enemy.gameObject.GetComponent<GoblinController>().TakeDamage();
                }
                else if (enemy.gameObject.name.Equals("LightBandit"))
                {
                    enemy.gameObject.GetComponent<BanditLightController>().TakeDamage();
                }
                else if (enemy.gameObject.name.Equals("Golem"))
                {
                    enemy.gameObject.GetComponent<Golem>().TakeDamage();
                }
                else if (enemy.gameObject.name.Equals("Guardian"))
                {
                    enemy.gameObject.GetComponent<Guardian>().TakeDamage();
                }
                else if (enemy.gameObject.name.Equals("Sprout"))
                {
                    enemy.gameObject.GetComponent<Sprout>().TakeDamage();
                }
                else if (enemy.gameObject.name.Equals("GuardianSpitter"))
                {
                    enemy.gameObject.GetComponent<ArcherController>().TakeDamage();
                }
                else if (enemy.gameObject.name.Equals("Stormhead"))
                {
                    enemy.gameObject.GetComponent<Guardian>().TakeDamage();
                }
            }

        }
        else
        {
            return;
        }
    }

    public void InputManager()
    {
        if (!canReceiveInput)
        {
            canReceiveInput = true;
        }
        else
        {
            canReceiveInput = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
