using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Injecto
{
    public class InjectoManager : MonoBehaviour
    {
        public static InjectoManager InitManager()
        {
            if (Instance == null) 
            {
                GameObject InjectoObject = new GameObject("InjectoManager");
                
                return InjectoObject.AddComponent<InjectoManager>();
            }
            return Instance;
        }

        List<IUpdatable> updatables;
        public static InjectoManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
                // Code for awake here

                updatables = new List<IUpdatable>();

            }
            else
            {
                Destroy(this.gameObject);
            }
        }


        // Start is called before the first frame update
        void Start()
        {

        }

        public bool RegisterUpdating(IUpdatable item)
        {
            if (updatables.Contains(item) == false)
            {
                updatables.Add(item);
                return true;
            }
            return false;
        }

        // Update is called once per frame
        void Update()
        {
            updatables.ForEach((item) => { 
                if (item != null) item.Update(); 
            });
        }
    }

    public interface IUpdatable
    {
        /// <summary>
        /// Injecto Update
        /// </summary>
        void Update();
    }
}
