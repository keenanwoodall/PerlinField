using UnityEngine;

public class PerlinField : MonoBehaviour
{
	[Range (1, 100)]
	public int size = 10;
	public float
		spacing = 1f,
		frequency = 1,
		magnitude = 1f,
		speed = 1f;
	public Vector2 offset;
	public bool roundHeight;

	[SerializeField]
	private Transform prefab;
	private Transform[,] instances;
	private Material[,] materialInstances;

	private float timeOffset;

	private void Start ()
	{
		instances = new Transform[size, size];
		materialInstances = new Material[size, size];
		for (int y = 0; y < size; y++)
		{
			for (int x = 0; x < size; x++)
			{
				var newInstance = Instantiate (prefab);
				newInstance.position = new Vector3 (x, 0, y) * spacing;

				instances[x, y] = newInstance;
				materialInstances[x, y] = newInstance.GetComponent<MeshRenderer> ().material;
			}
		}
	}

	private void Update ()
	{
		timeOffset += Time.deltaTime * speed;
		for (int y = 0; y < size; y++)
		{
			for (int x = 0; x < size; x++)
			{
				var instance = instances[x, y];
				var newPosition = new Vector3 (x * spacing, 0, y * spacing);
				var sampleX = (x + timeOffset + offset.x) * frequency;
				var sampleY = (y + timeOffset + offset.y) * frequency;
				var perlinValue = Mathf.PerlinNoise (sampleX, sampleY);
				newPosition.y = perlinValue * magnitude;
				if (roundHeight)
					newPosition.y = (int)newPosition.y;

				instance.position = newPosition;
			}
		}
	}
}