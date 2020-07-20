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
    public int priceCount;

    [Header("Design")]
    [SerializeField] Color availableColor;
    [SerializeField] Color notAvailableColor;

    private Image imageComp;

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
        description.text = $"Min Power - {Player.minPower}\nMax Power - {Player.maxPower}";
    }

    public void OnBuyButton()
	{
        if (GameManager.ChangeMoney(-priceCount))
		{
            Player.minPower *= 2;
            Player.maxPower = Player.minPower * 2;
            priceCount = (int)(priceCount * 2.6f);
            RerenderUI();
        }
    }
}
