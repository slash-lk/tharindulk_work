/*
GAME RULES:

- The game has 2 players, playing in rounds
- In each turn, a player rolls a dice as many times as he whishes. Each result get added to his ROUND score
- BUT, if the player rolls a 1, all his ROUND score gets lost. After that, it's the next player's turn
- The player can choose to 'Hold', which means that his ROUND score gets added to his GLBAL score. After that, it's the next player's turn
- The first player to reach 100 points on GLOBAL score wins the game

*/


var scores, roundScore, activePlayer, dice, diceDOM, isPlaying;


diceDOM = document.querySelector('.dice');

init();

document.querySelector('.btn-roll').addEventListener('click', () => {
    
    if (!isPlaying) {
        return;
    }

    // 1. Random number
    dice = Math.ceil(Math.random() * 6);

    // 2. Display the result
    diceDOM.style.display = 'block';
    diceDOM.src = `dice-${dice}.png`;

    // 3. Update the roundScore if the rolled number was not a 1
    if (dice !== 1) {
        roundScore += dice;
        document.getElementById(`current-${activePlayer}`).textContent = roundScore; 
    }
    else {
        switchPlayer();
    }
});


document.querySelector('.btn-hold').addEventListener('click', () => {

    if (!isPlaying) {
        return;
    }

    scores[activePlayer] += roundScore;
    document.getElementById(`score-${activePlayer}`).textContent = scores[activePlayer];

    // Check if won
    if (scores[activePlayer] >= 20) {
        document.querySelector(`#name-${activePlayer}`).textContent = "Winner!";
        document.querySelector(`.player-${activePlayer}-panel`).classList.remove('active');
        document.querySelector(`.player-${activePlayer}-panel`).classList.add('winner');

        isPlaying = false;
    } 
    else {
        switchPlayer();  
    }      
});


function switchPlayer() {
    roundScore = 0;
    document.getElementById(`current-${activePlayer}`).textContent = roundScore;

    activePlayer = (activePlayer === 0) ? 1 : 0;

    document.querySelector('.player-0-panel').classList.toggle('active');
    document.querySelector('.player-1-panel').classList.toggle('active');
    diceDOM.style.display = 'none';
}


function init() {
    scores = [0, 0];
    roundScore = 0;
    activePlayer = 0;
    isPlaying = true;

    document.getElementById('score-0').textContent = "0";
    document.getElementById('score-1').textContent = "0";
    document.getElementById('current-0').textContent = "0";
    document.getElementById('current-1').textContent = "0";

    document.getElementById('name-0').textContent = "Player 1";
    document.getElementById('name-1').textContent = "Player 2";

    document.querySelector('.player-0-panel').classList.remove('active');
    document.querySelector('.player-0-panel').classList.remove('winner');
    document.querySelector('.player-1-panel').classList.remove('active');
    document.querySelector('.player-1-panel').classList.remove('winner');

    document.querySelector('.player-0-panel').classList.add('active');

    diceDOM.style.display = 'none';
}


document.querySelector('.btn-new').addEventListener('click', init);



