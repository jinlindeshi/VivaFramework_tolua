using UnityEngine;
using System.Collections;
using LuaInterface;
using System.Collections.Generic;


//方便处理一些 摄像机与指定对象 的交互
public class HappyCamera : MonoBehaviour
{
    public GameObject cameraObj;
    public GameObject attachObj;

    public GameObject directionObj;

    public bool lockY = false;

    // OnEnable 时，是否缓动恢复视角
    public float tweenResumeSpeed = -1f;

    private bool _resuming = false;

    public LuaFunction resumCallBack;

    private LuaFunction _checkOverFun;

    // 是否自动更新相机三围
    public bool autoUpdate = true;

    // Use this for initialization
    void Start()
    {

        EditorInit();

        //Debug.Log ("HappyCamera - " + gameObject);

        //TakeShake(100,null, 5, 0.1f, 0.01f, 10);
    }

    private void OnDisable()
    {
        //if (cameraObj != null)
        //{
        //    cameraObj.transform.localPosition = new Vector3(11.6f, 16f, 13.7f);
        //    cameraObj.transform.localEulerAngles = new Vector3(37f, 165f, -28f);
        //}
    }

    private float _resumeStartTime = -1f;
    private void OnEnable()
    {
        if (tweenResumeSpeed > 0)
        {
            _resumeStartTime = Time.time;
            _resuming = true;
        }
        //print("HappyCamera-OnEnable " + _resumeStartTime + " " + tweenResumeSpeed);
    }

    private bool _editorInit = false;
    public void EditorInit()
    {
        if (_editorInit == true)
        {
            return;
        }

        _editorInit = true;
        Camera camera = GetComponent<Camera>();
        if (camera == null)
        {
            SetAttachObj(gameObject);
        }
        else
        {
            SetCameraObj(gameObject);
        }
    }



    //挂载对象身上
    public void SetAttachObj(GameObject obj)
    {
        attachObj = obj;
    }

    //参考角度对象
    public void SetDirectionObj(GameObject obj)
    {
        directionObj = obj;
    }

    //摄像机
    public void SetCameraObj(GameObject obj)
    {
        cameraObj = obj;
    }

    public float vRotateAngle = 0f;
    public float hRotateAngle = 0f;

    private Vector3 GetRotateRoundPosition(Vector3 pos, Vector3 centerPos, Vector3 axis, float angle)
    {
        Vector3 p = Quaternion.AngleAxis(angle, axis) * (pos - centerPos);

        return centerPos + p;
    }


    //设置相机相对观察者的坐标增量
    public void SetOffsetV(Vector3 v)
    {
        followParams.offsetV = v;
    }

    public Vector3 GetOffsetV()
    {
        return followParams.offsetV;
    }

    [System.Serializable]
    public class FollowVO
    {
        public Vector3 offsetV = new Vector3(0, 4, -6);
        public bool lookAt = true;
        public bool globalReference = false;
        public bool checkCover = false;
        public Vector3 attachOffset = Vector3.zero;
    }

    public FollowVO followParams = new FollowVO();
    //固定的相对位置 跟随挂载对象 reference true参考世界坐标系 false参考挂载对象坐标系
    public void TakeFollow(Vector3 offset = new Vector3(), bool lookAt = true,
                           bool globalReference = false, bool checkCover = false, Vector3 attachOffset = new Vector3(), LuaFunction checkOverFun = null)
    {
        followParams.lookAt = lookAt;
        followParams.globalReference = globalReference;
        followParams.checkCover = checkCover;
        followParams.attachOffset = attachOffset;

        _checkOverFun = checkOverFun;
        if (offset != Vector3.zero)
        {
            followParams.offsetV = offset;
        }
        else
        {
            Vector3 cv = gameObject.transform.localPosition;
            Vector3 av = attachObj.transform.localPosition;
            followParams.offsetV = new Vector3(cv.x - av.x, cv.y - av.y, cv.z - av.z);
        }
    }

