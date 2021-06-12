using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePoints : MonoBehaviour
{

    public int lifePoints = 100;
    bool isDead = false;

    int flyStealInsects = 1;
    int flyStealRabbit = 5;

    // PostProcessing
    GameObject postProcessingGO;

    // Scripts
    RabbitFalls fallScript;
    CameraShake shakeScript;

    // Start is called before the first frame update
    void Start()
    {

        fallScript = GetComponent<RabbitFalls>();
        shakeScript = GameObject.Find("CM vcam1").GetComponent<CameraShake>();
        postProcessingGO = GameObject.Find("PostProcessingGO");
        postProcessingGO.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(lifePoints <= 0 && !isDead)
        {
            Debug.Log("Chuchalas you ded");
            if (!fallScript.isRabbit)
            {
                Destroy(this.gameObject);
            }

            isDead = !isDead;
        }
    }

    private void OnTriggerEnter2D(Collider2D _col)
    {
        if (_col.CompareTag("Enemy"))
        {

            shakeScript.ShakeCamera(2f, 0.1f);

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

}
