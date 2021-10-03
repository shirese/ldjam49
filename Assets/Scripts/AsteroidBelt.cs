using UnityEngine;

public class AsteroidBelt : MonoBehaviour
{
    public float radius;
    public int number;
    public Vector2 meshScale;
    public GameObject prefab;

    void Start()
    {
        SpawnAsteroids();
    }

    void SpawnAsteroids()
    {
        for (int i = 0; i < number; i++)
        {
            Vector3 pos = Random.insideUnitCircle.normalized * radius;
            Vector3 rand = Random.insideUnitSphere * radius * 0.05f;
            pos += rand;

            GameObject o = Instantiate(prefab, this.transform);
            // new GameObject("asteroid_" + i);
            // o.transform.SetParent(this.transform);
            o.transform.localPosition = pos;

            o.transform.localScale = Vector3.one * Random.Range(meshScale.x, meshScale.y);
            o.transform.rotation = Random.rotation;
            if (o.TryGetComponent<Rigidbody>(out Rigidbody r))
            {
                r.velocity = rand;
            }
        }
    }
}
