using UnityEngine;

/// <summary>
/// PlayerShoot module responsible for spawning bullets and directing them forward relative to the spaceship
/// </summary>
public class PlayerShoot : MonoBehaviour {
    
    [Header("References")]
    [SerializeField] private Transform muzzle;
    [SerializeField] private GameObject bullet;

    [Header("Settings")]
    [SerializeField] public float bulletForce;
    [SerializeField] public float fireRate;

    private float timeSinceLastShot;

    /// <summary>
    /// CanShoot method checks when the player can shoot
    /// </summary>
    /// <returns>a bool based on if the player can shoot or not</returns>
    private bool CanShoot() => timeSinceLastShot > 1f / (fireRate / 60f);

    /// <summary>
    /// Update method responsible for shooting on user input and incrementing timeSinceLastShot
    /// </summary>
    private void Update() {
        if (Input.GetKey(KeyCode.Mouse0) && CanShoot()) {
            Shoot();
            timeSinceLastShot = 0f;
        }

        timeSinceLastShot += Time.deltaTime;
    }

    /// <summary>
    /// Shoot method shoots a bullet
    /// </summary>
    private void Shoot() {
        GameObject b = Instantiate(bullet, muzzle.position, muzzle.rotation);
        b.GetComponent<Rigidbody2D>().AddForce(muzzle.up * bulletForce, ForceMode2D.Impulse);
    }
}
