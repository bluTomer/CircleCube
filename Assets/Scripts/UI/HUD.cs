using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	public Button[] Buttons;
	public Slider ProgressSlider;
	public Text ProgressCount;

	private bool _isRunning;
	
	public void Init()
	{
        SetColor((int)GameManager.Instance.Config.StartingColor);
		for (int i = 0; i < Buttons.Length; i++)
		{
			SetImageColor(i);
		}
		
		SetProgress();
		GameManager.Instance.EnemyDestroyedEvent += SetProgress;
		_isRunning = true;
	}

	public void DeInit()
	{
		GameManager.Instance.EnemyDestroyedEvent -= SetProgress;
		_isRunning = false;
	}

	private void Update()
	{
		if (!_isRunning)
			return;

		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			SetColor(0);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			SetColor(1);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			SetColor(2);
		}
	}

	private void SetProgress()
	{
		var current = GameManager.Instance.DestroyedEnemies;
		var total = GameManager.Instance.Config.EnemiesToWin;

		ProgressSlider.value = (float)current / total;
		ProgressCount.text = string.Format("{0}/{1}", current, total);
	}

	private void SetImageColor(int index)
	{
		Buttons[index].GetComponentInChildren<Image>().color = GameManager.Instance.Config.GetColor((ColorType) index);
	}

	public void SetColor(int index)
	{
		GameManager.Instance.SetColor((ColorType) index);
		
		// Set outline
		for (int i = 0; i < Buttons.Length; i++)
		{
			Buttons[index].GetComponent<Outline>().enabled = i == index;
		}
	}
}
