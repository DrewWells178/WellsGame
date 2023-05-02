using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechMeleeWeapon : MonoBehaviour
{ 
 public float timeBetweenMeleeAttacks;
float nextMeleeAttackTime;

public Transform meleeAttackPoint;
public float meleeAttackRange;
public LayerMask enemiesLayer;
public int damage;

// Start is called before the first frame update
void Start()
{

}

// Update is called once per frame
void Update()
{

    if (Time.time > nextMeleeAttackTime)
    {
        if (Input.GetMouseButton(1))
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(meleeAttackPoint.position, meleeAttackRange, enemiesLayer);
            foreach (Collider2D col in enemiesToDamage)
            {
                col.GetComponent<Enemy>().TakeDamage(damage);
            }
            //Attack animation anim.SetTrigger("meleeattack");
            nextMeleeAttackTime = Time.time + timeBetweenMeleeAttacks;

        }
    }
}
private void OnDrawGizmosSelected()
{
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(meleeAttackPoint.position, meleeAttackRange);
}
}
