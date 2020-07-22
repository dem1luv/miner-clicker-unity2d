using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Blocks")]
    [SerializeField] GameObject blockCollider;
    [SerializeField] Color[] blockColors;
    [Header("UI")]
    [SerializeField] public Text moneyTextPublic;
    public static Text moneyText;

    private int chunkId = 0;

    void Start()
    {
        StartCoroutine("GenerateWorld");
        moneyText = moneyTextPublic;
        SaveScript.money = PlayerPrefs.GetInt("money");
        ChangeMoney(0);
    }

    IEnumerator GenerateWorld ()
	{
        GenerationBlock blockClay = new GenerationBlock(2, 6, 0.04f, 16f, 20f, 6, blockColors[0]);
        GenerationBlock blockCoal = new GenerationBlock(11, 500, 0.02f, 50f, 80f, 30, blockColors[1]);
        GenerationBlock blockDirt = new GenerationBlock(2, 2, 6, 8, 20f, 24f, 1, blockColors[2]);
        GenerationBlock blockStone = new GenerationBlock(6, 11, 20000, 20000, 30f, 50f, 1, blockColors[3]);
        GenerationBlock blockGrass = new GenerationBlock(1, 1, 1, 1, 8f, 15f, 1, blockColors[4]);
        SaveScript.blocks = new GenerationBlock[] { blockClay, blockCoal, blockStone, blockGrass, blockDirt };
		for (float y = 0; y >= -2048f; y -= 10.24f)
		{
			for (float x = -10.24f; x <= 0; x += 10.24f)
			{
				GameObject instChunk = Instantiate(blockCollider, new Vector3(x, y, 0), Quaternion.identity);
                BlockCollider collider = instChunk.GetComponent<BlockCollider>();
                collider.chunkId = chunkId;
                chunkId++;
                collider.StartCoroutine("ManualStart");
            }
			yield return new WaitForEndOfFrame();
		}
		/*yield return new WaitForSeconds(0.1f);
        GameObject instBlock = Instantiate(blockCollider, new Vector3(0, 0, 0), Quaternion.identity);*/
    }

    public static bool ChangeMoney(int value)
	{
        if (SaveScript.money + value < 0)
        {
            return false;
        }
        else
        {
            SaveScript.money += value;
            moneyText.text = SaveScript.money.ToString();
            PlayerPrefs.SetInt("money", SaveScript.money);
            return true;
        }
    }

    public void OnDeleteAllData()
	{
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

public class GenerationBlock
{
    private int minDepth;
    private int minGuaranteedDepth = -1;
    private int maxGuaranteedDepth = -1;
    private int maxDepth;
    private float topChanceCoof;
    private float bottomChanceCoof;
    private float chance;

    public float minStrength;
    public float maxStrength;
    public int money;
    public Color color;

    public GenerationBlock(int minDepth, int minGuaranteedDepth, int maxGuaranteedDepth, int maxDepth, float minStrength, float maxStrength, int money, Color color)
    {
        this.minDepth = minDepth;
        this.minGuaranteedDepth = minGuaranteedDepth;
        this.maxGuaranteedDepth = maxGuaranteedDepth;
        this.maxDepth = maxDepth;
        this.minStrength = minStrength;
        this.maxStrength = maxStrength;
        this.money = money;
        this.color = color;
        topChanceCoof = 1f / (minGuaranteedDepth - minDepth + 1);
        bottomChanceCoof = 1f / (maxDepth - maxGuaranteedDepth + 1);
    }

    public GenerationBlock(int minDepth, int maxDepth, float minStrength, float maxStrength, int money, Color color)
    {
        this.minDepth = minDepth;
        this.maxDepth = maxDepth;
        this.minStrength = minStrength;
        this.maxStrength = maxStrength;
        this.money = money;
        this.color = color;
        chance = 1f / (maxDepth - minDepth + 1);
    }

    public GenerationBlock(int minDepth, int maxDepth, float chance, float minStrength, float maxStrength, int money, Color color)
    {
        this.minDepth = minDepth;
        this.maxDepth = maxDepth;
        this.minStrength = minStrength;
        this.maxStrength = maxStrength;
        this.money = money;
        this.color = color;
        this.chance = chance;
    }

    public float GetGenerationChance(int depth)
    {
        if (minDepth > depth || maxDepth < depth)
        {
            return 0;
        }
        else if (minGuaranteedDepth <= depth && maxGuaranteedDepth >= depth)
        {
            return 1;
        }
        else if (minGuaranteedDepth == -1 && maxGuaranteedDepth == -1)
        {
            return chance;
        }
        else if (minDepth <= depth && minGuaranteedDepth > depth)
        {
            return topChanceCoof * (depth - minDepth + 1);
        }
        else
        {
            return bottomChanceCoof * (maxDepth - depth + 1);
        }
    }
}