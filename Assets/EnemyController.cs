using System;
using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float attackRadius;
    [SerializeField] private float rotationSpeed = 10;
    [SerializeField] private float attackTime = 2;
    [SerializeField] private float tongueVelocity;
    
    [SerializeField] private GameObject attackObjective;
    [SerializeField] private Transform tonguePivot;
    [SerializeField] private GameObject tongueGfx;
    
    
    private float lookRotation = 180;
    private float originalAttackTime;

    private bool attacking;
    private bool firstAttack = true;
    
    public float AttackTime
    {
        get => attackTime;
        set
        {
            attackTime = value;
            originalAttackTime = attackTime;
        }
    }

    private void Start()
    {
        originalAttackTime = attackTime;
    }

    private void Update()
    {
        
        float distance = Vector2.Distance(tonguePivot.position, attackObjective.transform.position);

        if (distance < attackRadius)
        {
            if (!attacking)
            {
                var direction = attackObjective.transform.position - tonguePivot.position;

                lookRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                tonguePivot.rotation = Quaternion.Slerp(tonguePivot.rotation, Quaternion.Euler(0, 0, lookRotation + 90),
                    Time.deltaTime * rotationSpeed);

                attackTime -= Time.deltaTime;
                
                if (!(attackTime <= 0)) return;
                attackTime = originalAttackTime;
                tongueGfx.SetActive(true);
                StartCoroutine(Attack(distance));
            }
        }
        else
        {
            attackTime = originalAttackTime;
            ResetTongueRotation();
        }
    }
    
    private IEnumerator Attack(float distance)
    {
        yield return new WaitForSeconds(0.01f);
        
        if (tonguePivot.transform.localScale.y >= distance - .5f )
        {
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(ReturnToTonguePosition());
            yield break;
        }

        tonguePivot.transform.localScale += new Vector3(0, tongueVelocity, 0);
        attacking = true;
        yield return StartCoroutine(Attack(distance));
    }

    private IEnumerator ReturnToTonguePosition()
    {
        yield return new WaitForSeconds(0.01f);
        
        if (tonguePivot.transform.localScale.y <= 0.5f)
        {
            attacking = false;
            tongueGfx.SetActive(false);
            yield break;
        }

        tonguePivot.transform.localScale -= new Vector3(0, tongueVelocity, 0);
        yield return StartCoroutine(ReturnToTonguePosition());
    }
    
    private void ResetTongueRotation()
    {
        tonguePivot.rotation = Quaternion.Slerp(tonguePivot.rotation, Quaternion.Euler(0, 0, 0),
            Time.deltaTime * rotationSpeed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    public void KillEnemy()
    {
        Destroy(this);
    }
}