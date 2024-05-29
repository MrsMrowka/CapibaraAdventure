using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    [SerializeField] bool leftSide;
    [SerializeField] EnemyBullet bullet;
    [SerializeField] float shootingSpeed;
    [SerializeField] Vector2 offset;

    void Start()
    {
        InvokeRepeating(nameof(ShootBullet), shootingSpeed, shootingSpeed);
    }

    void ShootBullet()
    {
        Vector3 position = new Vector3(offset.x, offset.y, transform.position.z);
        EnemyBullet enemyBullet = Instantiate(bullet, transform.position + position, Quaternion.identity);
        if (leftSide)
        {
            enemyBullet.direction = -1f;
        }
        else
        {
            enemyBullet.direction = 1f;
        }
    }
}
