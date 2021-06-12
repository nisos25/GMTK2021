using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePoints : MonoBehaviour
{

    [SerializeField] int lifePoints = 100;
    bool isDead = false;

    RabbitFalls fallScript;

    // Start is called before the first frame update
    void Start()
    {

        fallScript = GetComponent<RabbitFalls>();

    }

    // Update is called once per frame
    void Update()
    {
        if(lifePoints <= 0 && !isDead)
        {
            Debug.Log("Chuchalas you ded");
            isDead = !isDead;
        }
    }

    private void OnTriggerEnter2D(Collider2D _col)
    {
        if (_col.CompareTag("Enemy"))
        {
            if (fallScript.isRabbit)
            {
                lifePoints -= 5;
            }else if (!fallScript.isRabbit) {
                lifePoints -= 1;
            }
            
        }
    }

}
