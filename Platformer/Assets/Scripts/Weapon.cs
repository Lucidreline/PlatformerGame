using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    Transform firePoint; //the tip of gun

    [Header("Bullet")]
    [SerializeField] float fireRate = 0;
    [SerializeField] int damage = 10;
    float timeToFire = 0;

    [Header("Bullet Effect")]
    [SerializeField] Transform BulletTrailPrefab;
    [SerializeField] float bulletTrailSpawnRate = 10;
    [SerializeField] Transform hitPrefab;
    float timeToSpawnTrail = 0;

    [SerializeField] Transform muzzleFlashPreFab;

    [Header("Other")]
    [SerializeField] LayerMask whatToHit;
    
    void Awake() {
        firePoint = transform.Find("FirePoint");
        if (firePoint == null)
            Debug.LogError("WHERES MI FIREPOINT EH?");
    }

    void Update()
    {
        if(fireRate == 0) {
            if (Input.GetButtonDown("Fire1")) 
                Shoot();
        }
        else {
            if (Input.GetButton("Fire1") && Time.time > timeToFire) {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
    }

    void Shoot() {
        Vector2 mousePosition     = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition =  new Vector2(firePoint.transform.position.x, firePoint.transform.position.y);

        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatToHit);
 
        Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition) * 100, Color.cyan);

        if(hit.collider != null) {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);

            
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if(enemy != null) {
                enemy.DamageEnemy(damage);
                Debug.Log("We Hit: " + hit.collider.name + ", And did " + damage + " damage!");
            }
        }
        if (Time.time > timeToSpawnTrail) {
            Vector3 hitPos;
            Vector3 hitNormal;

            if(hit.collider == null) {
                //if we dont hit anything
                hitPos = (mousePosition - firePointPosition) * 30;
                hitNormal = new Vector3(9999, 9999, 9999);
            }
            else {
                hitPos = hit.point;
                hitNormal = hit.normal;
            }
            Effect(hitPos, hitNormal);
            timeToSpawnTrail = Time.time + 1 / bulletTrailSpawnRate;
        }
    }

    void Effect(Vector3 hitPos, Vector3 hitNormal) {
        Transform trail = (Transform) Instantiate(BulletTrailPrefab, firePoint.position, firePoint.rotation);
        LineRenderer lr = trail.GetComponent<LineRenderer>();

        if(lr != null) {
            lr.SetPosition(0, firePoint.position);
            lr.SetPosition(1, hitPos);
        }
        Destroy(trail.gameObject, 0.04f);
        if(hitNormal != new Vector3(9999, 9999, 9999)) {
            Transform impactClone = Instantiate(hitPrefab, hitPos, Quaternion.FromToRotation(Vector3.right, hitNormal));
            Destroy(impactClone.gameObject, 2f);
        }

        Transform clone = (Transform)Instantiate(muzzleFlashPreFab, firePoint.position, firePoint.rotation);
        clone.parent = firePoint;
        float size = Random.Range(0.6f, 0.9f);
        clone.localScale = new Vector3(size, size, size);
        Destroy(clone.gameObject, 0.02f);
    }

}
