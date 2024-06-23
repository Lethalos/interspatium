using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class HorizontalSkill : MonoBehaviour
{
    //public GameObject vfxGameobject;
    //BoxCollider boxCol;
    //VisualEffect vfx;
    //public float damage;
    //public float speed;
    //public float lifeTime;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    // vfx = GetComponent<VisualEffect>();
    //    //  col = GetComponent<BoxCollider>();
    //    //  vfx = transform.GetComponentInParent<VisualEffect>();
    //    boxCol = GetComponent<BoxCollider>();
    //    vfx = vfxGameobject.GetComponent<VisualEffect>();

    //    Destroy(vfxGameobject, lifeTime);

    //}

    //private void MovieCollider()
    //{
    //    boxCol.enabled = true;
    //    transform.Translate(Vector3.forward * speed * vfx.GetFloat("SpeedLine") * Time.deltaTime);
    //}
    //// Update is called once per frame
    //void Update()
    //{

    //    Invoke("MovieCollider", vfx.GetFloat("Delay"));
    //    if (Vector3.Distance(transform.position, vfx.GetVector3("LinePositionEnd")) < 0.2f)
    //    {
    //        Destroy(gameObject);
    //    }
    //}

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
