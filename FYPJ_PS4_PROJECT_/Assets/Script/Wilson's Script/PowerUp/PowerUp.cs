using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    protected Minion minion_unit;

    // Start is called before the first frame update
    void Start()
    {
        minion_unit = gameObject.GetComponent<Minion>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        
    }
}
