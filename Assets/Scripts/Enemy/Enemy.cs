using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public float hitPoints = 5f;
    public float enemyHeight = 2f;
    public enum DifficultLevel { VeryEasy, Easy, Normal, Hard, VeryHard};
    public DifficultLevel difficultLevel;
    public enum AttackTypes{ Melee, Ranged, Jumping };
    public float meleeDamage = 1f;
    public AttackTypes attackType;
    public float attackTimer;
    public float attackTimeRandomizer;
    float currentAttackTimer;
    public float movementSpeed;
    public EnemyType enemyType;
    public EnemyBullet[] bullet;
    public int prizeCurrency;
    public float powerUpDropRate; //(0-100)
    public float timeSpawn = 1f;
    public float timeInPoint = 3f;
    float currentTimeInPoint;
    bool enabledToShoot = false;
    public InGame inGame;
    public Pattern[] patterns;
    public Pattern currentPattern;
    public int currentPatternIndex = 0;
    public enum AttackPattern{ None, Once, Loop };
    public AttackPattern attackPattern;

    public enum EnemyState{Spawning, Moving, Dying, GettingOut};
    public EnemyState enemyState = EnemyState.Spawning;
    TweenPosition tp;
    // Use this for initialization
    void Start() {
        tp = GetComponent<TweenPosition>();
        inGame = Camera.main.GetComponent<InGame>();
        currentPattern = patterns[Random.Range(0, patterns.Length)];
        print(patterns.Length + " " + patterns[0].pointPositions.Length);
        Vector3 initialPosition = inGame.getPointPosition(currentPattern.pointPositions[0]);
        transform.position = initialPosition + new Vector3(0f, -enemyHeight, 0f);
        setDestiny(initialPosition, timeSpawn);
        //tp.PlayForward();
    }

    void setDestiny(Vector3 destiny, float time)
    {
        tp.from = transform.position;
        tp.to = destiny;
        tp.duration = time;
        tp.ResetToBeginning();
        tp.PlayForward();
    }

    public float getTimeUsingDistance(Vector3 initialPosition, Vector3 endPosition, float speed)
    {
        float dist = Vector3.Distance(initialPosition, endPosition);
        return dist / speed;
    } 

    public void inDestiny()
    {
        switch (enemyState)
        {
            case EnemyState.Spawning:
                enabledToShoot = true;
                enemyState = EnemyState.Moving;
                currentAttackTimer = Time.time + attackTimer + attackTimeRandomizer * Random.Range(-1f, 1f);
                StartCoroutine(setNextDestiny());
                break;
            case EnemyState.Moving:
                StartCoroutine(setNextDestiny());
                break;
            case EnemyState.GettingOut:
                inGame.EnemyEliminated(this, true);
                Destroy(gameObject);
                break;
        }
    }

    public void makeDamage(int damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            inGame.EnemyEliminated(this, false);
            Destroy(gameObject);
        }
    }

    IEnumerator setNextDestiny()
    {
        yield return new WaitForSeconds(timeInPoint);
        currentPatternIndex++;
        if (currentPatternIndex >= currentPattern.pointPositions.Length)
        {
            enemyState = EnemyState.GettingOut;
            setDestiny(transform.position + new Vector3(0f, -enemyHeight, 0f), timeSpawn);
        }
        else
        {
            Vector3 destiny = inGame.getPointPosition(currentPattern.pointPositions[currentPatternIndex]);
            switch (attackType)
            {
                case AttackTypes.Jumping:
                    if(currentPatternIndex == 1)
                        destiny += Vector3.up * 3f;
                    break;
                case AttackTypes.Melee:
                    if (currentPatternIndex == 1)
                        destiny += Vector3.up * 3f;
                    break;
                default:
                    break;
            }
            setDestiny(destiny, getTimeUsingDistance(transform.position, destiny, movementSpeed));
        }
    }

    public void GetHit(float attackPower)
    {

    }
	
    public void Death()
    {

    }

    public void Shoot()
    {

    }

	// Update is called once per frame
	void Update () {
        switch (enemyState)
        {
            case EnemyState.Moving:
                if(attackPattern != AttackPattern.None && Time.time >= currentAttackTimer)
                {
                    if (bullet.Length > 0)
                        Instantiate(bullet[Random.Range(0, bullet.Length - 1)].gameObject, transform.position + transform.forward, transform.rotation);
                    else
                    {
                        print("Melee damage");
                        inGame.hero.GetHit(meleeDamage);
                    }
                    if (attackPattern == AttackPattern.Once)
                        attackPattern = AttackPattern.None;
                    else
                        currentAttackTimer = Time.time + attackTimer + attackTimeRandomizer * Random.Range(-1f, 1f);
                }
                break;
        }
	}
}
