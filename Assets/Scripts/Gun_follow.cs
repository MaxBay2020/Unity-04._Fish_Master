using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_follow : MonoBehaviour
{
    public RectTransform UGUICanvas;
    public Camera mainCamera;
    // Update is called once per frame
    void Update()
    {
        Vector3 mouserPos;

        RectTransformUtility.ScreenPointToWorldPointInRectangle(UGUICanvas, new Vector2(Input.mousePosition.x, Input.mousePosition.y), mainCamera, out mouserPos);

        float z;

        if (mouserPos.x > transform.position.x)
        {
            z = -Vector3.Angle(Vector3.up, mouserPos - transform.position);
        }
        else
        {
            z = Vector3.Angle(Vector3.up, mouserPos - transform.position);
        }

        if(z<-70)
        {
            z = -70;
        }else if (z > 70)
        {
            z = 70;
        }

        transform.localRotation = Quaternion.Euler(0, 0, z);
    }
}
