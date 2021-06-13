using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    GameObject playerGO;
    GameObject check1;
    GameObject check2;
    GameObject check3;

    LevelManager levelScript;

    // Start is called before the first frame update
    void Start()
    {
        playerGO = GameObject.Find("Player");
        check1 = GameObject.Find("CheckPoint-1");
        check2 = GameObject.Find("CheckPoint-2");
        check3 = GameObject.Find("CheckPoint-3");

        levelScript = GameObject.Find("GameManager").GetComponent<LevelManager>();

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (levelScript.level1.activeSelf)
            {
                playerGO.transform.position = check1.transform.position;
            }else if (levelScript.level2.activeSelf)
            {
                playerGO.transform.position = check2.transform.position;
            }else if (levelScript.level3.activeSelf)
            {
                playerGO.transform.position = check3.transform.position;
            }
        }
    }


}
