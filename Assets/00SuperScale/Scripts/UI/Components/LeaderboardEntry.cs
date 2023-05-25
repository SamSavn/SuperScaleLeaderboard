using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using SuperScale.Data;
using SuperScale.Services;
using SuperScale.UI.Transitions;

namespace SuperScale.UI.Components
{
    public class LeaderboardEntry : Component
    {
        public new class UxmlFactory : UxmlFactory<LeaderboardEntry, UxmlTraits> { }
        public override string ID => "leaderboardentry";
        private string PlayerBackgroundClass => "player-entry-background";
        private string InvisibleClass => "invisible";
        public string Uid { get; private set; }

        private InfoService _infoService;
        private LeaderboardEntryData _entryData;

        private string _badgePrefix;
        private string _characterPrefix;
        private string _flagPrefix;
        private int _badgesNumber;
        private bool _isPlayer;

        private VisualElement _mainContainer;
        private VisualElement _background;
        private Image _badge;
        private Label _userPosition;
        private Image _userImageBackground;
        private Image _userImage;
        private Image _flag;
        private Label _userName;
        private Label _userScore;
        private Image _vipImage;

        public LeaderboardEntry()
        {
            AddToClassList(ID);            
            GetInfo();
        }

        public LeaderboardEntry(LeaderboardEntryData data) : this()
        {
            _entryData = data;
            Uid = _entryData.player.uid;
            _isPlayer = Uid.Equals(PlayerPrefs.GetString(_infoService.PlayerPrefsInfo.PlayerUidPPKey));

            RegisterAssetLoadedListener();
        }

        public override ITransition GetEnterTransition()
        {
            TransitionsInfo info = ServiceRegistry.Get<InfoService>().TransitionsInfo;
            return new OpacityTransition(_mainContainer, 0, 1f, info.EntriesFadeDuration);
        }

        private void GetInfo()
        {
            _infoService = ServiceRegistry.Get<InfoService>();
            _badgePrefix = _infoService.AddressesInfo.BadgeAdrsPrefix;
            _characterPrefix = _infoService.AddressesInfo.CharacterAdrsPrefix;
            _flagPrefix = _infoService.AddressesInfo.FlagAdrsPrefix;
            _badgesNumber = _infoService.LeaderboardInfo.LeaderboardBadgesNumber;
        }

        public override void GetElements()
        {
            _mainContainer = this.Q("main-container");
            _background = this.Q("entry-background");
            _badge = this.Q<Image>("badge");
            _userPosition = this.Q<Label>("user-position");
            _userImageBackground = this.Q<Image>("user-image-bg");
            _userImage = this.Q<Image>("user-image");
            _flag = this.Q<Image>("flag");
            _userName = this.Q<Label>("user-name");
            _userScore = this.Q<Label>("user-score");
            _vipImage = this.Q<Image>("vip-image");

            SetElements();
        }

        private void SetElements()
        {
            _mainContainer.AddToClassList(InvisibleClass);

            if (_isPlayer)
            {
                _background.AddToClassList(PlayerBackgroundClass);
            }

            SetBadge();
            SetUserImage();

            _flag.SetBackround($"{_flagPrefix}{_entryData.player.countryCode.ToLower()}");
            _userName.text = _entryData.player.username;
            _userScore.text = _entryData.points.ToString().InsertSpaces(3);
            _vipImage.ToggleVisibility(_entryData.player.isVip);
        }

        private void SetBadge()
        {
            bool showBadge = _entryData.ranking <= _badgesNumber;
            _userPosition.text = _entryData.ranking.ToString();

            if (showBadge)
            {
                _badge.SetBackround($"{_badgePrefix}{_entryData.ranking}");
            }
            else
            {
                _badge.Background = null;
            }
        }

        private void SetUserImage()
        {
            _userImageBackground.SetColor(_entryData.player.characterColor);
            _userImage.SetBackround($"{_characterPrefix}{_entryData.player.characterIndex}");
        }

        //private IEnumerator FadeIn()
        //{
        //    GameInfo gameInfo = ServiceRegistry.Get<InfoService>().GameInfo;
        //    yield return new WaitForSeconds(gameInfo.EntriesFadeDelay);
        //    //_mainContainer.style.opacity = 1f;
        //    _mainContainer.Fade(0, 1f, Mathf.FloorToInt(gameInfo.EntriesFadeDuration * 1000));
        //}

        //public override void OnShow()
        //{
        //    ServiceRegistry.Get<CoroutineService>().StartCoroutine(FadeIn());
        //}
    }
}