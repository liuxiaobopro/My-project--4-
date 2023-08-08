using UnityEngine;

namespace Sundry
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance != null && gameObject != null) // 如果已经存在实例则销毁自身
                Destroy(gameObject); // 销毁自身
            else
                Instance = this as T; // 设置实例

            if (!gameObject.transform.parent) // 如果没有父物体
            {
                DontDestroyOnLoad(gameObject); // 切换场景时不销毁
            }
        }
    }
}