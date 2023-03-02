using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI : Singleton<UI>
{
	[Header("UI Elements")]
    public Transform lifeContainer;
    public GameObject lifeObject;
	public TextMeshProUGUI scoreUI;
	[Header("Player")]
    public PlayerShip player;
	public int points;

    // Start is called before the first frame update
    void Start()
    {
		for (int i = 0; i < player.maxLife; i++)
		{
            GameObject.Instantiate(lifeObject, lifeContainer);
		}
    }

	public void UpdateLife(int life)
	{
		for (int i = 0; i < lifeContainer.childCount; i++)
		{
			lifeContainer.GetChild(i).gameObject.SetActive(i < life);
		}
	}

	public void UpdatePoints(int addPoints)
	{
		points += addPoints;
		scoreUI.text = points.ToString();
	}
}
