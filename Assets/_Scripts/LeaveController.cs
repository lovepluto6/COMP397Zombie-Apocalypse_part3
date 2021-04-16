using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveController : MonoBehaviour
{
    public bool isLeave = false;
    

    //public GameObject MissionLabel;
    public GameObject SuccessLabel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isLeave == true)
        {
            SuccessLabel.SetActive(isLeave = true);
            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isLeave = true;
        }
    }
}
