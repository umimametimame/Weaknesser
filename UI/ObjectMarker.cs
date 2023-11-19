using UnityEngine;

public class ObjectMarker : MonoBehaviour
{
    // �I�u�W�F�N�g���f���J����
    [SerializeField] private Camera targetCamera;
    
    // UI��\��������ΏۃI�u�W�F�N�g
    [SerializeField] private Transform target;

    [SerializeField] private RectTransform parentUI;

    // �I�u�W�F�N�g�ʒu�̃I�t�Z�b�g
    [SerializeField] private Vector3 worldOffset;


    // ���������\�b�h�iPrefab���琶�����鎞�ȂǂɎg���j
    public void Initialize(Transform target, Camera targetCamera = null)
    {
        this.target = target;
        this.targetCamera = targetCamera != null ? targetCamera : Camera.main;

        OnUpdatePosition();
    }

    private void Awake()
    {
        // �J�������w�肳��Ă��Ȃ���΃��C���J�����ɂ���
        if (targetCamera == null)
            targetCamera = Camera.main;

    }

    // UI�̈ʒu�𖈃t���[���X�V
    private void Update()
    {
        OnUpdatePosition();
    }

    // UI�̈ʒu���X�V����
    private void OnUpdatePosition()
    {
        // �J�����̌����x�N�g��
        var cameraDir = targetCamera.transform.forward;
        // �I�u�W�F�N�g�̈ʒu
        var targetWorldPos = target.position + worldOffset;
        // �J��������^�[�Q�b�g�ւ̃x�N�g��
        var targetDir = targetWorldPos - targetCamera.transform.position;

        // ���ς��g���ăJ�����O�����ǂ����𔻒�
        var isFront = Vector3.Dot(cameraDir, targetDir) > 0;

        // �J�����O���Ȃ�UI�\���A����Ȃ��\��
        gameObject.SetActive(isFront);
        if (!isFront) return;

        // �I�u�W�F�N�g�̃��[���h���W���X�N���[�����W�ϊ�
        var targetScreenPos = targetCamera.WorldToScreenPoint(targetWorldPos);

        // �X�N���[�����W�ϊ���UI���[�J�����W�ϊ�
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentUI,
            targetScreenPos,
            null,
            out var uiLocalPos
        );

        // RectTransform�̃��[�J�����W���X�V
        gameObject.transform.localPosition = uiLocalPos;
    }
}