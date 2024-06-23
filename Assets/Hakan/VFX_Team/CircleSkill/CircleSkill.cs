using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class CircleSkill : MonoBehaviour
{
    public float damage;
    public int damageIndex = 5;
    public float timeStack;
    public float lifetime;
    SphereCollider sphereCollider;
    VisualEffect vfx;


    // Start is called before the first frame update
    void Start()
    {
        vfx = GetComponent<VisualEffect>();
        sphereCollider = GetComponent<SphereCollider>();
        StartCoroutine(ColliderSpawn());
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator ColliderSpawn()
    {
        yield return new WaitForSeconds(vfx.GetFloat("DelayDS"));
        for (int i = 0; i < damageIndex; i++)
        {
            sphereCollider.enabled = true;
            yield return new WaitForSeconds(timeStack);
            sphereCollider.enabled = false;
            // yield return new WaitForSeconds(.05f);
        }
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
