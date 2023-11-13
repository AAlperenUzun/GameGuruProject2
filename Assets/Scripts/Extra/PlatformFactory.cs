using UnityEngine;

public class PlatformFactory : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private GameObject fallingPlatformPrefab;

    public Transform CreatePlatform(Vector3 position, Quaternion rotation, Transform parent, bool isFalling = false)
    {
        GameObject prefabToUse = isFalling ? fallingPlatformPrefab : platformPrefab;
        return Instantiate(prefabToUse, position, rotation, parent).transform;
    }
}