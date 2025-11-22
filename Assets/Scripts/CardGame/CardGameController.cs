using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace CardGame
{
    public enum GameState
    {
        None = 0,
        GameStart = 1,
        PlayerStart = 2,
        PlayerEnd = 3,
        OpponentStart = 4,
        OpponentEnd = 5,
        GameEnd = 6,
        DealCards = 7,
    }

    public class CardGameController : MonoBehaviour
    {
        // START GAME: Chuan bi, animation start game, ...
        // START PLAYER 1 TURN: Duoc thuc hien bao nhieu action?
        // END PLAYER 1 TURN
        // START PLAYER 2 TURN:
        // END PLAYER 2 TURN
        // ...
        // END GAME
        // 1 TURN = 1 ACTION

        public static event Action GameStarted;
        public static event Action<Card[], int> CardsDeal;
        public static event Action GameEnded;
        public static event Action PlayerStarted;
        public static event Action PlayerEnded;
        public static event Action OpponentStarted;
        public static event Action OpponentEnded;

        [SerializeField] private CardGameData gameData;

        [Header("Announcer")]
        [SerializeField] private GameObject announcer;
        [SerializeField] private TMP_Text announcerText;

        [Header("Scores")]
        [SerializeField] private TMP_Text playerScoreText;
        [SerializeField] private TMP_Text opponentScoreText;

        private const int NumberToStartCompare = 2;

        private Card[] PlayerCards;
        private Card[] OpponentCards;
        private int _playerScore;
        private int _opponentScore;
        private int _cardsPlayedCount;
        private CardItem _playerPlayedCard;
        private CardItem _opponentPlayedCard;

        public static CardGameController Instance;

        public Card GetOpponentCard(int index)
        {
            if (OpponentCards == null || OpponentCards.Length <= 0 || index >= OpponentCards.Length)
            {
                return null;
            }

            return OpponentCards[index];
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(Instance);
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            announcer.SetActive(false);

            CardController.CardPlayed += OnCardPlayed;
        }

        private void OnDestroy()
        {
            CardController.CardPlayed -= OnCardPlayed;
        }

        private void Start()
        {
            StartGame();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                OpponentEnded?.Invoke();
            }
        }

        private void StartGame()
        {
            playerScoreText.text = $"P: {_playerScore}";
            opponentScoreText.text = $"O: {_opponentScore}";

            GameStarted?.Invoke();
            OnChangeGameState(GameState.GameStart);
            DealCards();
            StartCoroutine(StartPlayerTurn(true));
        }

        private void DealCards()
        {
            if (gameData == null)
            {
                return;
            }

            var cards = gameData.Cards;
            var count = cards.Count;
            var dealCount = gameData.DealQuantity;
            PlayerCards = new Card[dealCount];
            OpponentCards = new Card[dealCount];

            for (var i = 0; i < dealCount; i++)
            {
                PlayerCards[i] = cards[GetRandomIndex(count)];
                OpponentCards[i] = cards[GetRandomIndex(count)];
            }

            CardsDeal?.Invoke(PlayerCards, dealCount);
            OnChangeGameState(GameState.DealCards);
        }

        private IEnumerator StartPlayerTurn(bool isPlayer)
        {
            yield return new WaitForSeconds(1f);
            if (isPlayer)
            {
                PlayerStarted?.Invoke();
                OnChangeGameState(GameState.PlayerStart);
            }
            else
            {
                OpponentStarted?.Invoke();
                OnChangeGameState(GameState.OpponentStart);
            }
        }

        private IEnumerator ChangeGameState(GameState state)
        {
            announcer.SetActive(true);
            announcerText.text = state.ToString();
            yield return new WaitForSeconds(2f);
            announcer.SetActive(false);
        }

        private void OnChangeGameState(GameState state)
        {
            StartCoroutine(ChangeGameState(state));
        }

        private void OnCardPlayed(CardItem cardItem, bool isPlayer)
        {
            if (cardItem == null)
            {
                return;
            }

            UnityEngine.Debug.Log($"PLAYER PLAYED: card={cardItem.GetData().CardName}, isPlayer={isPlayer}");
            if (isPlayer)
            {
                _playerPlayedCard = cardItem;
                _cardsPlayedCount++;
                EndPlayerTurn();
            }
            else
            {
                _opponentPlayedCard = cardItem;
                _cardsPlayedCount++;
                EndOpponentTurn();
            }

            CheckPlayedCards();
        }

        private void CheckPlayedCards()
        {
            if (_playerPlayedCard == null || _opponentPlayedCard == null)
            {
                return;
            }

            if (_cardsPlayedCount >= NumberToStartCompare)
            {
                var playerValue = _playerPlayedCard.GetData().CardValue;
                var opponentValue = _opponentPlayedCard.GetData().CardValue;
                if (playerValue > opponentValue)
                {
                    _playerScore += 1;
                    playerScoreText.text = $"P: {_playerScore}";
                }
                else if (playerValue < opponentValue)
                {
                    _opponentScore += 1;
                    opponentScoreText.text = $"O: {_opponentScore}";
                }

                Destroy(_playerPlayedCard.gameObject);
                Destroy(_opponentPlayedCard.gameObject);
                _cardsPlayedCount = 0;
            }
        }

        private void EndOpponentTurn()
        {
            OpponentEnded?.Invoke();
            OnChangeGameState(GameState.OpponentEnd);
            StartCoroutine(StartPlayerTurn(true));
        }

        private void EndPlayerTurn()
        {
            PlayerEnded?.Invoke();
            OnChangeGameState(GameState.PlayerEnd);
            StartCoroutine(StartPlayerTurn(false));
        }

        private int GetRandomIndex(int count)
            => new System.Random().Next(count);
    }
}