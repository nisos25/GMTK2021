using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    GameObject playerGO;
    Transform myTrans;
    // Start is called before the first frame update
    void Start()
    {
        playerGO = GameObject.Find("Player");
        myTrans = GetComponent<Transform>();
    }


    public void TakeMeToCheckpoint()
    {
        playerGO.transform.position = myTrans.position;
    }

}
