using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Injecto
{

    [AttributeUsage(AttributeTargets.Field)]
    public class Binding : Attribute { }

    public static class Injector
    {
        static Dictionary<Type, List<object>> objs = new Dictionary<Type, List<object>>();
        static Dictionary<Type, object> attrs = new Dictionary<Type, object>();

        /// <summary>
        /// Create an object of type T (if not MonoBehavior), and then binding attributes to this object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void BindingTo<T>()
        {
            object newObj = null;
            if (typeof(T).BaseType == typeof(MonoBehaviour))
            {
                newObj = Object.FindObjectOfType(typeof(T));
            }
            else
            {
                newObj = typeof(T).GetConstructors()[0].Invoke(new object[0]);
            }

            if (newObj != null)
            {
                if (objs.ContainsKey(typeof(T)) == false)
                {
                    objs.Add(typeof(T), new List<object>());
                }

                objs[typeof(T)].Add(newObj);
                attrs.Add(typeof(T), newObj);
            }
        }

        /// <summary>
        /// Binding this object to any other object need it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="attr"></param>
        public static void BindInstance<T>(T attr)
        {
            attrs.Add(typeof(T), attr);
        }


        /// <summary>
        /// Initiate the Injecto
        /// </summary>
        public static void Init()
        {
            var manager = InjectoManager.InitManager();

            foreach (var obj in objs)
            {
                var _type = obj.Key;
                var _objts = obj.Value;
                FieldInfo[] objectFields = _type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                //Debug.Log($"Binding for {_type}");
                //Debug.Log(objectFields.Length);

                //Debug.Log($"Binding: {objectFields[i].Name}");
                foreach (var _objt in _objts)
                {
                    if (_objt is IUpdatable)
                    {
                        manager.RegisterUpdating(_objt as IUpdatable);
                    }

                    for (int i = 0; i < objectFields.Length; i++)
                    {
                        if (Attribute.GetCustomAttribute(objectFields[i], typeof(Binding)) is Binding attribute)
                        {
                            objectFields[i].SetValue(_objt, attrs[objectFields[i].FieldType]);

                        }
                    }
                }
            }
        }
    }
}