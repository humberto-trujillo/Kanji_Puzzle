using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiece : MonoBehaviour
{
    public int xIndex;
    public int yIndex;
	bool m_isMoving = false;

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			Move((int)transform.position.x + 1,(int)transform.position.y,0.2f);
		}
		if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			Move((int)transform.position.x - 1,(int)transform.position.y,0.5f);
		}
	}

    public void SetCoord(int x, int y)
    {
        xIndex = x;
        yIndex = y;
    }

	public void Move(int destX, int destY, float timeToMove)
	{
		if(!m_isMoving)
		{
			StartCoroutine(MoveRoutine(new Vector3(destX,destY,0),timeToMove));
		}
	}
	IEnumerator MoveRoutine(Vector3 destination, float timeToMove)
	{
		Vector3 startPosition = transform.position;
		bool reachedDestination = false;
		float elapsedTime = 0f;
		m_isMoving = true;
		while(!reachedDestination)
		{
			if(Vector3.Distance(transform.position,destination)<0.01f)
			{
				reachedDestination = true;
				transform.position = destination;
				SetCoord((int)destination.x,(int)destination.y);
				break;
			}
			elapsedTime += Time.deltaTime;
			float t = Mathf.Clamp(elapsedTime / timeToMove,0f,1f);
			transform.position =Vector3.Lerp(startPosition,destination,t);
			yield return null;
		}
		m_isMoving = false;
	}
}
