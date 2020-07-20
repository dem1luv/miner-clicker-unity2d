using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Text title;
    [SerializeField] Text description;
    [SerializeField] Text price;

    public int priceCount;

	private void Start()
	{
        RerenderUI();
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
