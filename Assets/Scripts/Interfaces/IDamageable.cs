/// <summary>
/// Objects that implement IDamageable can be shot down by the player
/// </summary>
public interface IDamageable {
    /// <summary>
    /// TakeDamage method damages the IDamageable
    /// </summary>
    /// <param name="damage">the amount of damage as a float</param>
    public void TakeDamage(float damage);
}
