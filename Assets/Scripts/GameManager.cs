using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject blockCollider;
    void Start()
    {
        StartCoroutine("GenerateWorld");
    }
    IEnumerator GenerateWorld ()
	{
        for (float y = 0; y >= -512f; y -= 10.24f)
        {
            for (float x = -64f; x <= 64f; x += 10.24f)
            {
                GameObject instBlock = Instantiate(blockCollider, new Vector3(x, y, 0), Quaternion.identity);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
