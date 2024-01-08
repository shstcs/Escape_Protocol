using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFOV : MonoBehaviour
{
    [SerializeField] private LayerMask _targetMask;
    [SerializeField][Range(0f, 360f)] private float _viewAngle = 0f;
    [SerializeField] private float _viewDistance = 50f;
    private List<Collider> hitTargetList = new List<Collider>();


    private void Update()
    {
        View();
    }

    private Vector3 BoundaryAngle(float _angle)
    {
        _angle += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(_angle*Mathf.Deg2Rad), 0f, Mathf.Cos(_angle*Mathf.Deg2Rad));
    }

    private void View()
    {
        Vector3 leftBoundary = BoundaryAngle(-_viewAngle * 0.5f);
        Vector3 rightBoundary = BoundaryAngle(_viewAngle * 0.5f);

        Debug.DrawRay(transform.position + transform.up, leftBoundary, Color.red);
        Debug.DrawRay(transform.position + transform.up, rightBoundary, Color.red);

        Gizmos.DrawWireSphere(transform.position + Vector3.up * 0.5f, _viewDistance);

        hitTargetList.Clear();
        Collider[] targets = Physics.OverlapSphere(transform.position, _viewDistance, _targetMask);
        if (targets.Length == 0) return;
        foreach(Collider playerCollider in targets)
        {
            Vector3 targetPos = playerCollider.transform.position;
            Vector3 targetDir = (targetPos - transform.position).normalized;

        }

        for(int i = 0;i<targets.Length;i++)
        {
            Transform target = targets[i].transform;
            if(target.name == "Player")
            {
                Vector3 direction = (target.position - transform.position).normalized;  
                float angle = Vector3.Angle(direction, transform.forward);

                if(angle < _viewAngle * 0.5f)
                {
                    RaycastHit _hit;
                    if (Physics.Raycast(transform.position + transform.up, direction, out _hit, _viewDistance))
                    {
                        if (_hit.transform.name == "Player")
                        {
                            Debug.Log("성공");
                            Debug.DrawRay(transform.position + transform.up, direction, Color.blue);
                        }
                    }

                }
            }
        }
    }

}
