using FluentAssertions;
using GHub.Data.Games.TicTacToe;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class TicTacToeGameShould
    {
        private TicTacToeGame _game;
        private TicTacToePlayer _playerA;
        private TicTacToePlayer _playerB;

        [SetUp]
        public void Setup()
        {
            _playerA = new TicTacToePlayer() {Mark = TicTacToeMarkEnum.O};
            _playerB = new TicTacToePlayer() {Mark = TicTacToeMarkEnum.X};
            _game = new TicTacToeGame
            {
                PlayerA = _playerA,
                PlayerB = _playerB,
                
            };
        }

        [Test]
        public void KeepTrackOfCorrectMoveOrder_BadMove()
        {
            _game.CurrentMove.Should().NotBe(_playerA.Mark);

            var move = new TicTacToeMove(_playerA, 0, 0);
            var result = _game.MakeMove(move);
            result.Should().BeFalse();

            _game.CurrentMove.Should().Be(_playerB.Mark);
        }

        [Test]
        public void KeepTrackOfCorrectMoveOrder_GoodMove()
        {
            _game.CurrentMove.Should().Be(_playerB.Mark);

            var move = new TicTacToeMove(_playerB, 0, 0);
            var result = _game.MakeMove(move);
            result.Should().BeTrue();


            _game.CurrentMove.Should().Be(_playerA.Mark);

        }


        [Test]
        public void KeepTrackOfCorrectMoves_MarkSamePosition()
        {
            _game.CurrentMove.Should().Be(_playerB.Mark);


            var move = new TicTacToeMove(_playerB, 0, 0);
            var result = _game.MakeMove(move);
            result.Should().BeTrue();


            _game.CurrentMove.Should().Be(_playerA.Mark);

            var illegalMove = _game.MakeMove(_playerA, 0, 0); // same coords
            illegalMove.Should().BeFalse();

        }


        [Test]
        public void RaiseAnEventOn_RowWin()
        {
            bool result;

            // test win on 
            // x|x|x
            // o|o| 
            //  | |

            var gameEnded = false;
            _game.OnGameEnd += () => gameEnded = true;

            result = _game.MakeMove(_playerB, 0, 0);
            result.Should().BeTrue();

            result = _game.MakeMove(_playerA, 1, 0);
            result.Should().BeTrue();

            result = _game.MakeMove(_playerB, 0, 1);
            result.Should().BeTrue();

            result = _game.MakeMove(_playerA, 1, 1);
            result.Should().BeTrue();

            result = _game.MakeMove(_playerB, 0, 2);
            result.Should().BeTrue();

            gameEnded.Should().BeTrue();

            _game.TicTacToeGameResult.Should().Be(TicTacToeGameResult.PlayerBWin);
        }


        [Test]
        public void RaiseAnEventOn_ColumnWin()
        {
            bool result;

            // test win on 
            // x|o| 
            // x|o| 
            // x| |

            var gameEnded = false;
            _game.OnGameEnd += () => gameEnded = true;

            result = _game.MakeMove(_playerB, 0, 0);
            result.Should().BeTrue();

            result = _game.MakeMove(_playerA, 0, 1);
            result.Should().BeTrue();

            result = _game.MakeMove(_playerB, 1, 0);
            result.Should().BeTrue();

            result = _game.MakeMove(_playerA, 1, 1);
            result.Should().BeTrue();

            result = _game.MakeMove(_playerB, 2, 0);
            result.Should().BeTrue();

            gameEnded.Should().BeTrue();

            _game.TicTacToeGameResult.Should().Be(TicTacToeGameResult.PlayerBWin);
        }


        [Test]
        public void RaiseAnEventOn_DiagonalWin0()
        {
            bool result;

            // test win on 
            // x|o| 
            // o|x| 
            //  | |x

            var gameEnded = false;
            _game.OnGameEnd += () => gameEnded = true;

            result = _game.MakeMove(_playerB, 0, 0);
            result.Should().BeTrue();

            result = _game.MakeMove(_playerA, 0, 1);
            result.Should().BeTrue();

            result = _game.MakeMove(_playerB, 1, 1);
            result.Should().BeTrue();

            result = _game.MakeMove(_playerA, 1, 0);
            result.Should().BeTrue();

            result = _game.MakeMove(_playerB, 2, 2);
            result.Should().BeTrue();

            gameEnded.Should().BeTrue();

            _game.TicTacToeGameResult.Should().Be(TicTacToeGameResult.PlayerBWin);

        }


        [Test]
        public void RaiseAnEventOn_DiagonalWin1()
        {
            bool result;

            // test win on 
            //  |o|x 
            // o|x| 
            // x| |

            var gameEnded = false;
            _game.OnGameEnd += () => gameEnded = true;

            result = _game.MakeMove(_playerB, 0, 2);
            result.Should().BeTrue();

            result = _game.MakeMove(_playerA, 0, 1);
            result.Should().BeTrue();

            result = _game.MakeMove(_playerB, 1, 1);
            result.Should().BeTrue();

            result = _game.MakeMove(_playerA, 1, 0);
            result.Should().BeTrue();

            result = _game.MakeMove(_playerB, 2, 0);
            result.Should().BeTrue();

            gameEnded.Should().BeTrue();

            _game.TicTacToeGameResult.Should().Be(TicTacToeGameResult.PlayerBWin);
        }

    }
}