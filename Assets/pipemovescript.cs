using UnityEngine;

public class pipemovescript : MonoBehaviour
{
    public float moveSpeed = 5;
    public float deadZone = -45;
    private logicscript logic;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<logicscript>();
    }
        // Update is called once per frame
        void Update()
    {
        if (!logic.gameStarted) return; 

        float currentSpeed = logic.gameSpeed; // Get the current game speed from the logic script
        transform.position += Vector3.left * currentSpeed * Time.deltaTime;

        if (transform.position.x < deadZone)
        {
            Debug.Log("Pipe Destroyed");
            Destroy(gameObject);
        }
    }
}
