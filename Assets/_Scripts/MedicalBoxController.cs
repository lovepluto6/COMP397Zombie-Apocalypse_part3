using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicalBoxController : MonoBehaviour
{
    public int health;
    [Header("Player Sounds")]
    public AudioSource recoverSound;
    
    [Header("HealthBar")]
    public HealthBarScreenSpaceController healthBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("123344");
            RecoverHealth(50);
            Destroy(this.gameObject);
        }
    }
    public void RecoverHealth(int damage)
    {
        Debug.Log("123344");
        recoverSound.Play();
        health =100;
        healthBar.RecoverHealth(damage);
        if (health >100)
        {
            health = 100;
        }
    }
}
