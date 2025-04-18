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

    private void OnEnable()
    {
        // Reinicia a contagem regressiva quando o objeto é ativado
        StopCoroutine(nameof(Destruct));
        StartCoroutine(nameof(Destruct), timeToLive);
    }
    private void OnDisable()
    {
        // Para a contagem regressiva quando o objeto é desativado
        StopCoroutine(nameof(Destruct));
    }

    private IEnumerator Destruct()
    {
        // Destrói o objeto
        yield return new WaitForSeconds(timeToLive);
        ObjectPooling.Instance.ReturnProjectile(gameObject);
    }
}
