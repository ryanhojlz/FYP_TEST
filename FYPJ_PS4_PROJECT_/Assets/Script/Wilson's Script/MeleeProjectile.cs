using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeProjectile : MonoBehaviour
{
    private Transform target;
    private Transform UnitThatShoots;
    public float AnimationSpeed;
    float speed = 0f;
    float lifeTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
       
        //AttackSpeed = this.gameObject.GetComponent<Minion>().attackSpeedValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (!this)
            return;

        if (!UnitThatShoots)
            return;
        //if (lifeTime <= 0f && this)
        //{
        //    Debug.Log("Destroy projectile");
        //    Destroy(this.gameObject);
        //    return;//Stop updating after deleting
        //}
        float dist_between = (UnitThatShoots.position - target.position).magnitude;

        speed = (dist_between / AnimationSpeed);

        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        lifeTime -= Time.deltaTime;

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    public void Seek(Transform _target)
    {
        target = _target;
    }

    public void SetBase(Minion GO)
    {
        AnimationSpeed = GO.gameObject.GetComponent<Minion>().GetCountDownTimer();
        UnitThatShoots = GO.transform;
        lifeTime = AnimationSpeed;
    }

    void HitTarget()
    {
        Damage(target);
        Destroy(gameObject);
    }

    void Damage(Transform _unit)
    {
        Minion unit = _unit.GetComponent<Minion>();

        if (unit != null)
        {
            unit.TakeDamage(UnitThatShoots.GetComponent<Minion>().attackValue);
        }
    }
}
