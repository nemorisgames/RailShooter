using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {
    public string nameBullet;
    public float hitPoints = 1f;
    public float speed = 10f;
    public float attackPower = 1f;
	// Use this for initialization
	void Start () {
        transform.LookAt(Camera.main.transform.position + Camera.main.transform.forward * 2.5f + new Vector3(Random.Range(-1, 2) *  0.5f, Random.Range(-1, 2) * 0.2f, 0f));
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
        Destroy(gameObject, 10f);
    }

    public void makeDamage(int damage)
    {
        hitPoints-= damage;
        if (hitPoints <= 0)
            Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
