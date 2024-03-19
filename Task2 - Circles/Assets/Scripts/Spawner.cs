using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour {

    private Collider spawnArea;

    public GameObject fruitPrefabs;

    public float minSpawnDelay = 0.25f;

    public float maxSpawnDelay = 1f;

    public int max_count = 0;

    private void Awake() {
        spawnArea = GetComponent<Collider>();
    }

    private void OnEnable() {
        StartCoroutine(Spawn());
    }

    private void OnDisable() {
        StopAllCoroutines();
    }

    private IEnumerator Spawn() {
        yield return new WaitForSeconds(2f);

        while (enabled) {
            GameObject prefab = fruitPrefabs;

            Vector3 position = new Vector3();
            position.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            position.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            position.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);


            if (max_count < 6) {
                GameObject fruit = Instantiate(prefab, position, Quaternion.identity);
                max_count++;
            }

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));

        }
    }

}
