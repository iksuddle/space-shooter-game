using UnityEngine;

public class PlayerShoot : MonoBehaviour {
    
    [Header("References")]
    [SerializeField] private Transform muzzle;
    [SerializeField] private GameObject bullet;

    [Header("Settings")]
    [SerializeField] private float bulletForce;
    [SerializeField] private float fireRate;

    private float timeSinceLastShot;

    private bool CanShoot() => timeSinceLastShot > 1f / (fireRate / 60f);

    private void Update() {
        if (Input.GetKey(KeyCode.Mouse0) && CanShoot()) {
            Shoot();
            timeSinceLastShot = 0f;
        }

        timeSinceLastShot += Time.deltaTime;
    }

    private void Shoot() {
        GameObject b = Instantiate(bullet, muzzle.position, muzzle.rotation);
        b.GetComponent<Rigidbody2D>().AddForce(muzzle.up * bulletForce, ForceMode2D.Impulse);
    }
}
