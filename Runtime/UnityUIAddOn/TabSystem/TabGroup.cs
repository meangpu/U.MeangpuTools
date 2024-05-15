using System.Collections.Generic;
using UnityEngine;
using Meangpu.Audio;

namespace Meangpu
{
    public class TabGroup : MonoBehaviour
    {
        // learn from [Creating a Custom Tab System in Unity - YouTube](https://www.youtube.com/watch?v=211t6r12XPQ&t=298s)
        List<TabButton> _tabButtons;

        [Header("sprite")]
        [SerializeField] Sprite _tabIdle;
        [SerializeField] Sprite _tabHover;
        [SerializeField] Sprite _tabActive;

        [Header("color")]
        [SerializeField] Color _tabIdleColor = Color.white;
        [SerializeField] Color _tabHoverColor = Color.white;
        [SerializeField] Color _tabActiveColor = Color.white;

        TabButton _selectedTab;
        [Header("audio")]
        [SerializeField] SOSound _hoverSound;
        [SerializeField] SOSound _clickSound;
        [Header("parent of page")]
        [Tooltip("need to have same child count as tab button")]
        [SerializeField] Transform _parentPageTransform;

        public void Subscribe(TabButton button)
        {
            if (_tabButtons == null) _tabButtons = new List<TabButton>();
            _tabButtons.Add(button);
        }

        public void OnTabEnter(TabButton button)
        {
            ResetTab();
            if (_selectedTab == null || button != _selectedTab)
            {
                _hoverSound?.PlayOneShot();
                button.SetBackground(_tabHover);
                button.SetColor(_tabHoverColor);
            }
        }

        public void OnTabExit(TabButton button)
        {
            ResetTab();
        }

        public void OnTabSelect(TabButton button)
        {
            if (_selectedTab != null)
            {
                _selectedTab.DeSelect();
            }
            ResetTab();

            _selectedTab = button;
            _selectedTab.Select();
            _clickSound?.PlayOneShot();
            button.SetBackground(_tabActive);
            button.SetColor(_tabActiveColor);

            int tabIndex = button.transform.GetSiblingIndex();
            foreach (Transform child in _parentPageTransform) child.gameObject.SetActive(false);
            _parentPageTransform.GetChild(tabIndex).gameObject.SetActive(true);

        }

        public void ResetTab()
        {
            foreach (TabButton button in _tabButtons)
            {
                if (_selectedTab != null && button == _selectedTab) { continue; }
                button.SetBackground(_tabIdle);
                button.SetColor(_tabIdleColor);
            }
        }

    }
}
