using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI roundText;
    [SerializeField] private TextMeshProUGUI instructionText;
    [SerializeField] private GameObject deck;
    [SerializeField] private GameObject radialMenu;
    [SerializeField] private GameObject cardPrefab;

    [SerializeField] private GameObject panelTimer;
    [SerializeField] private Image timerBar;
    [SerializeField] private TextMeshProUGUI timerText;

    private int numberOfCards = 13;
    private float radius = 150f;

    private int chancesLeft = 2;
    private int actualRound = 1;
    private string cardOfTheRound;
    private int cardNumberOfTheRound;
    private bool isDeckClickable = true;
    private float roundTime = 10f;

    const string question = "Try to guess which card is this!";

    void Start()
    {
        CreateRadialMenu();

        // Inicializar textos
        roundText.text = "Round " + actualRound;
        instructionText.text = question;

        // Inicializar a primeira rodada manualmente
        cardOfTheRound = RandomizeCard();
        chancesLeft = 2;
    }

    public void ToggleRadialMenu()
    {
        bool isActive = !radialMenu.activeSelf;
        radialMenu.SetActive(isActive);
        isDeckClickable = !isActive; // Desativa a clicabilidade do Deck quando o menu radial está ativo
    }
    public bool IsDeckClickable()
    {
        return isDeckClickable;
    }
    void CreateRadialMenu()
    {
        string[] cardOptions = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };

        for (int i = 0; i < numberOfCards; i++)
        {
            float angle = (i * Mathf.PI * 2 / numberOfCards) + Mathf.PI / 2;
            Vector3 newPos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
            GameObject card = Instantiate(cardPrefab, radialMenu.transform);
            card.transform.localPosition = newPos;

            // Verificação de índice
            int reversedIndex = numberOfCards - 1 - i;
            if (reversedIndex >= 0 && reversedIndex < cardOptions.Length)
            {
                card.GetComponentInChildren<TextMeshProUGUI>().text = cardOptions[reversedIndex]; // Inverte a ordem aqui

                // Adicionar interação com cada carta
                string cardOption = cardOptions[reversedIndex]; // Captura a variável localmente para o listener
                card.AddComponent<Button>().onClick.AddListener(() => OnCardSelected(cardOption));
            }
            else
            {
                Debug.LogError("Índice fora dos limites: " + reversedIndex);
            }
        }
    }
    void OnCardSelected(string card)
    {
        Debug.Log("Carta selecionada: " + card);
        ToggleRadialMenu();
        CheckSelectedCard(card);
    }
    void CheckSelectedCard(string card)
    {
        if (card.Equals(cardOfTheRound))
        {
            instructionText.text = "Right! Now choose someone to drink";
            isDeckClickable = false; // Desativa a clicabilidade do Deck durante o tempo de espera
            StartCoroutine(StartNewRoundCountdown());
        }
        else
        {
            chancesLeft--;
            int lastChoice = ConvertStringToInt(card);
            instructionText.text = lastChoice > cardNumberOfTheRound ? "Wrong! Tip: It's a lower card" : "Wrong! Tip: It's a higher card";

            if (chancesLeft <= 0)
            {
                Debug.Log("O player deve beber");

                int result = Mathf.Abs(cardNumberOfTheRound - lastChoice);
                instructionText.text = result == 1 ? "Too bad! You need to drink " + result + " time" : "Too bad! You need to drink " + result + " times";
                isDeckClickable = false;
                StartCoroutine(StartNewRoundCountdown());
            }
        }
    }

    IEnumerator StartNewRoundCountdown()
    {
        panelTimer.SetActive(true);
        float timeLeft = roundTime;
        while (timeLeft > 0)
        {
            timerBar.fillAmount = timeLeft / roundTime;
            timerText.text = Mathf.Ceil(timeLeft).ToString();
            yield return new WaitForSeconds(1f);
            timeLeft--;
        }
        NewRound();
    }
    string RandomizeCard()
    {
        cardNumberOfTheRound = Random.Range(1, numberOfCards + 1);
        string resultado = ConvertIntToString(cardNumberOfTheRound);

        Debug.Log("Carta retirada: " + resultado);
        return resultado;
    }
    int ConvertStringToInt(string s)
    {
        switch (s)
        {
            case "A": return 1;
            case "J": return 11;
            case "Q": return 12;
            case "K": return 13;
            default:
                if (int.TryParse(s, out int number))
                {
                    return number;
                }
                return -1; // Valor padrão para strings inválidas
        }
    }
    string ConvertIntToString(int i)
    {
        switch (i)
        {
            case 1: return "A";
            case 11: return "J";
            case 12: return "Q";
            case 13: return "K";
            default: return i.ToString(); // Converte números de 2 a 10 para string
        }
    }
    void NewRound()
    {
        panelTimer.SetActive(false);
        cardOfTheRound = RandomizeCard();

        chancesLeft = 2;
        actualRound++;

        roundText.text = "Round " + actualRound;
        instructionText.text = question;

        isDeckClickable = true;
    }
}
