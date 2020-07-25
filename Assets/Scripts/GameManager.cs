using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Chunks")]
    [SerializeField] GameObject chunks;
    [SerializeField] GameObject chunk;
    [SerializeField] Color[] blockColors;
    [SerializeField] Color[] additBlockColors;
    [Header("UI")]
    [SerializeField] public Text moneyTextPublic;
    public static Text moneyText;

    private int chunkId = 0;

    void Awake()
    {
        GenerateWorld();
        moneyText = moneyTextPublic;
        SaveScript.money = PlayerPrefs.GetInt("money");
        UpdateMoney(0);
    }
    private void GenerateWorld()
    {
        // main blocks
        GenerationBlock grassBlock = new GenerationBlock(1, 1, 1, 1, 30f, 46f, 1, blockColors[0]);
        GenerationBlock dirtBlock = new GenerationBlock(2, 2, 6, 6, 50f, 82f, 1, blockColors[1]);
        GenerationBlock sandStripBlock = new GenerationBlock(7, 7, 7, 7, 27f, 36f, 5, blockColors[2]);
        GenerationBlock dryMudBlock = new GenerationBlock(8, 8, 20, 30, 68f, 92f, 1, blockColors[3]);
        GenerationBlock sandBlock = new GenerationBlock(20, 31, 50, 62, 20f, 33f, 2, blockColors[4]);
        GenerationBlock sandStoneBlock = new GenerationBlock(50, 63, 120, 120, 100f, 136f, 3, blockColors[5]);

        // additional blocks
        GenerationBlock blackEarthBlock = new GenerationBlock(2, 3, 0.05f, 40f, 70f, 5, additBlockColors[0]);
        GenerationBlock clayBlock = new GenerationBlock(2, 4, 0.1f, 60f, 82f, 6, additBlockColors[1]);
        GenerationBlock mudBlock = new GenerationBlock(2, 6, 30f, 40f, 2, additBlockColors[2]);

        // plug block
        GenerationBlock plugBlock = new GenerationBlock(121, int.MaxValue, 1f, 1f, 1f, 0, Color.red);

        SaveScript.blocks = new GenerationBlock[] { blackEarthBlock, clayBlock, mudBlock, grassBlock, dirtBlock, sandStripBlock, dryMudBlock, sandBlock, sandStoneBlock, plugBlock };
    }
    public static bool UpdateMoney(int value)
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