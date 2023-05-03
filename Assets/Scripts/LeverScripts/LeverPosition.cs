using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeverPosition : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] [Range(3, 7)] private int lever_pos_num;

    [SerializeField] AudioSource _audio_src;
    [SerializeField] SimpleSFX _lever_rotate_left;
    [SerializeField] SimpleSFX _lever_rotate_right;
    public int LeverPosNum
    {
        get { return lever_pos_num; }
    }
    public int LeverPosRotationX
    { 
        get { return (int)LeverHelper.ConvertEulerAngle(transform.localEulerAngles.x); }
    }

    private EventParameters leverPosParam;

    private void Start()
    {
        leverPosParam = new EventParameters();
        leverPosParam.AddParameter(EventParamKeys.LEVER_POS, this);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        EventBroadcaster.Instance.PostEvent(EventKeys.LEVER_POS_HOVER, leverPosParam);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(TagNames.LEVER_HANDLE))
            return;

        if (LeverHelper.ConvertEulerAngle(other.transform.localEulerAngles.x) > LeverHelper.ConvertEulerAngle(transform.localEulerAngles.x))
        {
            _lever_rotate_right.PlaySFX(_audio_src);
        }
        else if (LeverHelper.ConvertEulerAngle(other.transform.localEulerAngles.x) < LeverHelper.ConvertEulerAngle(transform.localEulerAngles.x))
        {
            _lever_rotate_left.PlaySFX(_audio_src);
        }
    }

}
