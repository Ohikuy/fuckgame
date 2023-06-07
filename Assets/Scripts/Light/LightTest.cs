using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
//ע�������ռ䣬��Ҫ���ѹ�ʱ���Ǹ�

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
		//�������������Բ�ֵ��timerʵ�ֽ���
		//���Ҫ������ָ��ʱ���ڱ任����Time.deltatime�ϳˡ�1/ָ��ʱ�䡱����
	}

	private void LightTimer()
	{
		if (Input.GetKey(KeyCode.P) && timer <= 1)//��סL����
		{
			timer += Time.deltaTime;
		}
		else if (timer >= 0)//�ɿ�L����
		{
			timer -= Time.deltaTime;
		}
	}
}