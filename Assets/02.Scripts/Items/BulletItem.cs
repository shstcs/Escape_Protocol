using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletItem : MonoBehaviour
{
    [SerializeField] private int _bulletCount;
    [SerializeField] private AudioClip _bulletSound;

    private float _rotationSpeed = 50f;

    void Update()
    {
        transform.eulerAngles += new Vector3(0, _rotationSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("Bullet 획득");
            collision.gameObject.GetComponent<Player>().GunController.PlaySE(_bulletSound);
            collision.gameObject.GetComponent<Player>().GunController.CurrentGun.CarryBulletCount += _bulletCount;
            Destroy(gameObject);
        }
    }

}
