using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GunType
{
    None,
    M4,
    M82,
}

public class GunItem : MonoBehaviour
{
    public GunType GunType;

    private float _rotationSpeed = 50f;

    void Update()
    {
        transform.eulerAngles += new Vector3(0, _rotationSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("GunItem 획득");
            switch(GunType)
            {
                case GunType.None:
                    break;

                case GunType.M4:
                    collision.gameObject.GetComponent<Player>().GunController.EquipM4();
                    break;

                case GunType.M82:
                    break;

                default:
                    break;
            }

            Destroy(gameObject);
        }
    }
}
