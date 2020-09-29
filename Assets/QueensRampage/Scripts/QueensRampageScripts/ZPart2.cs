using UnityEngine;
using System.Collections;

public class ZPart2 : MonoBehaviour {

	public int col;
	public bool killZone = true;

	public int row;

	public void getClosestParts()
	{
		ZBoard board = (ZBoard)GameObject.FindObjectOfType(typeof(ZBoard));
		if(board)
		{
			float cellOffset = board.cellOffset;


			col = (int)(transform.position.x / cellOffset);
			row = (int)(transform.position.z / cellOffset);
		}
		name = "C" + col + "R" + row;
	}
	public void init(int c, int r)
	{
		col = c;
		row = r;
	}
}
