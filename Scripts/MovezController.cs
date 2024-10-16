using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovezController : MonoBehaviour
{
    #region SetupField
    [SerializeField]
    private float defaultSpeed = 10;
    [SerializeField]
    private int startWaypointIndex;
    [SerializeField]
    private float rotateDuration = 4f;
    [SerializeField]
    private float adjustRotationTime = 0.5f;
    [SerializeField]
    private Transform targetTransformDebug;
    [SerializeField]
    private WPManager wPManager;
    #endregion

    int curTargetIndex;
    Vector3 targetPos;
    Vector3 directionToTarget;
    [SerializeField]
    private float curInterpolationVal;

    [SerializeField]
    private float speed = 10;
    float elapsedTime;
    float yCar;

    const float DELTA_ANGLE = 2;
    Quaternion targetQuarternion;
    Quaternion startQuarternion;

    

    private void Awake()
    {
        Application.runInBackground = true;
        yCar = transform.position.y;
        //Application.targetFrameRate = 30;
    }
    private void Start()
    {
        curTargetIndex = startWaypointIndex;
        speed = defaultSpeed;
        SetTarget();
        transform.position = targetPos;
        
        OnReachTarget();
        transform.forward = directionToTarget;
    }
    private void Update()
    {
        CalculateDirectionToTarget();
        if (IsReachTarget())
        {
            OnReachTarget();
        }
        LerpDirectionToTarget();
        MoveForward();
        AddPosToPathpoint();
    }

    public bool IsReachTarget()
    {
        return Vector3.Dot(transform.forward, directionToTarget) <= 0; 
        //Vector2 directionToTargetV2 = new Vector2(directionToTarget.x, directionToTarget.z);
        //Vector2 curDirection = new Vector2(transform.forward.x, transform.forward.z);

        //return Vector2.Dot(directionToTargetV2, curDirection) <= 0;
    }

    public void ResetLerpWhenReachTarget()
    {
        elapsedTime = 0;
    }

    public void LerpDirectionToTarget()
    {
        float angle = Vector3.Angle(directionToTarget, transform.forward);
        if (angle < DELTA_ANGLE)
        {
            transform.forward = directionToTarget;
            return;
        }
        elapsedTime += Time.deltaTime;
        curInterpolationVal = elapsedTime / rotateDuration;
        if (curInterpolationVal >= 1)
        {
            curInterpolationVal = 1;
        }
        targetQuarternion = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(startQuarternion, targetQuarternion, curInterpolationVal);
    }

 
    public void CalculateDirectionToTarget()
    {
        directionToTarget = targetPos - transform.position;
        //directionToTarget.y = 0;
        
    }
    public void CalculateStartTargetQuarternion()
    {
        startQuarternion = Quaternion.LookRotation(transform.forward);
        //targetQuarternion = Quaternion.LookRotation(directionToTarget);
    }
    public void OnReachTarget()
    {
        if (curTargetIndex == startWaypointIndex)
        {
            OnCompleteARound();
        }
        IncreaseIndex();
        SetTarget();
        CalculateDirectionToTarget();
        CalculateStartTargetQuarternion();
        SetSpeed();
        ResetLerpWhenReachTarget();
    }

    public void AddPosToPathpoint()
    {
        wPManager.AddPathPoints(transform.position);
    }
    public void MoveForward()
    {
        transform.position += speed * Time.deltaTime * transform.forward;
    }

    public int GetNextIndex()
    {
        int result = curTargetIndex + 1;
        if (result >= wPManager.targets.Length)
        {
            result = 0;
        }
        return result;
    }
    public void IncreaseIndex()
    {
        curTargetIndex = GetNextIndex();
        if (curTargetIndex >= wPManager.targets.Length)
        {
            curTargetIndex = 0;
        }
       
    }

    void OnCompleteARound()
    {
        wPManager.ClearPathPoints();
    }
    public void SetSpeed()
    {
        float distance = directionToTarget.magnitude;
        float targetSpeedToRotate = distance / (rotateDuration + adjustRotationTime);
        if (targetSpeedToRotate > defaultSpeed)
        {
            targetSpeedToRotate = defaultSpeed;
        }

        speed = targetSpeedToRotate;
    }
    public void SetTarget()
    {
        targetPos = wPManager.targets[curTargetIndex].position;
        //targetPos.y = yCar;
        targetTransformDebug.position = targetPos;
    }
}
