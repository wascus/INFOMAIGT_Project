using UnityEngine;
using System.Collections;

public class HealthComponent : MonoBehaviour
{

    public int MaximumHealth;
    public int CurrentHealth;

    void Start ()
    {
        CurrentHealth = MaximumHealth;
    }

    void Update()
    {
        if (CurrentHealth <= 0)
        {
            //gameObject.SetActive (false);
        }
    }

    public void Damage (int damageToGive)
    {
        CurrentHealth = CurrentHealth - damageToGive;
    }

    public void Heal (int healthToGive)
    {
        CurrentHealth = CurrentHealth + healthToGive;
    }
}
