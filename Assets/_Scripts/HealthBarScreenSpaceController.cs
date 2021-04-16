using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class HealthBarScreenSpaceController : MonoBehaviour
{
    [Header("Health Properties")]
    [Range(0, 100)]
    public int currentHealth = 100;
    [Range(1, 100)]
    public int maximumHealth = 100;

    private Slider healthBarSlider;
    public PlayerBehaviour player;
    [Header("Scene Data")]
    public SceneDataSO sceneData;
    // Start is called before the first frame update
    void Start()
    {
        healthBarSlider = GetComponent<Slider>();
        currentHealth = maximumHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth == 0)
        {
            player.controller.enabled = false;
            player.transform.position = sceneData.playerposition;
            player.controller.enabled = true;

            player.health = sceneData.playerHealthy;
            player.healthBar.SetHealthy(sceneData.playerHealthy);
        }
    }

    public void TakeDamage(int damage)
    {
        healthBarSlider.value -= damage;
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            healthBarSlider.value = 0;
            currentHealth = 0;
        }
    }
    public void RecoverHealth(int damage)
    {
        healthBarSlider.value += damage;
        currentHealth += damage;
        if (currentHealth > 100)
        {
            healthBarSlider.value = 100;
            currentHealth = 100;
        }
    }

    public void SetHealthy(int healthValue)
    {
        healthBarSlider.value = healthValue;
        currentHealth = healthValue;
    }

    public void Reset()
    {
        healthBarSlider.value = maximumHealth;
        currentHealth = maximumHealth;
    }
}
