using TMPro;
using UnityEngine;

public class PlayerNave : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject shootPoint;
    [SerializeField] Vector3 dir;

    [SerializeField] int score;
    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] PoolManager poolManager;
    [SerializeField] MeshRenderer mesh;
    [SerializeField] StateMachine stateMachine;

    public bool canMove = false;

    // Update is called once per frame
    void Update() 
    {
        if (!canMove) return;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            transform.Translate(dir * Time.deltaTime);
        } 
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) 
        { 
            transform.Translate(-dir * Time.deltaTime); 
        }

        if (Input.GetMouseButtonDown(0))
        {
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        var obj = poolManager.GetPoolGObjects();
        if (obj != null)
        {
            obj.SetActive(true);
            //obj.GetComponent<Projectile>().StartProjectile();
            obj.GetComponent<Projectile>().OnHitEnemy = ScoreUp;
            obj.transform.SetParent(null);
            obj.transform.position = shootPoint.transform.position;
        }
    }

    public void ScoreUp()
    {
        score++;
        Debug.Log("Score: "+score);
        scoreText.text = "Score: " + score;

        if (score >= 3) stateMachine.SwitchState(StateMachine.States.WIN);
    }

    public void ChangeColor(Color color)
    {
        mesh.material.color = color;
    }
}
