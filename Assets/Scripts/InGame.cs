using UnityEngine;
using System.Collections;

public class InGame : MonoBehaviour {
    PreloadedVariables preloadedVariables;
    public Hero hero;
    public int currentPrize;
    public int maxEnemies = 8;
    public int enemiesDefeated;
    public float initialTime;
    public float timeBetweenEnemies;
    float currentTimeBetweenEnemies;
    public GameObject[] enemies;
    public ArrayList enemiesInScreen = new ArrayList();
    public ArrayList powerUpsInScreen = new ArrayList();
    public ArrayList powerUpsActive = new ArrayList();
    public ArrayList bulletsInScreen = new ArrayList();
    public enum GameState { Begin, Pause, Normal, Finish }
    public GameState gameState;
    public ArrayList points;
    // Use this for initialization
    void Start () {
        points = new ArrayList();
        GameObject[] p = GameObject.FindGameObjectsWithTag("Point");
        foreach(GameObject g in p)
        {
            points.Add(g);
        }
        GameObject h = (GameObject)Instantiate(hero.gameObject);
        hero = h.GetComponent<Hero>();
        hero.setCurrentValues();
        currentTimeBetweenEnemies = Time.time + timeBetweenEnemies * 0.2f;
    }

    public Vector3 getPointPosition(Point.PointPosition pointPosition)
    {
        for(int i = 0; i < points.Count; i++)
        {
            if (((GameObject)points[i]).GetComponent<Point>().pointPosition == pointPosition)
                return ((GameObject)points[i]).transform.position;
        }
        return Vector3.zero;
    }

    void ChangeState(GameState state)
    {

    }

    public void EnemyEliminated(Enemy enemy, bool alive)
    {
        enemiesInScreen.Remove(enemy);
    }

    public void CreateEnemy(Enemy.DifficultLevel difficult)
    {
        if (enemiesInScreen.Count > maxEnemies)
            return;
        ArrayList enemiesInDifficulty = new ArrayList();
        for(int i = 0; i < enemies.Length; i++)
        {
            if(enemies[i].GetComponent<Enemy>().difficultLevel == difficult)
            {
                enemiesInDifficulty.Add(enemies[i]);
            }
        }
        print(enemiesInDifficulty.Count);
        GameObject currentEnemy = null;
        if (enemiesInDifficulty.Count > 0)
            currentEnemy = (GameObject)enemiesInDifficulty[Random.Range(0, enemiesInDifficulty.Count)];
        else
            currentEnemy = enemies[Random.Range(0, enemies.Length)];
        GameObject g = (GameObject)Instantiate(currentEnemy);
        Enemy e = g.GetComponent<Enemy>();
        enemiesInScreen.Add(e);
        print(enemiesInScreen.Count);
    }

    void OnTriggerEnter(Collider other)
    {
        EnemyBullet b = other.GetComponent<EnemyBullet>();
        if (b != null)
        {
            hero.GetHit(b.attackPower);
            Destroy(other.gameObject);
        }
    }

    void makeDamage(RaycastHit hit)
    {
        Enemy e = hit.collider.GetComponent<Enemy>();
        if (e != null)
        {
            e.makeDamage(Mathf.FloorToInt(hero.attackPower));
        }
        else
        {
            EnemyBullet eb = hit.collider.GetComponent<EnemyBullet>();
            if (eb != null)
            {
                eb.makeDamage(Mathf.FloorToInt(hero.attackPower));
            }
        }
    }

        // Update is called once per frame
    void Update () {
        //Spawning
        if (Time.time > currentTimeBetweenEnemies)
        {
            currentTimeBetweenEnemies = Time.time + timeBetweenEnemies;
            timeBetweenEnemies = Mathf.Clamp(timeBetweenEnemies - timeBetweenEnemies * Random.Range(0.01f, 0.05f), 1f, 100f);
            CreateEnemy(Enemy.DifficultLevel.VeryEasy);
        }

        //Shooting
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (hero.activePerk != null && hero.activePerk.specialEffect == Perk.SpecialEffect.Shotgun)
            {
                RaycastHit[] hit = Physics.SphereCastAll(ray, hero.activePerk.effectArea, 100f, LayerMask.GetMask(new string[2] { "Enemy", "EnemyBullet" }));
                for (int i = 0; i < hit.Length; i++)
                {
                    makeDamage(hit[i]);
                }
            }
            else
            {
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask(new string[2] { "Enemy", "EnemyBullet" })))
                {
                    makeDamage(hit);
                }
            }
        }
    }
}
