using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private KeyCode pauseButtonKey = KeyCode.Escape;
        [SerializeField] private AbstractMenu[] menus = new AbstractMenu[0];
        private Stack<AbstractMenu> openedMenus = new Stack<AbstractMenu>();
        public AbstractMenu CurrentMenu => _currentMenu;
        private AbstractMenu _currentMenu;
        [HideInInspector] public EnumScenes gameMode;
        private AbstractMenu GetMenu(MenuType type)
        {
            foreach (AbstractMenu menu in menus)
            {
                if ((int)menu.type == (int)type) return menu;
            }

            Debug.LogError("Menu is not found!");
            return null;
        }
        /// <summary>
        /// Скрыть все меню, кроме текущего. Текущее меню будет включено.
        /// </summary>
        private void HideAllMenuAndActiveCurrent()
        {
            foreach (AbstractMenu menu in menus)
            {
                menu.SetActive(menu == _currentMenu);
            }
        }
        /// <summary>
        /// Открыть меню заданного типа.
        /// <br/>Все меню прочих типов будут скрыты.
        /// <br/>Текущее меню будет сохранено для возвращения обратно,
        /// если в нем указано, что это можно сделать.
        /// </summary>
        /// <param name="type"></param>
        public void OpenMenu(MenuType type)
        {
            if (_currentMenu != null && _currentMenu.isRememberForBack)
            {
                openedMenus.Push(_currentMenu);
            }
            _currentMenu = GetMenu(type);
            HideAllMenuAndActiveCurrent();
        }
        /// <summary>
        /// Открыть меню, которое было открыто перед нынешним.
        /// </summary>
        public void OpenPreviousMenu()
        {
            if (openedMenus.Count == 0)
            {
                if (gameMode == EnumScenes.MenuScene)
                {
                    _currentMenu = GetMenu(MenuType.mainMenu);
                }
                else
                {
                    _currentMenu = GetMenu(MenuType.menuPause);
                }
            }
            else
            {
                _currentMenu = openedMenus.Pop();
            }
            HideAllMenuAndActiveCurrent();
        }
        public void GoToMainMenu()
        {
            _currentMenu = GetMenu(MenuType.mainMenu);
            openedMenus.Clear();
            HideAllMenuAndActiveCurrent();
        }
        /// <summary>
        /// Проверить, что все меню разных типов.
        /// </summary>
        private void CheckMenuTypes()
        {
            HashSet<MenuType> menuTypes = new HashSet<MenuType>();
            foreach (AbstractMenu menu in menus)
            {
                if (menuTypes.Contains(menu.type))
                {
                    Debug.LogError($"Menu with type {menu.type} more then 1!");
                }
                else
                {
                    menuTypes.Add(menu.type);
                }
            }
        }
        private void Awake()
        {
            foreach (AbstractMenu menu in menus)
            {
                menu.Init(this);
            }
            CheckMenuTypes();

            GoToMainMenu();
        }

        public event Action pauseKeyDowned;
        private void Update()
        {
            if (Input.GetKeyDown(pauseButtonKey))
            {
                if (gameMode != EnumScenes.GameScene) return;

                pauseKeyDowned?.Invoke();
                //if (_currentMenu.GetType() != typeof(PauseMenu))
                //    _currentMenu.SetActive(false);
            }
        }
    }
}