using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Image image;
    [SerializeField] Text title;
    [SerializeField] Text description;
    [SerializeField] Text price;

    [Header("Logic")]
    [SerializeField] int id = 0;
    [SerializeField] int type = 0;
    [SerializeField] int priceCount;
    [SerializeField] int increaseNum;
    [SerializeField] int increaseIteration;

    [Header("Design")]
    [SerializeField] Color availableColor;
    [SerializeField] Color notAvailableColor;

    private Image imageComp;
    private int lvl = 0;

	private void Start()
	{
        imageComp = GetComponent<Image>();
        RerenderUI();
	}

	private void Update()
	{
        if (SaveScript.money >= priceCount)
            imageComp.color = availableColor;
        else
            imageComp.color = notAvailableColor;
    }

    private void RerenderUI()
    {
        price.text = priceCount.ToString();
        switch (type) {
            case 0:
                description.text = $"Min Mining Power - {Player.minPower * 2}\nMax Mining Power - {Player.minPower * 4}";
                break;
            case 1:
                description.text = $"Automining Delay - {Player.miningDelay / 1.2f}s";
                break;
            case 2:
                description.text = $"Climbing Speed - {Player.climbingSpeed}";
                break;
            case 3:
                description.text = $"Min Automining Power - {Player.minAutoPower * 2}\nMax Automining Power - {Player.minAutoPower * 4}";
                break;
        }
    }

    private void Buy()
    {
        lvl++;
        if (lvl > 50)
            increaseNum += increaseIteration * 6;
        else if (lvl > 40)
            increaseNum += increaseIteration * 5;
        else if (lvl > 30)
            increaseNum += increaseIteration * 4;
        else if (lvl > 20)
            increaseNum += increaseIteration * 3;
        else if (lvl > 10)
            increaseNum += increaseIteration * 2;
        else if (lvl > 5)
            increaseNum += increaseIteration;
        priceCount += increaseNum;
        switch (type) {
            case 0:
                Player.minPower += 1;
                Player.maxPower += 2;
                break;
            case 1:
                Player.miningDelay -= 0.1f;
                break;
            case 2:
                Player.climbingSpeed += 0.7f;
                break;
            case 3:
                Player.minAutoPower += 1;
                Player.maxAutoPower += 2;
                break;
        }
        RerenderUI();
    }

    public void OnBuyButton()
	{
        if (GameManager.ChangeMoney(-priceCount))
		{
            Buy();
        }
    }
}
