using UnityEngine;

public class AsteroidBelt : MonoBehaviour
{
    public float radius;
    public int number;
    public Vector2 meshScale;
    public Mesh mesh;
    public Material mat;

    void Start()
    {
        SpawnAsteroids();
    }

    void SpawnAsteroids()
    {
        for (int i = 0; i < number; i++)
        {
            Vector3 pos = Random.insideUnitCircle.normalized * radius;
            pos += Random.insideUnitSphere * radius * 0.05f;

            GameObject o = new GameObject("asteroid_" + i);
            o.transform.SetParent(this.transform);
            o.transform.localPosition = pos;

            o.transform.localScale = Vector3.one * Random.Range(meshScale.x, meshScale.y);
            o.transform.rotation = Random.rotation;

            MeshFilter filter = o.AddComponent<MeshFilter>();
            MeshRenderer renderer = o.AddComponent<MeshRenderer>();
            renderer.material = mat;
            filter.mesh = mesh;
        }
    }
}
