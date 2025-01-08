using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CollactableBase : MonoBehaviour
{
    public string compareTagPlayer = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(compareTagPlayer))
        {
            Collect();
        }
    }

    protected virtual void Collect()
    {
        Debug.Log("Collected");
        gameObject.SetActive(false);
        OnCollect();
    }

    protected virtual void OnCollect()
    {
    }

}
