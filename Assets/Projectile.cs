using UnityEngine;

public class Projectile: MonoBehaviour
{
  void OnCOllisionEntered2D(Collision2D collision)
  {
    Destroy(gameObject);
  }
}
