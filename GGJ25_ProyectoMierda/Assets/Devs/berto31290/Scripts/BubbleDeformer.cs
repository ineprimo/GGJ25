using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class BubbleDeformer : MonoBehaviour
{
    private Mesh originalMesh;
    private Mesh deformingMesh;
    private Vector3[] originalVertices;
    private Vector3[] displacedVertices;
    private float timeOffset;

    [SerializeField] private float deformIntensity = 0.1f;
    [SerializeField] private float deformSpeed = 2f;

    void Start()
    {
        originalMesh = GetComponent<MeshFilter>().mesh;
        deformingMesh = Instantiate(originalMesh);
        GetComponent<MeshFilter>().mesh = deformingMesh;

        originalVertices = originalMesh.vertices;
        displacedVertices = new Vector3[originalVertices.Length];

        timeOffset = Random.Range(0f, 100f); // Variar la deformación en cada pompa
    }

    void Update()
    {
        for (int i = 0; i < originalVertices.Length; i++)
        {
            Vector3 offset = originalVertices[i].normalized * Mathf.Sin(Time.time * deformSpeed + timeOffset) * deformIntensity;
            displacedVertices[i] = originalVertices[i] + offset;
        }

        deformingMesh.vertices = displacedVertices;
        deformingMesh.RecalculateNormals();
    }
}
