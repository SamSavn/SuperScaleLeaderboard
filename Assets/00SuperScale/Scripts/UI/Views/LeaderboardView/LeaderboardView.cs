using UnityEngine.UIElements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperScale.UI.Components;
using SuperScale.UI.Components.Layout;
using SuperScale.Data;
using SuperScale.Services;
using SuperScale.UI.Transitions;

namespace SuperScale.UI.Views
{
    public class LeaderboardView : OverlayView<LeaderboardViewPresenter, ILeaderboardView>, ILeaderboardView
    {
        public override string ID => "leaderboard";

        private List<LeaderboardEntry> _entries = new List<LeaderboardEntry>();

        private Column _entriesContainer;
        private VisualElement _exitButton;
        private VisualElement _background;

        private WaitForSeconds _wait;

        public LeaderboardView(LeaderboardViewModel model)
        {
            Presenter = new LeaderboardViewPresenter(this, model);

            TransitionsInfo info = ServiceRegistry.Get<InfoService>().TransitionsInfo;
            _wait = new WaitForSeconds(info.EntriesInsertDelay);
        }

        public override ITransition GetEnterTransition()
        {
            return new SlideTransition(this, new Vector2(0, -1f), SlideMode.In, .5f);
        }

        public override ITransition GetExitTransition()
        {
            return GetEnterTransition().Reverse();
        }

        public override void GetElements()
        {
            _exitButton = this.Q<VisualElement>("exit-button");
            _background = this.Q<VisualElement>("background");
            _entriesContainer = this.Q<Column>("entries-container");

            _background.AddToClassList("invisible");
            _exitButton.RegisterCallback<ClickEvent>(ExitButtonClickEventHandler);

            TriggerReadyState();
        }

        protected override void OnEnter()
        {
            _background.style.opacity = 1f;
            base.OnEnter();
        }

        protected override void OnExit()
        {
            GetEnterTransition().Reset();
            base.OnExit();
        }

        public void ShowEntries()
        {
            ServiceRegistry.Get<CoroutineService>().StartCoroutine(ShowEntriesCo());
        }

        private IEnumerator ShowEntriesCo()
        {
            yield return _wait;

            for (int i = 0; i < _entries.Count; i++)
            {
                _entriesContainer.Add(_entries[i]);
                yield return _wait;
            }
        }

        public void SetEntries(LeaderboardData data)
        {
            for (int i = 0; i < data.ranking.Count; i++)
            {
                _entries.Add(new LeaderboardEntry(data.ranking[i]));
            }

            RegisterAssetLoadedListener();
        }

        private void ExitButtonClickEventHandler(ClickEvent evt)
        {
            _background.style.opacity = 0;
            Presenter.ExitButtonPressed();
        }
    }
}