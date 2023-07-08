using System;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Скрипт реализующий общую логику для всех меню.
    /// </summary>
    public abstract class AbstractMenu : MonoBehaviour
    {
        /// <summary>
        /// Тип меню, задается из инспектора.
        /// </summary>
        [SerializeField] protected MenuType _type;
        [SerializeField] private bool _isRememberForBack = true;
        /// <summary>
        /// Тип меню.
        /// </summary>
        public MenuType type
        {
            get => _type;
        }
        /// <summary>
        /// Надо ли запоминать меню, чтобы потом к нему можно было вренуться.
        /// </summary>
        public bool isRememberForBack
        {
            get => _isRememberForBack;
        }
        protected MenuController menuController;
        /// <summary>
        /// Проверить, что тип меню установлен.
        /// </summary>
        private void CheckValidType()
        {
            if (type == MenuType.unknow)
            {
                Debug.LogError($"Menu {gameObject.name} have not valid type!");
            }
            else
            {
                Array types = Enum.GetValues(typeof(MenuType));
                bool isFoundType = false;
                foreach(object type in types)
                {
                    if (this.type == (MenuType)type)
                    {
                        isFoundType= true;
                        break;
                    }
                }

                if(!isFoundType)
                {
                    Debug.LogError($"Menu {gameObject.name} have not valid type!");
                }
            }
        }
        /// <summary>
        /// Провести инициализацию данных в меню.
        /// Всегда запускать Base.Init() перед прочими инициализациями.
        /// </summary>
        public virtual void Init(MenuController menuController)
        {
            CheckValidType();
            this.menuController = menuController;
        }
        /// <summary>
        /// Установить активность объекта меню, на котором висит скрипт.
        /// </summary>
        /// <param name="isActive"></param>
        public virtual void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
        /// <summary>
        /// Открыть меню заданного типа.
        /// <br/>Все меню прочих типов будут скрыты.
        /// <br/>Текущее меню будет сохранено для возвращения обратно,
        /// если в нем указано, что это можно сделать.
        /// </summary>
        /// <param name="type"></param>
        protected void OpenMenu(MenuType type)
        {
            menuController.OpenMenu(type);
        }
    }
}