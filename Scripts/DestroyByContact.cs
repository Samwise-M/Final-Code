using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explotion;
    public GameObject playerexplotion;
    public int scoreValue;
    public GameController gameController;

    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameControllerObject == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("boundry") || other.CompareTag("Enemy") || other.CompareTag("Pickup"))
        {
            return;
        }
        if (explotion != null)
        {
            Instantiate(explotion, transform.position, transform.rotation);
        }
        if (other.tag == "Player")
        {
            Instantiate(playerexplotion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
        }
        gameController.AddScore(scoreValue);
        Destroy(gameObject);
        Destroy(other.gameObject);
    }
}
