using UnityEngine;

/// <summary>
/// Responsible for destroying particles after spawnings
/// </summary>
public class ParticleFX : MonoBehaviour {
    
    /// <summary>
    /// Start method responsible for destroying the particle after half a second
    /// </summary>
    private void Start() => Destroy(gameObject, 0.5f);
}
