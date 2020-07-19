﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject blockCollider;
    [SerializeField] Color[] blockColors;
    void Start()
    {
        StartCoroutine("GenerateWorld");
    }
    IEnumerator GenerateWorld ()
	{
        GenerationBlock blockClay = new GenerationBlock(2, 6, 0.04f, blockColors[0]);
        GenerationBlock blockCoal = new GenerationBlock(11, 500, 0.02f, blockColors[1]);
        GenerationBlock blockDirt = new GenerationBlock(2, 2, 6, 8, blockColors[2]);
        GenerationBlock blockStone = new GenerationBlock(6, 11, 500, 560, blockColors[3]);
        GenerationBlock blockGrass = new GenerationBlock(1, 1, 1, 1, blockColors[4]);
        SaveScript.blocks = new GenerationBlock[] { blockClay, blockCoal, blockStone, blockGrass, blockDirt };
		for (float y = 0; y >= -512f; y -= 10.24f)
		{
			for (float x = -64f; x <= 64f; x += 10.24f)
			{
				GameObject instBlock = Instantiate(blockCollider, new Vector3(x, y, 0), Quaternion.identity);
			}
			yield return new WaitForSeconds(0.1f);
		}
		/*yield return new WaitForSeconds(0.1f);
        GameObject instBlock = Instantiate(blockCollider, new Vector3(0, 0, 0), Quaternion.identity);*/
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

    public Color color;

    public GenerationBlock(int minDepth, int minGuaranteedDepth, int maxGuaranteedDepth, int maxDepth, Color color)
    {
        this.minDepth = minDepth;
        this.minGuaranteedDepth = minGuaranteedDepth;
        this.maxGuaranteedDepth = maxGuaranteedDepth;
        this.maxDepth = maxDepth;
        this.color = color;
        topChanceCoof = 1f / (minGuaranteedDepth - minDepth + 1);
        bottomChanceCoof = 1f / (maxDepth - maxGuaranteedDepth + 1);
    }

    public GenerationBlock(int minDepth, int maxDepth, Color color)
    {
        this.minDepth = minDepth;
        this.maxDepth = maxDepth;
        this.color = color;
        chance = 1f / (maxDepth - minDepth + 1);
    }

    public GenerationBlock(int minDepth, int maxDepth, float chance, Color color)
    {
        this.minDepth = minDepth;
        this.maxDepth = maxDepth;
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