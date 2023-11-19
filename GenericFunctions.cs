using System;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.Assertions;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace My
{

    public class GenericFunctions : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

    public class SingletonDontDestroy<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T instance;

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));
                DontDestroyOnLoad(gameObject); // �ǉ�
            }
            else
                Destroy(gameObject);
        }
    }
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {

        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    Type t = typeof(T);

                    instance = (T)FindObjectOfType(t);
                    if (instance == null)
                    {
                        Debug.LogError(t + " ���A�^�b�`���Ă���GameObject�͂���܂���");
                    }
                }

                return instance;
            }
        }

        virtual protected void Awake()
        {
            // ���̃Q�[���I�u�W�F�N�g�ɃA�^�b�`����Ă��邩���ׂ�
            // �A�^�b�`����Ă���ꍇ�͔j������B
            CheckInstance();
        }

        protected bool CheckInstance()
        {
            if (instance == null)
            {
                instance = this as T;
                return true;
            }
            else if (Instance == this)
            {
                return true;
            }
            Destroy(this);
            return false;
        }
    }

    public static class AddFunction
    {

        /// <summary>
        /// Vector2���p�x(360�x)�ɕύX
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static float Vec2ToAngle(Vector2 v)
        {
            return Mathf.Repeat(Mathf.Atan2(v.x, v.y) * Mathf.Rad2Deg, 360);
        }

        public static float GetAngleByVec2(Vector3 start, Vector3 target)
        {
            float angle;
            Vector3 dt = start - target;
            angle = Mathf.Atan2(dt.y, dt.x) * Mathf.Rad2Deg;

            return angle;
        }

        public static Vector3 CameraToMouse()
        {
            return new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0.0f);
        }

        /// <summary>
        /// �^�O����v�f�ii�Ȃǁj�ɂ��ĕԂ�
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static int TagToArray(string tag)
        {
            switch (tag)
            {
                case "Player01":
                    return 0;
                case "Player02":
                    return 1;
            }
            return -1;
        }

        /// <summary>
        /// �v�f�ii�Ȃǁj���^�O���ɂ��ĕԂ�
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static string ArrayToTag(int array)
        {
            switch (array)
            {
                case 0:
                    return "Player01";
                case 1:
                    return "Player02";
            }
            return "0";
        }

        /// <summary>
        /// Rect�̗�
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static float Neighbor(Rect rect)
        {
            return rect.x + rect.width;
        }
        public static float Neighbor(HorizontalRect rect)
        {
            return rect.x + rect.width;
        }
        /// <summary>
        /// Animation�̒�����Ԃ�
        /// </summary>
        /// <param name="animator"></param>
        /// <param name="clipName"></param>
        /// <returns></returns>
        public static float GetAnimationClipLength(Animator animator, string clipName)
        {
            return Get(animator.runtimeAnimatorController.animationClips, clipName);

            float Get(IEnumerable<AnimationClip> animationClips, string clipName)
            {
                return (from animationClip in animationClips
                        where animationClip.name == clipName
                        select animationClip.length).FirstOrDefault();
            }
        }

        public enum AnchorPresets
        {
            TopLeft,
            TopCenter,
            TopRight,

            MiddleLeft,
            MiddleCenter,
            MiddleRight,

            BottomLeft,
            BottonCenter,
            BottomRight,
            BottomStretch,

            VertStretchLeft,
            VertStretchRight,
            VertStretchCenter,

            HorStretchTop,
            HorStretchMiddle,
            HorStretchBottom,

            StretchAll
        }

        public enum PivotPresets
        {
            TopLeft,
            TopCenter,
            TopRight,

            MiddleLeft,
            MiddleCenter,
            MiddleRight,

            BottomLeft,
            BottomCenter,
            BottomRight,
        }
        public static void SetAnchor(this RectTransform source, AnchorPresets allign, int offsetX = 0, int offsetY = 0)
        {
            source.anchoredPosition = new Vector3(offsetX, offsetY, 0);

            switch (allign)
            {
                case (AnchorPresets.TopLeft):
                    {
                        source.anchorMin = new Vector2(0, 1);
                        source.anchorMax = new Vector2(0, 1);
                        break;
                    }
                case (AnchorPresets.TopCenter):
                    {
                        source.anchorMin = new Vector2(0.5f, 1);
                        source.anchorMax = new Vector2(0.5f, 1);
                        break;
                    }
                case (AnchorPresets.TopRight):
                    {
                        source.anchorMin = new Vector2(1, 1);
                        source.anchorMax = new Vector2(1, 1);
                        break;
                    }

                case (AnchorPresets.MiddleLeft):
                    {
                        source.anchorMin = new Vector2(0, 0.5f);
                        source.anchorMax = new Vector2(0, 0.5f);
                        break;
                    }
                case (AnchorPresets.MiddleCenter):
                    {
                        source.anchorMin = new Vector2(0.5f, 0.5f);
                        source.anchorMax = new Vector2(0.5f, 0.5f);
                        break;
                    }
                case (AnchorPresets.MiddleRight):
                    {
                        source.anchorMin = new Vector2(1, 0.5f);
                        source.anchorMax = new Vector2(1, 0.5f);
                        break;
                    }

                case (AnchorPresets.BottomLeft):
                    {
                        source.anchorMin = new Vector2(0, 0);
                        source.anchorMax = new Vector2(0, 0);
                        break;
                    }
                case (AnchorPresets.BottonCenter):
                    {
                        source.anchorMin = new Vector2(0.5f, 0);
                        source.anchorMax = new Vector2(0.5f, 0);
                        break;
                    }
                case (AnchorPresets.BottomRight):
                    {
                        source.anchorMin = new Vector2(1, 0);
                        source.anchorMax = new Vector2(1, 0);
                        break;
                    }

                case (AnchorPresets.HorStretchTop):
                    {
                        source.anchorMin = new Vector2(0, 1);
                        source.anchorMax = new Vector2(1, 1);
                        break;
                    }
                case (AnchorPresets.HorStretchMiddle):
                    {
                        source.anchorMin = new Vector2(0, 0.5f);
                        source.anchorMax = new Vector2(1, 0.5f);
                        break;
                    }
                case (AnchorPresets.HorStretchBottom):
                    {
                        source.anchorMin = new Vector2(0, 0);
                        source.anchorMax = new Vector2(1, 0);
                        break;
                    }

                case (AnchorPresets.VertStretchLeft):
                    {
                        source.anchorMin = new Vector2(0, 0);
                        source.anchorMax = new Vector2(0, 1);
                        break;
                    }
                case (AnchorPresets.VertStretchCenter):
                    {
                        source.anchorMin = new Vector2(0.5f, 0);
                        source.anchorMax = new Vector2(0.5f, 1);
                        break;
                    }
                case (AnchorPresets.VertStretchRight):
                    {
                        source.anchorMin = new Vector2(1, 0);
                        source.anchorMax = new Vector2(1, 1);
                        break;
                    }

                case (AnchorPresets.StretchAll):
                    {
                        source.anchorMin = new Vector2(0, 0);
                        source.anchorMax = new Vector2(1, 1);
                        break;
                    }
            }
        }

        public static void SetPivot(this RectTransform source, PivotPresets preset)
        {

            switch (preset)
            {
                case (PivotPresets.TopLeft):
                    {
                        source.pivot = new Vector2(0, 1);
                        break;
                    }
                case (PivotPresets.TopCenter):
                    {
                        source.pivot = new Vector2(0.5f, 1);
                        break;
                    }
                case (PivotPresets.TopRight):
                    {
                        source.pivot = new Vector2(1, 1);
                        break;
                    }

                case (PivotPresets.MiddleLeft):
                    {
                        source.pivot = new Vector2(0, 0.5f);
                        break;
                    }
                case (PivotPresets.MiddleCenter):
                    {
                        source.pivot = new Vector2(0.5f, 0.5f);
                        break;
                    }
                case (PivotPresets.MiddleRight):
                    {
                        source.pivot = new Vector2(1, 0.5f);
                        break;
                    }

                case (PivotPresets.BottomLeft):
                    {
                        source.pivot = new Vector2(0, 0);
                        break;
                    }
                case (PivotPresets.BottomCenter):
                    {
                        source.pivot = new Vector2(0.5f, 0);
                        break;
                    }
                case (PivotPresets.BottomRight):
                    {
                        source.pivot = new Vector2(1, 0);
                        break;
                    }
            }
        }
        /// <summary>
         /// Canvas��Render Mode �� Scene Space - Overlay �̏ꍇ�ɁA���[���h���W���X�N���[�����W�ɕϊ�����
         /// </summary>
         /// <returns>�ϊ����ꂽ�X�N���[�����W</returns>
         /// <param name="position">�Ώۂ̃��[���h���W</param>
        public static Vector2 ToScreenPositionCaseScreenSpaceOverlay(this Vector3 position)
        {
            return position.ToScreenPositionCaseScreenSpaceOverlay(Camera.main);
        }

        /// <summary>
        /// Canvas��Render Mode �� Scene Space - Overlay �̏ꍇ�ɁA���[���h���W���X�N���[�����W�ɕϊ�����
        /// </summary>
        /// <returns>�ϊ����ꂽ�X�N���[�����W</returns>
        /// <param name="position">�Ώۂ̃��[���h���W</param>
        /// <param name="camera">���C���J����</param>
        public static Vector2 ToScreenPositionCaseScreenSpaceOverlay(this Vector3 position, Camera camera)
        {
            return RectTransformUtility.WorldToScreenPoint(camera, position);
        }

        /// <summary>
        /// Canvas��Render Mode �� Scene Space - Camera �̏ꍇ�ɁA���[���h���W���X�N���[�����W�ɕϊ�����
        /// </summary>
        /// <returns>�ϊ����ꂽ�X�N���[�����W</returns>
        /// <param name="position">�Ώۂ̃��[���h���W</param>
        /// <param name="canvas">UI��Canvas</param>
        public static Vector2 ToScreenPositionCaseScreenSpaceCamera(this Vector3 position, Canvas canvas)
        {
            return position.ToScreenPositionCaseScreenSpaceCamera(canvas, Camera.main);
        }

        /// <summary>
        /// Canvas��Render Mode �� Scene Space - Camera �̏ꍇ�ɁA���[���h���W���X�N���[�����W�ɕϊ�����
        /// </summary>
        /// <returns>�ϊ����ꂽ�X�N���[�����W</returns>
        /// <param name="position">�Ώۂ̃��[���h���W</param>
        /// <param name="canvas">UI��Canvas</param>
        /// <param name="uiCamera">UI���ʂ��J�����iCanvas��RenderCamera�ɐݒ肳��Ă���J�����j</param>
        public static Vector2 ToScreenPositionCaseScreenSpaceCamera(this Vector3 position, Canvas canvas, Camera uiCamera)
        {
            return position.ToScreenPositionCaseScreenSpaceCamera(canvas, uiCamera, Camera.main);
        }

        /// <summary>
        /// Canvas��Render Mode �� Scene Space - Camera �̏ꍇ�ɁA���[���h���W���X�N���[�����W�ɕϊ�����
        /// </summary>
        /// <returns>�ϊ����ꂽ�X�N���[�����W</returns>
        /// <param name="position">�Ώۂ̃��[���h���W</param>
        /// <param name="canvas">UI��Canvas</param>
        /// <param name="uiCamera">UI���ʂ��J�����iCanvas��RenderCamera�ɐݒ肳��Ă���J�����j</param>
        /// <param name="worldCamera">���C���J����</param>
        public static Vector2 ToScreenPositionCaseScreenSpaceCamera(this Vector3 position, Canvas canvas, Camera uiCamera, Camera worldCamera)
        {
            Assert.IsTrue(
                canvas.renderMode == RenderMode.ScreenSpaceCamera,
                "Canvas�̃����_�[���[�h���uScene Space - Camera�v�ɂȂ��Ă��܂���"
            );

            var p = RectTransformUtility.WorldToScreenPoint(worldCamera, position);
            var retPosition = Vector2.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.GetComponent<RectTransform>(),
                p,
                uiCamera,
                out retPosition
            );
            return retPosition;
        }
    }

    /// <summary>
    /// �w�肵���^���������AList�ɂ��ĕԂ��֐�
    /// </summary>
    public class TypeFinder : MonoBehaviour
    {
        [field: SerializeField] public FieldInfo[] fields { get; private set; }
        public List<T> GetAndInList<T>(Type type)
        {
            fields = type.GetFields(BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            List<T> variables = new List<T>();
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType == typeof(T))
                {
                    T variable = (T)field.GetValue(GetComponent(type));
                    variables.Add(variable);

                }
            }

            return variables;
        }
    }


    public enum ExistState
    {
        Disable,
        Start,
        Enable,
        Ending,
    }

    [Serializable] public class Exist
    {
        [field: SerializeField, NonEditable] public ExistState state { get; private set; } = ExistState.Disable;
        [field: SerializeField] public Action initialize { get; set; }  
        [field: SerializeField] public Action disable { get; set; }
        [field: SerializeField] public Action start { get; set; }
        [field: SerializeField] public Action enable { get; set; }
        [field: SerializeField] public Action toEnd { get; set; }
        [field: SerializeField] public Action ending { get; set; }

        public bool started { get; private set; }

        public void Initialize(bool started = false)
        {
            initialize?.Invoke();
            this.started = started;
            state = ExistState.Disable;
        }

        public void Reset()
        {
            state = ExistState.Disable;
        }

        public void Update()
        {
            switch (state)
            {
                case ExistState.Disable:
                    disable?.Invoke();
                    break;

                case ExistState.Start:
                    state = ExistState.Enable;
                    start?.Invoke();
                    break;

                case ExistState.Enable:
                    enable?.Invoke();
                    break;
                case ExistState.Ending:

                    break;
            }
        }

        public void Stop()
        {
            state = ExistState.Disable;
        }

        public void Start()
        {
                state = ExistState.Start;
        }

        /// <summary>
        /// ��x�̂�
        /// </summary>
        public void StartOneShot()
        {
            if (started == false)
            {
                state = ExistState.Start;
                started = true;
            }
        }

        public void Finish()
        {
            state = ExistState.Ending;
            toEnd?.Invoke();
        }
    }


    /// <summary>
    /// �ړ��͈͂��~�`�ɐ���
    /// </summary>
    [Serializable]
    public class CircleClamp
    {

        [SerializeField] private GameObject center;
        [field: SerializeField] public GameObject moveObject { get; set; }
        [field: SerializeField] public float radius { get; private set; }

        public void Initialize()
        {
            moveObject.transform.position = center.transform.position;
        }
        public void AdjustByCenter()
        {
            moveObject.transform.position = center.transform.position;
        }
        public void Limit()
        {
            if (Vector2.Distance(moveObject.transform.position, center.transform.position) > radius)
            {
                Vector3 nor = moveObject.transform.position - center.transform.position;
                moveObject.transform.position = center.transform.position + nor.normalized * radius;
            }

        }
    }

    [Serializable] public class SmoothRotate
    {
        [SerializeField] private float speed;
        [SerializeField] private GameObject targetObj;
        public void Initialize(GameObject targetObj)
        {
            this.targetObj = targetObj;
        }
        public void Update(Vector3 direction)
        {
            Quaternion me = targetObj.transform.rotation;
            Quaternion you = Quaternion.LookRotation(direction);
            targetObj.transform.rotation = Quaternion.RotateTowards(me, you, speed * Time.deltaTime);
        }
    }

    [Serializable] public class EasingAnimator
    {
        [field: SerializeField, NonEditable] public float nowRatio { get; private set; } 
        [field: SerializeField, NonEditable] public float maxTime { get; private set; }
        [SerializeField] private AnimationCurve curve;
        public Animator animator { get; set; }
        public void Initialize(float maxTime,Animator animator = null)
        {
            if (animator != null) { this.animator = animator; }
            this.maxTime = maxTime;
            nowRatio = 0.0f;
        }

        public void Reset()
        {
            nowRatio = 0.0f;
        }
        public void Update()
        {
            animator.speed *= curve.Evaluate(nowRatio);
            nowRatio += 1 / maxTime * Time.deltaTime;
        }
    }

    /// <summary>
    /// �Ԋu����N���X
    /// </summary>
    [Serializable]
    public class Interval
    {
        public enum IncreseType
        {
            DeltaTime,
            Frame,
            Manual,
        }
        [field: SerializeField, NonEditable] public bool active { get; private set; }
        [field: SerializeField] public float interval { get; private set; }
        [field: SerializeField, NonEditable] public float value;
        [field: SerializeField] public IncreseType valueIncreseType { get; set; }
        private bool autoReset;
        private bool reached;
        public Action reachAction { get; set; }
        public Action activeAction { get; set; }
        public Action lowAction { get; set; }

        /// <summary>
        /// ����:<br/>
        /// �E�ŏ�����active�ɂ���(interval�l��value�𓯂��ɂ���)��<br/>
        /// �Evalue��interval�l�ɓ��B������0�ɖ߂邩<br/>
        /// �E�ŏ���interval�l
        /// </summary>
        /// <param name="start"></param>
        public void Initialize(bool start, bool autoReset = true, float interval = 0.0f)
        {
            if(interval != 0.0f) { this.interval = interval; }
            this.autoReset = autoReset;
            if (start == true)
            {
                value = this.interval;
            }
            else
            {
                value = 0.0f;
            }

            active = (value >= interval) ? true : false;
            reached = false;
        }

        public void Update(float manualValue = 0.0f)
        {
            switch (valueIncreseType)
            {
                case IncreseType.DeltaTime:
                value += Time.deltaTime;

                    break;

                case IncreseType.Frame:
                    value++;

                    break;

                case IncreseType.Manual:
                    value = manualValue;
                    break;
            }
            if (value >= interval)
            {
                if(reached == false)
                {
                    reached = true;
                    reachAction?.Invoke();
                }

                active = true;
                activeAction?.Invoke();
                if(autoReset == true) { Reset(); }
            }
            else
            {
                active = false;
                lowAction?.Invoke();
            }
        }


        public void Reset()
        {
            reached = false;
            value = 0.0f;
        }
    }

    /// <summary>
    /// �͈͖���Action�����s����
    /// </summary>
    [Serializable] public class ThresholdRatio
    {
        [SerializeField, NonEditable] private bool reaching;
        [SerializeField, NonEditable] private bool beforeBool;

        [SerializeField] private Vector2 thresholdRange;
        [SerializeField, NonEditable] private float currentValue;
        [SerializeField, NonEditable] private Vector2 beforeRange;
        public MomentAction withinRangeAction { get; set; } = new MomentAction();
        public Action inRangeAction { get; set; }
        public MomentAction exitRangeAction { get; set; } = new MomentAction();
        public Action outOfRangeAction { get; set; }

        public void Initialize(float min, float max)
        {
            thresholdRange = new Vector2(min, max);
            Reset();
        }
        public void Initialize(Vector2 range = default)
        {
            if (range != default) { thresholdRange = range; }
            Reset();
        }

        public void Reset()
        {
            beforeBool = false;
            beforeRange = Vector2.zero;
            withinRangeAction.Initialize();
            exitRangeAction.Initialize();
        }

        /// <summary>
        /// ����:���݂̊���
        /// </summary>
        /// <param name="value"></param>
        public void Update(float value)
        {
            currentValue = value;
            
            // �͈͓��Ȃ�
            if (thresholdRange.x <= currentValue && currentValue <= thresholdRange.y) { reaching = true; }
            else { reaching = false; }


            if(reaching == true)        // �͈͓���
            {
                if (beforeBool == false)    // �������u�ԂȂ�
                {
                    withinRangeAction.Enable();
                }

                inRangeAction?.Invoke();

            }

            if(beforeBool == true)      // �O��͈͓���
            {
                if (reaching == false)  // �o��u�ԂȂ�
                {
                    exitRangeAction.Enable();
                }
            }

            if(reaching == false)   // �͈͊O�Ȃ�
            {
                outOfRangeAction?.Invoke();
            }

            beforeBool = reaching;
            beforeRange = thresholdRange;
        }

    }

    /// <summary>
    /// Update���ł���x�������s�ł���
    /// </summary>
    [Serializable] public class MomentAction
    {
        public Action action { get; set; }
        [SerializeField, NonEditable] private bool activated;

        public void Initialize()
        {
            activated = false;
        }

        public void Enable()
        {
            if(activated == false) 
            { 
                action?.Invoke(); 
                activated = true;
            }
        }
    }

    [Serializable]
    public class Shake
    {
        [field: SerializeField] public Interval interval { get; set; }
        [field: SerializeField] public GameObject targetObj { get; set; }

        public void Initialize()
        {
            interval.Initialize(true);
        }

        public void Update()
        {
            if (interval.active == true)
            {
            }
            interval.Update();

        }

    }

    [Serializable] public class EntityAndPlan<T>
    {
        [field: SerializeField, NonEditable] public T entity { get; set; }
        [field: SerializeField, NonEditable] public T plan { get; set; }

        public void Assign()
        {
            plan = entity;
        }
    }

    [Serializable] public class HorizontalRect
    {
        [field: SerializeField] public float x { get; private set; }
        [field: SerializeField] public float y { get; private set; }
        [field: SerializeField] public float width { get; private set; }
        [field: SerializeField] public float height { get; private set; }
        public Rect entity { get; private set; }

        public HorizontalRect(Rect rect)
        {
            x = rect.x;
            y = rect.y;
            width = rect.width;
            height = rect.height;

            entity = rect;
        }

        public void Initialize(Rect rect)
        {
            x = rect.x;
            y = rect.y;
            width = rect.width;
            height = rect.height;

            entity = rect;
        }

        public void Set(float x, float width)
        {
            this.x = x;
            this.width = width;

            entity = new Rect(this.x, y, this.width, height);
        }

        public float X
        {
            set 
            { 
                x = value;
                entity = new Rect(this.x, y, this.width, height);
            }
        }

        public float Width
        {
            set
            {
                width = value;
                entity = new Rect(this.x, y, this.width, height);
            }
        }

    }

    /// <summary>
    /// SpriteRenderer,Image,TMPro�̂��ׂĂ��擾����
    /// </summary>
    [Serializable]
    public class SpriteOrImage
    {
        [field: SerializeField] public SpriteRenderer[] sprites { get; set; }
        [field: SerializeField] public Image[] images { get; set; }
        [field: SerializeField] public TextMeshProUGUI[] texts { get; set; }

        /// <summary>
        /// ������SpriteRenderer�܂���Image���A�^�b�`���ꂽGameObject
        /// </summary>
        /// <param name="obj"></param>
        public virtual void Initialize(GameObject obj)
        {
            sprites = obj.GetComponentsInChildren<SpriteRenderer>();
            images = obj.GetComponentsInChildren<Image>();
            texts = obj.GetComponentsInChildren<TextMeshProUGUI>();
            if(sprites.Length == 0 && images.Length == 0 && texts.Length == 0)
            {
                Debug.LogError("��������A�^�b�`����Ă��܂���");
            }
        }

        /// <summary>
        /// SpriteRenderer�܂���Image��Color��Ԃ�<br/>
        /// �������݂��Ȃ��ꍇ�̓G���[���b�Z�[�W���o����Color.white��Ԃ�
        /// </summary>
        public Color color
        {
            get
            {
                if (sprites.Length != 0)
                {

                    return sprites[0].color;
                }
                else if (images.Length != 0)
                {
                    return images[0].color;
                }

                Debug.LogError("SpriteRenderer�܂���Image���A�^�b�`���Ă�������");
                return Color.white;
            }
            set
            {
                if (sprites.Length != 0)
                {
                    foreach (SpriteRenderer sprite in sprites)
                    {
                        sprite.color = value;

                    }
                }
                else if (images.Length != 0)
                {
                    foreach (Image image in images)
                    {

                        image.color = value;
                    }
                }
                else if (texts.Length != 0)
                {
                    foreach (TextMeshProUGUI text in texts)
                    {

                        text.color = value;
                    }
                }
                else
                {
                    Debug.LogError("SpriteRenderer�܂���Image���A�^�b�`���Ă�������");
                }
            }
        }

        public float Alpha
        {
            get
            {
                if (sprites.Length != 0)
                {

                    return sprites[0].color.a;
                }
                else if (images.Length != 0)
                {
                    return images[0].color.a;
                }

                Debug.LogError("SpriteRenderer�܂���Image���A�^�b�`���Ă�������");
                return 0.0f;
            }
            set
            {
                if (sprites.Length != 0)
                {
                    foreach (SpriteRenderer sprite in sprites)
                    {
                        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, value);

                    }
                }
                else if (images.Length != 0)
                {
                    foreach (Image image in images)
                    {

                        image.color = new Color(image.color.r, image.color.g, image.color.b, value);
                    }
                }
                else if (texts.Length != 0)
                {
                    foreach (TextMeshProUGUI text in texts)
                    {

                        text.color = new Color(text.color.r, text.color.g, text.color.b, value);
                    }
                }
                else
                {
                    Debug.LogError("SpriteRenderer�܂���Image���A�^�b�`���Ă�������");
                }
            }
        }

    }
    /// <summary>
    /// �}�`��localScale.x�܂���y���Q�ƒl�ɍ��킹�Ċg�k������
    /// </summary>
    [Serializable] public class BarByParam
    {

        [SerializeField] private GameObject bar;
        [SerializeField] private float entity;
        [SerializeField] private float max;
        [SerializeField] private float ratio;
        [SerializeField] private bool warp;

        public void Update(float entity, float max)
        {
            this.entity = entity;
            this.max = max;
            ratio = entity / max;
            if (warp == true)
            {
                bar.transform.localScale = new Vector3(bar.transform.localScale.x, ratio);

            }
            else
            {
                bar.transform.localScale = new Vector3(ratio, bar.transform.localScale.y);


            }
        }
    }




