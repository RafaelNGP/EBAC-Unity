using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Vector3 dir;
    [SerializeField] float timeDestroy = 5f;

    public Action OnHitEnemy;
    public Action OnHitPlayer;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir * Time.deltaTime);
    }

    /*public void StartProjectile()
    {
        Invoke(nameof(FinishUsage), timeDestroy);
    }*/

    private void FinishUsage()
    {
        gameObject.SetActive(false);
        OnHitEnemy = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            collision.gameObject.SetActive(false);
            //Destroy(collision.gameObject);
            OnHitEnemy?.Invoke();
            FinishUsage();
        }

        if (collision.transform.CompareTag("Wall"))
        {
            FinishUsage();
        }

        if (collision.transform.CompareTag("Player"))
        {
            FinishUsage();
            OnHitPlayer?.Invoke();
        }
    }
}
