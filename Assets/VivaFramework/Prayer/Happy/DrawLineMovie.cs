using UnityEngine;
using System.Collections;

//画线动画
public class DrawLineMovie : MonoBehaviour
{
	public float perDistance = 1f;

	private LineRenderer _lineR;

	public float lineWidth = 0.1f;

	public PointVO[] positions = new PointVO[2];
    // Use this for initialization
    void Start()
    {
		InitLineRenderer();

		Play();

		//print("2的3次方 " + Mathf.Pow(2, 3));
    }
    
	private void InitLineRenderer()
	{
		if (_lineR != null)
		{
			return;
		}
        _lineR = gameObject.GetComponent<LineRenderer>();
        if (_lineR == null)
        {
            _lineR = gameObject.AddComponent<LineRenderer>();
        }
	}

    [System.Serializable]
	public class PointVO {
		public Vector3 pos;
		public bool bezierP = false;
	}

	private int _playedIndex = -1;

	public void Play()
	{
		ClearLine();
		_playedIndex = 0;
		_startP = positions[_playedIndex].pos;
		_lineR.SetPositions(new Vector3[2]);
	}

	private Vector3 GetPosInStraightLine(PointVO vo)
	{
		Vector3 rP;

		if (Vector3.Distance(_startP, vo.pos) > perDistance)
        {
			Vector3 normal = (vo.pos - _startP).normalized;
			rP = _startP + normal * perDistance;
        }
        else
        {
			rP = vo.pos;
            _playedIndex++;
            //print("画线" + _playedIndex + " " + positions.Length);
        }
		return rP;
	}

	private Vector3 GetPosInCurve(PointVO vo)
	{
        Vector3 rP;
		_curveIndex++;
		float t = (float)_curveIndex / _curveTotal;
        //print("GetPosInCurve " + _curveIndex + " " + t);
		Vector3 p1 = _startP;
		Vector3 p2 = _bezierVO.pos;
		Vector3 p3 = vo.pos;

		rP = Mathf.Pow(1 - t, 2) * p1 + 2 * t * (1 - t) * p2 + Mathf.Pow(t, 2) * p3;


		if (_curveIndex == _curveTotal)
		{
			_bezierVO = null;
			_playedIndex++;

		}
        return rP;
	}

	private void OnDrawGizmosSelected()
	{
		//print("OnDrawGizmos");
        InitLineRenderer();
        Gizmos.color = Color.red;
		for (int i = 0; i < positions.Length;i++)
		{
			if (positions[i].pos != null)
			{
				Vector3 pos = positions[i].pos;
				Gizmos.color = positions[i].bezierP == true ? Color.green : Color.red;
				if (_lineR.useWorldSpace != true)
				{
					pos = gameObject.transform.TransformPoint(pos);
				}
				Gizmos.DrawSphere(pos, 0.2f);
            }
		}
		nextDraw();
	}

    //立即绘出线条
	public void ImmediateShowLine()
	{
		InitLineRenderer();
		Play();

		if (Application.isPlaying == true)
		{
			return;
		}
		int loopTime = 0;
        while (_playedIndex != -1)
        {
			loopTime++;
            nextDraw();
			if (loopTime > 500)
			{
				break;
			}
			//print("TestShowLine " + _playedIndex);
        }
	}

	public void ClearLine()
	{
		_playedIndex = -1;
		_lineR.positionCount = 0;
	}

	private void nextDraw()
	{

        if (_playedIndex == -1)
        {
            return;
        }

        //_lineR.SetWidth(lineWidth, lineWidth);

        //PointVO startVO = positions[_playedIndex];
        PointVO endVO = positions[_playedIndex + 1];
        if (endVO.bezierP == true)
        {
            _playedIndex++;
            _bezierVO = endVO;
            _curveIndex = 0;
            endVO = positions[_playedIndex + 1];

			_curveTotal = (int)Mathf.Round((Vector3.Distance(_startP, _bezierVO.pos) + Vector3.Distance(_bezierVO.pos, endVO.pos))/perDistance);
			_curveTotal = Mathf.Min(200, _curveTotal);
			//print("曲线绘制总数：" + _curveTotal);
        }


        if (_bezierVO == null)
        {
            _startP = GetPosInStraightLine(endVO);
        }
        else
        {
            _startP = GetPosInCurve(endVO);
        }

        _lineR.positionCount++;
        _lineR.SetPosition(_lineR.positionCount - 1, _startP);

        //播放结束
        if (_playedIndex == positions.Length - 1)
        {
            _playedIndex = -1;
        }
        else if (_playedIndex > 300)
        {
            print("绘制次数太大了兄弟，请把perDistance改大些~");
			_playedIndex = -1;
            return;
        }
	}

	private Vector3 _startP;
	private PointVO _bezierVO;
    
    private int _curveIndex;
	private int _curveTotal;
	// Update is called once per frame
	void Update()
	{
		nextDraw();
    }
}
