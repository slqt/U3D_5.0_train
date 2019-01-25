using UnityEngine;
using System.Collections;

/// <summary>
/// 实现从上劈下的闪电
/// </summary>
public class LighteningManager : MonoBehaviour {
	public Vector3 startPos;//闪电起始位置
	public Vector3 endPos;//闪电终止位置
	public int steps;//闪电阶段数
	public float duration;//劈下花费的时间

	private LineRenderer lineRenderer;
	private int stepNow=0;
	void Awake()
	{
		//得到LineRenderer
		lineRenderer = GetComponent<LineRenderer>();
	}

	void OnGUI()
	{
		if (GUILayout.Button ("Start")) {
			Reset();
			NextStep();	
		}
	}

	void NextStep()
	{
		//设置LineRenderer的路径点数
		lineRenderer.SetVertexCount (stepNow+1);
		//计算当前阶段百分比，并得到当前点的坐标
		float pcg = (float)(stepNow+1)/(float)steps;
		Vector3 pos = Vector3.Lerp (startPos, endPos,pcg) + new Vector3 (Random.Range (-0.5f, 0.5f), Random.Range (-0.1f, 0.1f), 0);
		//设置路经点
		lineRenderer.SetPosition (stepNow, pos);
		//未结束时，过一段时间后进行下一步
		if (stepNow < steps) {
						stepNow++;
						Invoke ("NextStep", duration / (float)steps);
		}
	}

	/// <summary>
	/// 重置LineRenderer
	/// </summary>
	void Reset()
	{
		CancelInvoke();
		stepNow =0;
		lineRenderer.SetVertexCount (0);
	}
}
