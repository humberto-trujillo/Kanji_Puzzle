using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class RectXformMover : MonoBehaviour
{
	public Vector3 startPosition;
	public Vector3 onScreenPosition;
	public Vector3 endPosition;
	public float timeToMove = 1f;
	RectTransform m_rectXform;
	bool m_isMoving = false;

	void Awake()
	{
		m_rectXform = GetComponent<RectTransform>();
	}

	void Start()
	{
		//MoveOff();
	}

	void Move(Vector3 startPos, Vector3 endPos, float timeToMove)
	{
		if(!m_isMoving)
		{
			StartCoroutine(MoveRoutine(startPos,endPos,timeToMove));
		}
	}

	IEnumerator MoveRoutine(Vector3 startPos, Vector3 endPos, float timeToMove)
	{
		if(m_rectXform != null)
		{
			m_rectXform.anchoredPosition = startPosition;
		}
		bool reachedDestination = false;
		float elapsedTime = 0f;
		m_isMoving = true;
		while(!reachedDestination)
		{
			if(Vector3.Distance(m_rectXform.anchoredPosition,endPos) < 0.01f)
			{
				reachedDestination = true;
				break;
			}
			elapsedTime += Time.deltaTime;
			float t = Mathf.Clamp(elapsedTime/timeToMove,0f,1f);
			t = t*t*t*(t*(t*6-15)+10); //smoother step interpolation
			if(m_rectXform != null)
			{
				m_rectXform.anchoredPosition = Vector3.Lerp(startPos,endPos,t);
			}
			yield return null;
		}
		m_isMoving = false;
	}

	public void MoveOn()
	{
		Move(startPosition,onScreenPosition,timeToMove);
	}

	public void MoveOff()
	{
		Move(onScreenPosition,endPosition,timeToMove);
	}
}
