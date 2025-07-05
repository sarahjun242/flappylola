using UnityEngine;

public class backgroundscroll : MonoBehaviour
{

    public float scrollSpeed = 2f; // Speed of the background scroll
    public float resetPositionX; //where to reset when it moves off screen 
    public float startPositionX; //where to start the background

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

        transform.position += Vector3.left * scrollSpeed * Time.deltaTime;

        if (transform.position.x <= resetPositionX)
        {
            // Reset the position of the background to the start position
            transform.position = new Vector3(startPositionX, transform.position.y, transform.position.z);
        }
    }
}
