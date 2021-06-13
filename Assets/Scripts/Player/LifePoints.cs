using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class LifePoints : MonoBehaviour
{
    [field: SerializeField] public SpriteRenderer Sprite { get; set; }
    [SerializeField] private GameObject[] flies;
    
    public int lifePoints = 100;
    public bool isDead;

    int flyStealInsects = 1;
    int flyStealRabbit = 5;
    
    // PostProcessing
    GameObject postProcessingGO;

    // Scripts
    RabbitFalls fallScript;
    CameraShake shakeScript;
    LevelManager levelScript;

    private List<GameObject> fliesToKill = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < flies.Length; i++)
        {
            fliesToKill.Add(flies[i]);
        }

        Debug.Log(fliesToKill.Count);
        
        fallScript = GetComponent<RabbitFalls>();
        levelScript = GameObject.Find("GameManager").GetComponent<LevelManager>();
        shakeScript = GameObject.Find("CM vcam1").GetComponent<CameraShake>();
        postProcessingGO = GameObject.Find("PostProcessingGO");
        postProcessingGO.SetActive(false);
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(lifePoints <= 0 && !isDead)
        {
            isDead = !isDead;
            levelScript.ItDed();
        }
    }

    private void OnTriggerEnter2D(Collider2D _col)
    {
        if (_col.CompareTag("Enemy"))
        {
            Vector2 direction = Sprite.flipX ? Vector2.right : Vector2.left;

            fallScript.Knockback(direction, 800);
            TakeDamage();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        EnemyController enemy ;
        
        if (other.gameObject.TryGetComponent(out enemy) && fallScript.isRabbit)
        {
            foreach (var contact in other.contacts)
            {
                if (contact.normal.y >= 0.9f)
                {
                    enemy.KillEnemy();
                    fallScript.Knockback(Vector2.up);
                    
                    lifePoints += Random.Range(7, 11);

                    if (lifePoints > 100)
                    {
                        lifePoints = 100;
                    }
                }
                else
                {
                    fallScript.Knockback(contact.normal, 800);
                    TakeDamage();
                }
            }
        }
    }

    private void TakeDamage()
    {
        //shakeScript.ShakeCamera(2f, 0.1f);

        fliesToKill.First().gameObject.SetActive(false);
        fliesToKill.RemoveAt(0);
        
        if (fallScript.isRabbit)
        {
            lifePoints -= flyStealRabbit;
        }else if (!fallScript.isRabbit) {
            lifePoints -= flyStealInsects;
        }

        if (lifePoints <= 10 && !isDead)
        {
            postProcessingGO.SetActive(true);
        }
    }
}
