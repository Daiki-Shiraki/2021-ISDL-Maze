using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class TouchPadMove : MonoBehaviour
{
    public Transform bodyCollider;
    public SteamVR_Action_Vector2 walkAction;
    public float walkSpeed = 2.0f;

    void Start()
    {
        // ヘッドセットのある位置に合わせてPlayerの初期位置を移動
        Vector3 pos = transform.position;
        pos.x -= Player.instance.hmdTransform.localPosition.x;
        pos.z -= Player.instance.hmdTransform.localPosition.z;
        pos.y = transform.position.y;
        transform.position = pos;

        // bodyCollider初期位置
        Vector3 colpos = bodyCollider.transform.position;
        colpos.x = Player.instance.hmdTransform.position.x;
        colpos.z = Player.instance.hmdTransform.position.z;
        bodyCollider.transform.position = colpos;
    }

    void LateUpdate()
    {
        // 頭と身体の位置が合うよう、playerの位置を調整
        Vector3 player_pos = transform.position;
        player_pos.x -= Player.instance.hmdTransform.position.x - bodyCollider.transform.position.x;
        player_pos.z -= Player.instance.hmdTransform.position.z - bodyCollider.transform.position.z;
        player_pos.y = bodyCollider.transform.position.y;

        transform.position = player_pos;
    }

    void FixedUpdate()
    {
        Vector3 player_pos = transform.position;
        Vector3 body_pos = bodyCollider.transform.position;

        // bodyの位置をヘッドセットに合わせる
        body_pos.x = Player.instance.hmdTransform.position.x;
        body_pos.z = Player.instance.hmdTransform.position.z;
        bodyCollider.transform.position = body_pos;

        // Actionでの移動
        Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(walkAction.axis.x, 0, walkAction.axis.y));
        player_pos.x += walkSpeed * Time.deltaTime * direction.x;
        player_pos.z += walkSpeed * Time.deltaTime * direction.z;

        // 高さ移動
        player_pos.y = bodyCollider.transform.position.y;
        transform.position = player_pos;
    }
}