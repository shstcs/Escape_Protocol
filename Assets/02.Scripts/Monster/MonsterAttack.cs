using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    [SerializeField] private Collider myCollider;

    private List<Collider> myColliders = new List<Collider>();

    private void OnEnable()
    {
        myColliders.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) return;
        if(myColliders.Contains(other)) return;

        myColliders.Add(other);

        if(other.TryGetComponent(out PlayerHealth _health))
        {
            _health.TakeDamage(50);
            Debug.Log(_health.ToString());
        }
        if(other.TryGetComponent(out ForceReceiver _forceReceiver))
        {
            Vector3 direction = (other.transform.position - myCollider.transform.position).normalized;
            _forceReceiver.AddForce(direction*2f);
        }
    }
}
