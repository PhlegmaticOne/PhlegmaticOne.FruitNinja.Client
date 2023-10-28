﻿using App.Scripts.Shared.Progress.Services;

namespace App.Scripts.Game.Features.Score.Services {
    public class SessionScoreService : ISessionScoreService {
        private readonly IPlayerScoreService _playerScoreService;
        private int _sessionScore;
        
        public SessionScoreService(IPlayerScoreService playerScoreService) {
            _playerScoreService = playerScoreService;
        }
        
        public int MaxScore => _playerScoreService.MaxScore;

        public int AddScore(int score) {
            _sessionScore += score;

            if (_sessionScore > _playerScoreService.MaxScore) {
                _playerScoreService.ChangeMaxScore(_sessionScore);
            }

            return _sessionScore;
        }
    }
}