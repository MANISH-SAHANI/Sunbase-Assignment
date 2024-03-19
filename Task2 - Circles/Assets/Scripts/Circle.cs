using UnityEngine;

public class Circle : MonoBehaviour
{
    public int count;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            count = FindObjectOfType<GameManager>().GameOverCount++;
            if (count == 5) {
                FindObjectOfType<GameManager>().gameOver();
            }
        }
        CircleDes(other);
    }

    private void CircleDes(Collider other) {
        if (other.CompareTag("Player")) {
            Destroy(gameObject);
        }
    }
}