#if UNITY_EDITOR
    /// <summary>
    /// serializedObjectUpdate�Ɋ֐���ǉ�����
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MyEditor<T> : Editor where T : UnityEngine.Object
    {
        protected Action serializedObjectUpdate;
        protected T tg;
        protected void Initialize()
        {
            tg = (T)target;
        }
        

        public override void OnInspectorGUI()
        {

            SerializedObjectUpdate();
            EndUpdate();

        }

        protected void EndUpdate()
        {

            if (GUI.changed)
            {
                EditorUtility.SetDirty(tg);
            }

        }

        void SerializedObjectUpdate()
        {
            serializedObject.Update();
            {
                serializedObjectUpdate?.Invoke();
            }
            serializedObject.ApplyModifiedProperties();
        }

    }


    /// <summary>
    /// OnEnable��
    /// </summary>
    public class MyPropertyDrawer : PropertyDrawer
    {
        protected Rect pos;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            {
                pos = position;

                // �q�̃t�B�[���h���C���f���g���Ȃ� 
                var indent = EditorGUI.indentLevel;
                EditorGUI.indentLevel = 0;

                Update(position, property, label);


                // �C���f���g�����ʂ�ɖ߂��܂�
                EditorGUI.indentLevel = indent;
            }
            EditorGUI.EndProperty();


            EditorGUI.BeginDisabledGroup(true);
            {
            }
            EditorGUI.EndDisabledGroup();
        }

        public float RightEnd(Rect pos)
        {
            return pos.width - 90;
        }
        protected virtual void Update(Rect ops, SerializedProperty property, GUIContent label)
        { }

        protected virtual void ReadOnly(Rect pos, SerializedProperty property, GUIContent label)
        { }

    }

#endif
}