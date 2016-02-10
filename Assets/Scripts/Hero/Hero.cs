using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {
    public float hitPoints = 5;
    public float attackPower = 1f;
    public float attackCooldown = 0.5f;
    public float areaOfEffect = 1f;
    public int bulletsInChamber = 6;
    public Perk activePerk;
    [HideInInspector]
    public float currentHitPoints;
    [HideInInspector]
    public float currentAttackPower;
    [HideInInspector]
    public float currentAttackCooldown;
    [HideInInspector]
    public float currentAreaOfEffect;
    [HideInInspector]
    public int currentBulletsLeft;

    public InGame inGame;
    // Use this for initialization
    void Start () {
	
	}

    public void setCurrentValues()
    {
        currentHitPoints = hitPoints;
        currentAttackPower = attackPower;
        currentAttackCooldown = attackCooldown;
        currentAreaOfEffect = areaOfEffect;
        currentBulletsLeft = bulletsInChamber;
    }

    public void Initialize(float hitPoints, float attackPower, float attackCooldown, float areaOfEffect, int bulletsInChamber)
    {

    }

    public void GetHit(float attackPower)
    {

        currentHitPoints -= attackPower;
        if (currentHitPoints <= 0)
        {
            print("game over");
        }
    }

    public void Death()
    {

    }

    public void activatePerk(Perk perk)
    {

    }

    public void deactivatePerk()
    {

    }

    // Update is called once per frame
    void Update () {
	
	}
}
