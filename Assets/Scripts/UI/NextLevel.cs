using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    LevelManager levelScript;

    // Start is called before the first frame update
    void Start()
    {
        levelScript = GameObject.Find("GameManager").GetComponent<LevelManager>();
    }


    private void OnTriggerEnter2D(Collider2D _col)
    {
        if (_col.CompareTag("Player"))
        {
            levelScript.OpenNextLevel();
        }
    }
}