    private Quaternion GetFixedRotation(Vector3 pos)
    {

        Vector3 lookP = attachObj.transform.position + followParams.attachOffset;
        Quaternion rotation;
        if (followParams.lookAt == true)
        {
#if UNITY_EDITOR
            Debug.DrawLine(lookP, pos, Color.red);
#endif
            rotation = Quaternion.LookRotation(lookP - pos);

            //print("呵呵 " + Quaternion.Angle(cameraObj.transform.rotation, rotation) + " " + _resumeStartTime);
            if (_resumeStartTime > 0 && Quaternion.Angle(cameraObj.transform.rotation, rotation) > 1)
            {
                //print("相机角度恢复中 " + Quaternion.Angle(cameraObj.transform.rotation, rotation));
                Quaternion hehe = rotation;
                rotation = Quaternion.Lerp(cameraObj.transform.rotation, rotation, (Time.time - _resumeStartTime) * tweenResumeSpeed * 0.1f);
                //print("相机角度恢复后 " + Quaternion.Angle(rotation, hehe));
            }
        }
        else
        {
            rotation = cameraObj.transform.rotation;
        }

        return rotation;
    }

    private Vector3 GetFixedPosition(bool noTween = false)
    {
        Vector3 pos = cameraObj.transform.position;

        if (followParams.globalReference == false)
        {
            Transform directionTran;

            if (directionObj != null)
            {
                directionTran = directionObj.transform;
            }
            else
            {
                directionTran = attachObj.transform;
            }

            pos = attachObj.transform.position + directionTran.right * followParams.offsetV.x
                           + directionTran.up * followParams.offsetV.y + directionTran.forward * followParams.offsetV.z;
        }
        else
        {
            pos = attachObj.transform.position + followParams.offsetV;
        }


        Vector3 lookP = attachObj.transform.position + followParams.attachOffset;
        pos = GetRotateRoundPosition(pos, lookP, attachObj.transform.right, vRotateAngle);
        pos = GetRotateRoundPosition(pos, lookP, attachObj.transform.up, hRotateAngle);


        if (followParams.checkCover == true)
        {
            while (CheckCover(pos, lookP) == true)
            {
                pos = Vector3.Lerp(pos, lookP, 0.01f);
            }
        }

        if (noTween == false && _resumeStartTime > 0 && Vector3.Distance(cameraObj.transform.position, pos) > 0.02f)
        {
            //print("相机位置恢复中");
            pos = Vector3.Lerp(cameraObj.transform.position, pos, (Time.time - _resumeStartTime) * tweenResumeSpeed * 0.1f);
        }
        else if (noTween == false)
        {
            _resuming = false;
        }

        return pos;
    }

    public void FixTransform(bool onlyGetValue = false, LuaFunction call = null)
    {
        if (attachObj == null || cameraObj == null)
        {
            return;
        }



        Vector3 pos = cameraObj.transform.position;
        float initY = pos.y;
        pos = GetFixedPosition();
        Quaternion rot = GetFixedRotation(GetFixedPosition(true));

        if (call != null)
        {
            call.Call(pos, rot);
        }

        if (onlyGetValue == true)
        {
            return;
        }

        if (lockY == true)
        {
            pos.y = initY;
        }
        cameraObj.transform.position = pos;
        cameraObj.transform.rotation = rot;

        if (_resuming == false && _resumeStartTime > 0)
        {
            _resumeStartTime = -1;
            if (resumCallBack != null)
            {
                resumCallBack.Call();
            }
        }

    }


	public void SetCheckOverFun(LuaFunction fun)
	{
		_checkOverFun = fun;
	}

    //验证在相机和物体之间是否有遮挡
    private bool CheckCover(Vector3 pos, Vector3 attachWorldPos)
    {
        bool covered = false;
        //Debug.Log("CheckCover " + attachObj.transform.position.x + "_" + attachObj.transform.position.y + "_" + attachObj.transform.position.z);
        Vector3 origin = Vector3.Lerp(attachWorldPos, pos, 0.1f);
        Ray ray = new Ray(origin, pos - attachWorldPos);
        RaycastHit hit;
        Debug.DrawLine(origin, pos, Color.red);
        if (Physics.Raycast(ray, out hit, Vector3.Distance(pos, attachWorldPos)))
        {
            //if (hit.collider.gameObject.GetComponent<CharacterController>() == null)
            //{
            //             covered = true;
            //}

            if (_checkOverFun != null)
            {
                //covered = _checkOverFun.Call();
				covered = _checkOverFun.Invoke<GameObject, bool>(hit.collider.gameObject);
            }
            else
            {
                covered = true;
            }
        }
        return covered;
    }

    // Update is called once per frame
    void Update()
    {
        if (attachObj == null)
        {
            return;
        }

        if (autoUpdate == true)
        {
            FixTransform();
        }
    }
}

