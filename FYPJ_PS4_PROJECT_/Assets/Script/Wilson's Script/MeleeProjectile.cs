using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeProjectile : MonoBehaviour
{
    private Transform target;
    private Transform UnitThatShoots;
    public float AnimationSpeed;
    float speed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        AnimationSpeed = this.gameObject.GetComponent<Minion>().attackSpeedValue;
        UnitThatShoots = this.transform;
        //AttackSpeed = this.gameObject.GetComponent<Minion>().attackSpeedValue;
    }

    // Update is called once per frame
    void Update()
    {
        //target.position - 
        float dist_between = (UnitThatShoots.position - target.position).magnitude;

        speed = dist_between / AnimationSpeed;

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

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void HitTarget()
    {
        Destroy(gameObject);
    }
}
