using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
//注意命名空间，不要用已过时的那个

public class LightTest : MonoBehaviour
{
	private Light2D light2D;
	private float timer = 0;

	void Start()
	{
		light2D = GetComponent<Light2D>();
	}

	void Update()
	{
		LightTimer();

		light2D.intensity = Mathf.Lerp(0, 1, timer);
		//这里我们用线性插值和timer实现渐变
		//如果要控制在指定时间内变换，在Time.deltatime上乘“1/指定时间”即可
	}

	private void LightTimer()
	{
		if (Input.GetKey(KeyCode.P) && timer <= 1)//按住L渐亮
		{
			timer += Time.deltaTime;
		}
		else if (timer >= 0)//松开L渐暗
		{
			timer -= Time.deltaTime;
		}
	}
}