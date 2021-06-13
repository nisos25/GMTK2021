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
    [SerializeField] private Transform tongue;
    
    private float lookRotation = 180;
    private float originalAttackTime;

    private bool attacking;

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
        float distance = Vector2.Distance(tongue.position, attackObjective.transform.position);

        if (distance < attackRadius)
        {
            if (!attacking)
            {
                var direction = attackObjective.transform.position - tongue.position;

                lookRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                tongue.rotation = Quaternion.Slerp(tongue.rotation, Quaternion.Euler(0, 0, lookRotation + 90),
                    Time.deltaTime * rotationSpeed);

                Debug.Log(Quaternion.Angle(tongue.rotation, Quaternion.Euler(0, 0, lookRotation + 90)));
                
                if (Quaternion.Angle(tongue.rotation, Quaternion.Euler(0, 0, lookRotation + 90)) < 0.1f)
                {
                    StartCoroutine(Attack(distance));
                    
                }
                
                if (attackTime <= 0)
                {
                    attackTime = originalAttackTime;
                    StartCoroutine(Attack(distance));
                }
                
                attackTime -= Time.deltaTime;
            }
        }
        else
        {
            attackTime = originalAttackTime;
            ResetTonguePosition();
        }
    }

    private IEnumerator Attack(float distance)
    {
        yield return new WaitForSeconds(0.02f);
        
        if (tongue.transform.localScale.y >= distance - .5f )
        {
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(ReturnToTonguePosition());
            yield break;
        }

        tongue.transform.localScale += new Vector3(0, tongueVelocity, 0);
        attacking = true;
        yield return StartCoroutine(Attack(distance));
    }

    private IEnumerator ReturnToTonguePosition()
    {
        yield return new WaitForSeconds(0.02f);
        
        if (tongue.transform.localScale.y <= 1)
        {
            attacking = false;
            yield break;
        }

        tongue.transform.localScale -= new Vector3(0, tongueVelocity, 0);
        yield return StartCoroutine(ReturnToTonguePosition());
    }
    
    private void ResetTonguePosition()
    {
        tongue.rotation = Quaternion.Slerp(tongue.rotation, Quaternion.Euler(0, 0, 0),
            Time.deltaTime * rotationSpeed);
    }
}