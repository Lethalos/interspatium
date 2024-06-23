using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SlashRotate : MonoBehaviour
{
    VisualEffect vfx;
    SphereCollider col;

    public float damage;
    public float lifeTime;
   
    // Start is called before the first frame update
    void Start()
    {
        vfx = GetComponent<VisualEffect>();
        col = GetComponent<SphereCollider>();
        Invoke("ColliderActive", vfx.GetFloat("DelaySlash"));
        Destroy(gameObject, lifeTime);
    }

   private void ColliderActive()
    {
        col.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent<EnemyBehavior>(out EnemyBehavior enemyBehavior))
            {
                enemyBehavior.KillEnemy();
            }
            else if (other.TryGetComponent<SerpmareBehaviour>(out SerpmareBehaviour serpmareBehaviour))
            {
                serpmareBehaviour.KillEnemy();
            }
        }
    }
}
