using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;

    public GameObject check1;
    public GameObject check2;
    public GameObject check3;

    public GameObject winPanel;

    public GameObject playerGO;

    LifePoints lifeScript;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;

        level1 = GameObject.Find("level-1");
        level2 = GameObject.Find("level-2");
        level3 = GameObject.Find("level-3");

        check1 = GameObject.Find("CheckPoint-1");
        check2 = GameObject.Find("CheckPoint-2");
        check3 = GameObject.Find("CheckPoint-3");

        winPanel = GameObject.Find("Win Panel");

        playerGO = GameObject.Find("Player");
        lifeScript = playerGO.GetComponent<LifePoints>();


        level2.SetActive(false);
        level3.SetActive(false);
        winPanel.SetActive(false);

    }


    public void OpenNextLevel()
    {
        if (level1.activeSelf)
        {
            level1.SetActive(false);
            level2.SetActive(true);
            playerGO.transform.position = check2.transform.position;
        }else if (level2.activeSelf)
        {
            level2.SetActive(false);
            level3.SetActive(true);
            playerGO.transform.position = check3.transform.position;
        }
        else
        {
            Time.timeScale = 0f;
            winPanel.SetActive(true);
        }
    }

    public void ItDed()
    {
        lifeScript.isDead = false;
        lifeScript.lifePoints = 100;
        lifeScript.ResetFlies();
        if (level1.activeSelf)
        {
            playerGO.transform.position = check1.transform.position;
        }
        else if (level2.activeSelf)
        {
            playerGO.transform.position = check2.transform.position;
        }
        else if (level3.activeSelf)
        {
            playerGO.transform.position = check3.transform.position;
        }
    }


}
