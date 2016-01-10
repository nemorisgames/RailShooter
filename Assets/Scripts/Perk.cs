using UnityEngine;
using System.Collections;

public class Perk : MonoBehaviour {
    public string namePerk;
    public string icon;
    public string description;
    public int cost;
    public int level = 0;
    public float effectArea;
    public float extraDamage;
    public float duration;
    public bool automatic;
    public enum SpecialEffect{ Shotgun, Rifle, MachineGun, Dynamite, EagleEyes, Booze, DuckCover, GoldRush };
    public SpecialEffect specialEffect;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
