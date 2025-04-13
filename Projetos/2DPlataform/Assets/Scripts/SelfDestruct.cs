using System.Collections;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [Tooltip("Tempo até o objeto ser destruído automaticamente")]
    [SerializeField] private float timeToLive = 2f;

    private void Start()
    {
        // Inicia a contagem regressiva para a destruição
        StartCoroutine(nameof(Destruct), timeToLive);
    }

    private IEnumerator Destruct()
    {
        // Destrói o objeto
        yield return new WaitForSeconds(timeToLive);
        ObjectPooling.Instance.ReturnProjectile(gameObject);
    }
}
